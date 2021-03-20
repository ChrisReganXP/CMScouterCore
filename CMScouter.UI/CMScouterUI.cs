using CMScouter.DataClasses;
using CMScouter.UI.Converters;
using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace CMScouter.UI
{
    public class CMScouterUI
    {
        private SaveGameData _savegame;
        private PlayerDisplayHelper _displayHelper;
        private IPlayerRater _rater;

        public DateTime GameDate;

        public CMScouterUI(string fileName)
        {
            SaveGameData file = FileFunctions.LoadSaveGameFile(fileName);

            _savegame = file;

            GameDate = _savegame.GameDate;

            ConstructLookups();

            IntrinsicMasker = new DefaultIntrinsicMasker();
            //_rater = new DefaultRater(IntrinsicMasker);
            //_rater = new InvestigationRater(IntrinsicMasker);
            _rater = new CoreRater(IntrinsicMasker);
        }

        public IIntrinsicMasker IntrinsicMasker { get; internal set; }

        public List<Club> GetClubs()
        {
            return _savegame.Clubs.Values.ToList();
        }

        public List<Nation> GetAllNations()
        {
            return _savegame.Nations.Values.ToList();
        }

        public List<Club_Comp> GetAllClubCompetitions()
        {
            return _savegame.ClubComps.Values.ToList();
        }

        public List<PlayerView> GetPlayerByPlayerId(List<int> playerIds)
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => playerIds.Contains(x._player.PlayerId));
            return ConstructPlayerByFilter(filter);
        }

        public List<PlayerView> GetPlayersBySecondName(string playerName)
        {
            List<int> surnameIds = _savegame.Surnames.Where(x => x.Value.StartsWith(playerName, StringComparison.InvariantCultureIgnoreCase)).Select(x => x.Key).ToList();
            Func<Player, bool> filter = new Func<Player, bool>(x => surnameIds.Contains(x._staff.SecondNameId));
            return ConstructPlayerByFilter(filter);
        }

        public List<PlayerView> GetHighestIntrinsic(DP dataPoint, short numberOfRecords)
        {
            SearchFilterHelper filterHelper = new SearchFilterHelper(_savegame, _rater);
            var playersToConstruct = filterHelper.OrderByDataPoint(dataPoint).Take(numberOfRecords).ToList();
            var list = _displayHelper.ConstructPlayers(playersToConstruct, _rater).ToList();
            return list.OrderByDescending(x => x.Attributes.Tackling).ToList();
        }

        public List<PlayerView> GetScoutResults(ScoutingRequest request)
        {
            List<Func<Player, bool>> filters = new List<Func<Player, bool>>();
            SearchFilterHelper filterHelper = new SearchFilterHelper(_savegame, _rater);

            if (request.PlayerId.HasValue)
            {
                filters.Add(x => x._player.PlayerId == request.PlayerId.Value);
            }
            else
            {
                filterHelper.CreateClubFilter(request, filters);
                filterHelper.CreatePositionFilter(request, filters);
                filterHelper.CreatePlayerBasedFilter(request, filters);
                filterHelper.CreateNationalityFilter(request, filters);
                filterHelper.CreateEUNationalityFilter(request, filters);
                filterHelper.CreateValueFilter(request, filters);
                filterHelper.CreateContractStatusFilter(request, filters);
                filterHelper.CreateAgeFilter(request, filters);
                filterHelper.CreateWageFilter(request, filters);
                filterHelper.CreateAvailabilityFilter(request, filters);
                filterHelper.CreateReputationFilter(request, filters);
            }

            var players = _savegame.Players;
            foreach (var filter in filters)
            {
                players = players.Where(x => filter(x)).ToList();
            }

            return ConstructPlayerByScoutingValueDesc(request.PlayerType, request.NumberOfResults, players);
        }

        private List<PlayerView> ConstructPlayerByFilter(Func<Player, bool> filter)
        {
            return _displayHelper.ConstructPlayers(ApplyFilterToPlayerList(filter), _rater).ToList();
        }

        private IEnumerable<Player> ApplyFilterToPlayerList(Func<Player, bool> filter, List<Player> specificPlayerList = null)
        {
            if (specificPlayerList == null)
            {
                specificPlayerList = _savegame.Players;
            }

            return specificPlayerList.Where(x => filter(x));
        }

        private List<PlayerView> ConstructPlayerByScoutingValueDesc(PlayerType? type, short numberOfResults, List<Player> preFilteredPlayers)
        {
            if (numberOfResults == 0)
            {
                numberOfResults = (short)Math.Min(100, preFilteredPlayers.Count);
            }

            var playersToConstruct = preFilteredPlayers ?? _savegame.Players;
            var list = _displayHelper.ConstructPlayers(playersToConstruct, _rater).ToList();

            var scoutOrder = ScoutingOrdering(list, type);
            return scoutOrder.Take(numberOfResults).ToList();
        }

        private IEnumerable<PlayerView> ScoutingOrdering(IEnumerable<PlayerView> list, PlayerType? type)
        {
            if (type == null)
            {
                return list.OrderByDescending(x => x.ScoutRatings.BestPosition.BestRole().PurchaseRating);
            }

            Dictionary<byte, int> scoreDistribution = new Dictionary<byte, int>();
            foreach (var player in list)
            {
                byte score = player.BestRoleRatingForPlayerType(type.Value).AbilityRating;
                if (scoreDistribution.ContainsKey(score))
                {
                    scoreDistribution[score]++;
                }
                else
                {
                    scoreDistribution.Add(score, 1);
                }
            }

            var keys = scoreDistribution.Keys.ToList();
            keys.Sort();
            keys.ForEach(x =>
            {
                int percent = (int)Math.Round(scoreDistribution[x] / (decimal)list.Count() * 100);
                Debug.WriteLine($"{x} => {scoreDistribution[x]} {percent}");
            });

            byte cumulativePercent = 0;
            for (byte i = 1; i < 100; i++)
            {
                cumulativePercent += scoreDistribution.ContainsKey(i) ? (byte)Math.Round(scoreDistribution[i] / (decimal)list.Count() * 100) : 0;
                Debug.WriteLine(cumulativePercent);
            }

            return list.OrderByDescending(x => x.ScoutRatings.GroupedRatings.playmakingRating);
            // return list.OrderByDescending(x => x.BestRoleRatingForPlayerType(type.Value).AbilityRating);
        }

        private void ConstructLookups()
        {
            Lookups lookups = new Lookups();
            lookups.clubNames = _savegame.Clubs.Values.ToDictionary(x => x.ClubId, x => x.Name);
            lookups.firstNames = _savegame.FirstNames;
            lookups.secondNames = _savegame.Surnames;
            lookups.commonNames = _savegame.CommonNames;
            lookups.nations = NationConverter.ConvertNations(_savegame.Nations);
            _displayHelper = new PlayerDisplayHelper(lookups, _savegame.GameDate);
        }
    }
}
