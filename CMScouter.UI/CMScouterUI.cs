using CMScouter.DataClasses;
using CMScouter.DataContracts;
using CMScouter.UI.Converters;
using CMScouter.UI.DataClasses;
using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CMScouter.UI
{
    class CustomSearch
    {
        public string SearchName { get; set; }

        public string SearchId { get; set; }

        public List<PlayerView> SearchMethod { get; set; }
    }

    public class CMScouterUI
    {
        private List<CustomSearch> customSearches = new List<CustomSearch>();

        private static List<PropertyInfo> csv_order = new List<PropertyInfo>()
        {
            typeof(PlayerView).GetProperty(nameof(PlayerView.FirstName)), 
            typeof(PlayerView).GetProperty(nameof(PlayerView.SecondName)),
            typeof(PlayerView).GetProperty(nameof(PlayerView.PlayerId)),
            typeof(PlayerView).GetProperty(nameof(PlayerView.PlayingPositionDescription)),
            typeof(PlayerView).GetProperty(nameof(PlayerView.Age)), 
            typeof(PlayerView).GetProperty(nameof(PlayerView.CurrentAbility)), 
            typeof(PlayerView).GetProperty(nameof(PlayerView.PotentialAbility))
        };

        private static List<PropertyInfo> csv_positions_order = new List<PropertyInfo>()
        {
            typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.GK)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.SW)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.DF)), 
            typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.DM)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.WingBack)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.MF)), 
            typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.AM)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.ST)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.FreeRole)), 
            typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.Right)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.Centre)), typeof(PlayerPositionView).GetProperty(nameof(PlayerView.Positions.Left))
        };

        private static List<PropertyInfo> csv_fitness_attributes_order = new List<PropertyInfo>()
        {
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Acceleration)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Agility)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Jumping)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.NaturalFitness)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Pace)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Reflexes)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Stamina)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Strength))
        };

        private static List<PropertyInfo> csv_skill_attributes_order = new List<PropertyInfo>()
        {
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Corners)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Crossing)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Dribbling)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.FreeKicks)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Heading)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Passing)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Tackling)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Technique)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.ThrowIns))
        };

        private static List<PropertyInfo> csv_tactic_attributes_order = new List<PropertyInfo>()
        {
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Decisions)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Marking)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.OffTheBall)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Positioning)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Teamwork))
        };

        private static List<PropertyInfo> csv_shooting_attributes_order = new List<PropertyInfo>()
        {
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Finishing)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.LongShots)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Penalties)),
        };

        private static List<PropertyInfo> csv_goalkeeping_attributes_order = new List<PropertyInfo>()
        {
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Handling)),
        };

        private static List<PropertyInfo> csv_experience_attributes_order = new List<PropertyInfo>()
        {
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Anticipation)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Consistency)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Creativity)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Influence)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Versatility))
        };

        private static List<PropertyInfo> csv_other_attributes_order = new List<PropertyInfo>()
        {
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Adaptability)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Aggression)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Ambition)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Balance)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Bravery)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Determination)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Dirtiness)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Flair)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.ImportantMatches)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.InjuryProneness)), 
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Loyalty)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.OneOnOnes)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Pressure)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Professionalism)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Sportsmanship)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.Temperament)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.WorkRate))
        };

        private static List<PropertyInfo> csv_foot_attributes_order = new List<PropertyInfo>()
        {
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.LeftFoot)),
            typeof(PlayerAttributeView).GetProperty(nameof(PlayerView.Attributes.RightFoot))
        };

        private SaveGameData _savegame;
        private PlayerDisplayHelper _displayHelper;
        private IPlayerRater _rater;
        private List<PlayerShortlistEntry> _shortlist = new List<PlayerShortlistEntry>();

        public DateTime GameDate;

        public CMScouterUI(string fileName, decimal valueMultiplier, string weightFileName, Guid weightingSelected)
        {
            // IntrinsicMasker = new DefaultIntrinsicMasker();
            IntrinsicMasker = new MadScientist_MatchMasker();

            LoadGameData(fileName, valueMultiplier);

            WeightCollection collection;

            if (weightFileName != null)
            {
                using (StreamReader sw = new StreamReader(weightFileName))
                {
                    try
                    {
                        collection = JsonSerializer.Deserialize<WeightCollection>(sw.ReadToEnd());
                    }
                    catch
                    {
                        collection = new WeightCollection();
                        collection.GroupedWeights.Add(new GroupedAttributeRater(IntrinsicMasker, null).DefaultGroupedWeights());
                        collection.IndividualWeights.Add(new IndividualAttributeRater(IntrinsicMasker, null).GetDefaultWeights());
                    }
                }

                IndividualWeightSet selectedIndividualWeights = collection.IndividualWeights.FirstOrDefault(x => x.ID == weightingSelected);
                GroupedWeightSet selectedGroupWeights = null;
                if (selectedIndividualWeights != null)
                {
                    _rater = new IndividualAttributeRater(IntrinsicMasker, selectedIndividualWeights);
                }
                else
                {
                    selectedGroupWeights = collection.GroupedWeights.FirstOrDefault(x => x.ID == weightingSelected);
                    if (selectedGroupWeights == null)
                    {
                        selectedGroupWeights = collection.GroupedWeights.First();
                    }

                    if (selectedGroupWeights == null)
                    {
                        return;
                    }

                    _rater = new GroupedAttributeRater(IntrinsicMasker, selectedGroupWeights);
                }

                SetupCustomSearches();
            }
        }

        public List<string> GetLoadingFailures()
        {
            return _savegame.LoadingFailures;
        }

        private void LoadGameData(string fileName, decimal valueMultiplier)
        {
            SaveGameData file = FileFunctions.LoadSaveGameFile(fileName, valueMultiplier);

            _savegame = file;

            if (_savegame.LoadingFailures.Any())
            {
                return;
            }

            GameDate = _savegame.GameDate;

            ConstructLookups();
        }

        public bool HasShortlistData()
        {
            if (_shortlist != null &&  _shortlist.Count > 0)
            {
                return true;
            }

            return false;
        }

        public void SavePlayersAsShortlist(string filePath, out string failureMessage)
        {
            failureMessage = null;
            if (_shortlist != null && _shortlist.Count > 0)
                FileFunctions.WriteShortlistFile(filePath, _shortlist, out failureMessage);
        }

        public IIntrinsicMasker IntrinsicMasker { get; internal set; }

        public List<Tuple<string, string>> GetCustomSearchList()
        {
            return customSearches.Select(x => new Tuple<string, string>(x.SearchId, x.SearchName)).ToList();
        }

        private void SetupCustomSearches()
        {
            customSearches.Add(new CustomSearch() { SearchName = "Highest CA", SearchId = "CA", SearchMethod = this.GetHighestCA() });
            customSearches.Add(new CustomSearch() { SearchName = "Highest PA", SearchId = "PA", SearchMethod = this.GetHighestPA() });
            customSearches.Add(new CustomSearch() { SearchName = "Unfulfilled Potential", SearchId = "YG", SearchMethod = this.GetHighestUnfulfilledPotential() });
            customSearches.Add(new CustomSearch() { SearchName = "Highest Finishing", SearchId = "HF", SearchMethod = this.GetHighestFinishing() });
            customSearches.Add(new CustomSearch() { SearchName = "Highest Off The Ball & Finishing", SearchId = "OF", SearchMethod = this.GetHighestOffTheBallAndFinishing() });
            customSearches.Add(new CustomSearch() { SearchName = "Highest Mark, Position and Tackle", SearchId = "DF", SearchMethod = this.GetHighestMarkingPositioningAndTackling() });
            customSearches.Add(new CustomSearch() { SearchName = "Lazy Centre Back Ratings", SearchId = "CB", SearchMethod = this.GetLazyCentreBackRating() });
            customSearches.Add(new CustomSearch() { SearchName = "Lazy Full Back Ratings", SearchId = "FB", SearchMethod = this.GetLazyFullBackRating() });
            customSearches.Add(new CustomSearch() { SearchName = "Highest Agility, OffBall and Pass", SearchId = "PS", SearchMethod = this.GetHighestAgilityOffBallAndPassing() });
            customSearches.Add(new CustomSearch() { SearchName = "Highest Creative, Pass and Technique", SearchId = "PL", SearchMethod = this.GetHighestCreativityPassingAndTechnique() });
        }

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

        public PlayerView GetPlayerByPlayerId(int playerId)
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => playerId == x._player.PlayerId);
            return ConstructPlayerByFilter(filter).FirstOrDefault();
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

            if (!string.IsNullOrEmpty(request.CustomSearch))
            {
                var resultset = new List<PlayerView>();
                switch (request.CustomSearch.ToUpper())
                {
                    case "CA":
                        resultset = GetHighestCA();
                        break;
                    case "PA":
                        resultset = GetHighestPA();
                        break;
                    case "YG":
                        resultset = GetHighestUnfulfilledPotential();
                        break;
                    case "HF":
                        resultset = GetHighestFinishing();
                        break;
                    case "OF":
                        resultset = GetHighestOffTheBallAndFinishing();
                        break;
                    case "DF":
                        resultset = GetHighestMarkingPositioningAndTackling();
                        break;
                    case "CB":
                        resultset = GetLazyCentreBackRating();
                        break;
                    case "FB":
                        resultset = GetLazyFullBackRating();
                        break;
                    case "PS":
                        resultset = GetHighestAgilityOffBallAndPassing();
                        break;
                    case "PL":
                        resultset = GetHighestCreativityPassingAndTechnique();
                        break;
                    default:
                        break;
                }

                CreateShortlistData(resultset.Select(p => p.ShortlistData));

                return resultset;
            }

            if (filters.Count == 0)
            {
                filterHelper.CreateTextFilter(request, filters);
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

        public PlayerView GetPlayerPotential(int playerId)
        {
            ConstructPlayerOptions options = new ConstructPlayerOptions() { UsePotential = true };
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
            csv.Append(string.Join(",", csv_fitness_attributes_order.Select(x => x.Name)) + ",");
            csv.Append(string.Join(",", csv_skill_attributes_order.Select(x => x.Name)) + ",");
            csv.Append(string.Join(",", csv_tactic_attributes_order.Select(x => x.Name)) + ",");
            csv.Append(string.Join(",", csv_shooting_attributes_order.Select(x => x.Name)) + ",");
            csv.Append(string.Join(",", csv_goalkeeping_attributes_order.Select(x => x.Name)) + ",");
            csv.Append(string.Join(",", csv_experience_attributes_order.Select(x => x.Name)) + ",");
            csv.Append(string.Join(",", csv_other_attributes_order.Select(x => x.Name)) + ",");
            csv.Append(string.Join(",", csv_foot_attributes_order.Select(x => x.Name)) + ",");
            csv.Append(string.Join(",", csv_positions_order.Select(x => x.Name)));
            csv.Append(Environment.NewLine);

            foreach (var player in players)
            {
                csv.Append(player.CreateCSVTextFromPlayer(csv_order));
                csv.Append(player.CreateCSVTextFromAttributes(
                    csv_fitness_attributes_order,
                    csv_skill_attributes_order,
                    csv_tactic_attributes_order,
                    csv_shooting_attributes_order,
                    csv_goalkeeping_attributes_order,
                    csv_experience_attributes_order,
                    csv_other_attributes_order,
                    csv_foot_attributes_order));
                csv.Append(player.CreateCSVTextFromPositions(csv_positions_order) + Environment.NewLine);
                
                // Append blank line to enable comparison with another file
                csv.Append(Environment.NewLine);
            }

            return csv.ToString();
        }

        public void UpdateInflationValue(decimal inflation)
        {
            LoadGameData(_savegame.FileName, inflation);
        }

        public void UpdateGroupedWeighting(GroupedWeightSet weights)
        {
            _rater = new GroupedAttributeRater(IntrinsicMasker, weights);
            LoadGameData(_savegame.FileName, _savegame.ValueMultiplier);
        }

        public void UpdateIndividualWeighting(IndividualWeightSet weights)
        {
            _rater = new IndividualAttributeRater(IntrinsicMasker, weights);
            LoadGameData(_savegame.FileName, _savegame.ValueMultiplier);
        }

        public List<PlayerView> GetHighestCA()
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => x._player.CurrentAbility > 170);
            return ConstructPlayerByFilter(filter).OrderByDescending(x => x.CurrentAbility).ToList();
        }

        public List<PlayerView> GetHighestPA()
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => x._player.PotentialAbility > 170);
            return ConstructPlayerByFilter(filter).OrderByDescending(x => x.PotentialAbility).ToList();
        }

        public List<PlayerView> GetHighestUnfulfilledPotential()
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => x._player.PotentialAbility > 150 && GetAge(x._staff.DOB) <= 24);
            return ConstructPlayerByFilter(filter).OrderByDescending(x => x.ScoutRatings.BestPosition.BestRole.PotentialRating -  x.ScoutRatings.BestPosition.BestRole.AbilityRating).ToList();
        }

        public List<PlayerView> GetHighestFinishing()
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => IntrinsicMasker.GetIntrinsicBasicMask(x._player.Finishing, x._player.CurrentAbility) > 20);
            return ConstructPlayerByFilter(filter).OrderByDescending(x => IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Finishing, x.CurrentAbility)).ToList();
        }

        public List<PlayerView> GetHighestOffTheBallAndFinishing()
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => x._player.Finishing > 160 && x._player.OffTheBall > 160);
            return ConstructPlayerByFilter(filter).OrderByDescending(x => 
                IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Finishing, x.CurrentAbility) 
                + IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.OffTheBall, x.CurrentAbility)
                ).ToList();
        }

        public List<PlayerView> GetHighestMarkingPositioningAndTackling()
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => x._player.Marking > 150 && x._player.Positioning > 150 && x._player.Tackling > 150);
            return ConstructPlayerByFilter(filter).OrderByDescending(x =>
                IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Marking, x.CurrentAbility)
                + IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Positioning, x.CurrentAbility)
                + IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Tackling, x.CurrentAbility)
                ).ToList();
        }

        public List<PlayerView> GetLazyCentreBackRating()
        {
            Func<Player, bool> filter = new Func<Player, bool>(x =>
                IntrinsicMasker.GetIntrinsicBasicMask(x._player.Marking, x._player.CurrentAbility) > 16 
                && IntrinsicMasker.GetIntrinsicBasicMask(x._player.Positioning, x._player.CurrentAbility) > 16 
                && IntrinsicMasker.GetIntrinsicBasicMask(x._player.Tackling, x._player.CurrentAbility) > 16 
                && x._player.Jumping > 12 
                && x._player.Strength > 12);
            return ConstructPlayerByFilter(filter).OrderByDescending(x =>
                IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Marking, x.CurrentAbility)
                + IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Positioning, x.CurrentAbility)
                + IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Tackling, x.CurrentAbility)
                + x.Attributes.Jumping
                + x.Attributes.Strength
                ).ToList();
        }

        public List<PlayerView> GetLazyFullBackRating()
        {
            Func<Player, bool> filter = new Func<Player, bool>(x =>
                IntrinsicMasker.GetIntrinsicBasicMask(x._player.Positioning, x._player.CurrentAbility) > 16 
                && IntrinsicMasker.GetIntrinsicBasicMask(x._player.Tackling, x._player.CurrentAbility) > 16 
                && x._player.Acceleration > 12 
                && x._player.Pace > 12);
            return ConstructPlayerByFilter(filter).OrderByDescending(x =>
                + IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Positioning, x.CurrentAbility)
                + IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Tackling, x.CurrentAbility)
                + x.Attributes.Pace
                + x.Attributes.Acceleration
                ).ToList();
        }

        public List<PlayerView> GetHighestCreativityPassingAndTechnique()
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => x._player.Creativity > 150 && x._player.Passing > 150 && x._player.Technique > 13);
            return ConstructPlayerByFilter(filter).OrderByDescending(x =>
                IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Creativity, x.CurrentAbility)
                + IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Passing, x.CurrentAbility)
                + x.Attributes.Technique
                ).ToList();
        }

        public List<PlayerView> GetHighestAgilityOffBallAndPassing()
        {
            Func<Player, bool> filter = new Func<Player, bool>(x => x._player.Agility > 13 && x._player.OffTheBall > 150 && x._player.Passing > 150);
            return ConstructPlayerByFilter(filter).OrderByDescending(x =>
                x.Attributes.Agility
                + IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.OffTheBall, x.CurrentAbility)
                + IntrinsicMasker.GetIntrinsicBasicMask(x.Attributes.Passing, x.CurrentAbility)
                ).ToList();
        }

        private byte GetAge(DateTime date)
        {
            var age = _savegame.GameDate.Year - date.Year;

            // leap years
            if (date.Date > _savegame.GameDate.AddYears(-age)) age--;

            return (byte)Math.Min(byte.MaxValue, age);
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

            var filteredPlayers = specificPlayerList.Where(x => filter(x));

            return filteredPlayers;
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
            var results = scoutOrder.Take(numberOfResults).ToList();

            CreateShortlistData(results.Select(p => p.ShortlistData));

            return results;
        }

        private void CreateShortlistData(IEnumerable<PlayerShortlistEntry> players)
        {
            _shortlist = players.ToList();
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
                cumulativePercent += scoreDistribution.ContainsKey(i) ? (byte)Math.Round(scoreDistribution[i] / (decimal)list.Count() * 100) : (byte)0;
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
