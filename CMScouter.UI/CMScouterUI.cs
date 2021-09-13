using CMScouter.DataClasses;
using CMScouter.DataContracts;
using CMScouter.UI.Converters;
using CMScouter.UI.DataClasses;
using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMScouter.UI
{
    public class CMScouterUI
    {
        private static List<PropertyInfo> csv_order = new List<PropertyInfo>()
        {
            typeof(PlayerView).GetProperty(nameof(PlayerView.FirstName)), typeof(PlayerView).GetProperty(nameof(PlayerView.SecondName)), typeof(PlayerView).GetProperty(nameof(PlayerView.CurrentAbility)), typeof(PlayerView).GetProperty(nameof(PlayerView.PotentialAbility))
        };

        private static List<PropertyInfo> csv_positions_order = new List<PropertyInfo>()
        {
            typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.GK)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.SW)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.DF)), 
            typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.DM)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.WingBack)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.MF)), 
            typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.AM)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.ST)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.FreeRole)), 
            typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.Right)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.Centre)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.Left))
        };

        private static List<PropertyInfo> csv_attributes_order = new List<PropertyInfo>()
        {
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Acceleration)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Adaptability)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Aggression)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Agility)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Ambition)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Anticipation)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Balance)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Bravery)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Consistency)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Corners)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Creativity)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Crossing)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Decisions)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Determination)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Dirtiness)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Dribbling)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Finishing)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Flair)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.FreeKicks)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Handling)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Heading)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.ImportantMatches)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Influence)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.InjuryProneness)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Jumping)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.LeftFoot)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.LongShots)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Loyalty)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Marking)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.NaturalFitness)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.OffTheBall)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.OneOnOnes)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Pace)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Passing)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Penalties)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Positioning)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Pressure)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Professionalism)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Reflexes)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.RightFoot)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Sportsmanship)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Stamina)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Strength)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Tackling)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Teamwork)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Technique)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Temperament)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.ThrowIns)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Versitility)), typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.WorkRate))

        };

        private SaveGameData _savegame;
        private PlayerDisplayHelper _displayHelper;
        private IPlayerRater _rater;

        public DateTime GameDate;

        public CMScouterUI(string fileName, decimal valueMultiplier)
        {
            // IntrinsicMasker = new DefaultIntrinsicMasker();
            IntrinsicMasker = new MadScientist_MatchMasker();

            //_rater = new DefaultRater(IntrinsicMasker);
            //_rater = new InvestigationRater(IntrinsicMasker);

            GroupedRoleWeights tester = new GroupedRoleWeights(Roles.GK);
            tester.Acceleration = 5;
            tester.SpeedPercent = 100;

            string testerJson = JsonSerializer.Serialize(tester);

            GroupedRoleWeights weights = JsonSerializer.Deserialize<GroupedRoleWeights>(@"{""Role"" : 2, ""SpeedPercent"" : 100, ""Acceleration"": 5}");

            _rater = new GroupedAttributeRater(IntrinsicMasker, weights);

            LoadGameData(fileName, valueMultiplier);
        }

        private void LoadGameData(string fileName, decimal valueMultiplier)
        {
            SaveGameData file = FileFunctions.LoadSaveGameFile(fileName, valueMultiplier);

            _savegame = file;

            GameDate = _savegame.GameDate;

            ConstructLookups();
        }

        public IIntrinsicMasker IntrinsicMasker { get; internal set; }

        public List<Club> GetClubs()
        {
            return _savegame.Clubs.Values.ToList();
        }

        public Club GetClubByName(string name)
        {
            return _savegame.Clubs.FirstOrDefault(x => x.Value.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase)).Value;
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

            if (filters.Count == 0)
            {
                return new List<PlayerView>();
            }

            return ConstructPlayerByScoutingValueDesc(request.PlayerType, request.NumberOfResults, players, request.OutputDebug);
        }

        public PlayerView GetPlayerByPlayerId(int playerId, ConstructPlayerOptions options)
        {
            return _displayHelper.ConstructPlayers(_savegame.Players.Where(x => x._player.PlayerId == playerId), _rater, options).FirstOrDefault();
        }

        public string CreateExportSet(List<int> playerIds)
        {
            if (playerIds == null)
            {
                return string.Empty;
            }

            List<PlayerView> players = GetPlayerByPlayerId(playerIds);
            if (players == null || players.Count() == 0)
            {
                return "no records";
            }

            StringBuilder csv = new StringBuilder();

            csv.Append(string.Join(",", csv_order.Select(x => x.Name)) + ",");
            csv.Append(string.Join(",", csv_attributes_order.Select(x => x.Name)) + ",");
            csv.Append(string.Join(",", csv_positions_order.Select(x => x.Name)));
            csv.Append(Environment.NewLine);

            foreach (var player in players)
            {
                csv.Append(player.CreateCSVText(csv_order, csv_attributes_order,csv_positions_order) + Environment.NewLine);
                
                // Append blank line to enable comparison with another file
                csv.Append(Environment.NewLine);
            }

            return csv.ToString();
        }

        public void UpdateInflationValue(decimal inflation)
        {
            LoadGameData(_savegame.FileName, inflation);
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

        private List<PlayerView> ConstructPlayerByScoutingValueDesc(PlayerPosition? type, short numberOfResults, List<Player> preFilteredPlayers, bool outputDebug)
        {
            if (numberOfResults == 0)
            {
                numberOfResults = (short)Math.Min(100, preFilteredPlayers.Count);
            }

            var playersToConstruct = preFilteredPlayers ?? _savegame.Players;

            _rater.OutputDebug(outputDebug);
            var list = _displayHelper.ConstructPlayers(playersToConstruct, _rater).ToList();
            _rater.OutputDebug(false);

            var scoutOrder = ScoutingOrdering(list, type);
            return scoutOrder.Take(numberOfResults).ToList();
        }

        private IEnumerable<PlayerView> ScoutingOrdering(IEnumerable<PlayerView> list, PlayerPosition? type)
        {
            if (type == null)
            {
                // return list.OrderByDescending(x => x.WagePerWeek);
                return list.OrderByDescending(x => x.ScoutRatings.BestPosition.BestRole.AbilityRating);
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

            // return list.OrderByDescending(x => x.Age);
            return list.OrderByDescending(x => x.BestRoleRatingForPlayerType(type.Value).AbilityRating);
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
