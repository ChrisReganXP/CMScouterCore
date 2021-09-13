using CMScouter.DataClasses;
using CMScouter.DataContracts;
using CMScouter.UI.Converters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMScouter.UI
{
    internal class PlayerDisplayHelper
    {
        private Lookups _lookups;
        private IPlayerRater _rater;
        private DateTime _gamedate;

        public PlayerDisplayHelper(Lookups lookups, DateTime gameDate)
        {
            _lookups = lookups;
            _gamedate = gameDate;
        }

        public IEnumerable<PlayerView> ConstructPlayers(IEnumerable<Player> players, IPlayerRater rater, ConstructPlayerOptions options = null)
        {
            _rater = rater;

            foreach (var player in players)
            {
                yield return ConstructPlayer(player, options);
            }
        }

        private byte GetAge(DateTime date)
        {
            var age = _gamedate.Year - date.Year;

            // partial years
            if (date.Date > _gamedate.AddYears(-age)) age--;

            return (byte)Math.Min(byte.MaxValue, age);
        }

        public PlayerView ConstructPlayer(Player item, ConstructPlayerOptions options = null)
        {
            return new PlayerView()
            {
                PlayerId = item._player.PlayerId,
                FirstName = GetLookupString(item._staff.FirstNameId, _lookups.firstNames),
                SecondName = GetLookupString(item._staff.SecondNameId, _lookups.secondNames),
                CommonName = item._staff.CommonNameId > 0 ? GetLookupString(item._staff.CommonNameId, _lookups.commonNames) : null,
                Nationality = GetLookupString<NationView>(item._staff.NationId, _lookups.nations)?.Name,
                SecondaryNationality = GetLookupString<NationView>(item._staff.SecondaryNationId, _lookups.nations)?.Name,
                Age = GetAge(item._staff.DOB),
                Value = item._contract == null ? 0 : item._staff.Value,
                WagePerWeek = item._contract == null ? 0 : item._contract?.WagePerWeek ?? item._staff.Wage,
                ContractExpiryDate = item._staff.ContractExpiryDate,

                ClubName = item._staff.ClubId == -1 ? string.Empty : GetLookupString(item._staff.ClubId, _lookups.clubNames),

                CurrentAbility = item._player.CurrentAbility,
                PotentialAbility = item._player.PotentialAbility,

                ReputationDescription = ReputationConverter.Convert(item._player.Reputation),
                DomesticReputation = item._player.DomesticReputation,
                WorldReputation = item._player.WorldReputation,

                Contract = item._contract == null ? new ContractView() : new ContractView()
                {
                    ReleaseValue = item._contract.ReleaseClauseValue,
                    ReleaseClause = item._contract.MinimumFeeReleaseClause ? ReleaseClauseType.MinimumFee :
                                        item._contract.ManagerReleaseClause ? ReleaseClauseType.ManagerJob :
                                        item._contract.NonPromotionReleaseClause ? ReleaseClauseType.NonPromotion :
                                        item._contract.RelegationReleaseClause ? ReleaseClauseType.Relegation :
                                        item._contract.NonPlayingReleaseClause ? ReleaseClauseType.NotPlaying :
                                        ReleaseClauseType.None,
                    SquadStatus = SquadStatusConverter.ConvertSquadStatus(item._contract.SquadStatus, GetAge(item._staff.DOB)),
                    SquadStatusValue = item._contract.SquadStatus,
                    TransferStatus = ((TransferStatus)item._contract.TransferStatus).ToName(),
                },


                Positions = new PlayerPositionView()
                {
                    GK = item._player.GK,
                    SW = item._player.SW,
                    DF = item._player.DF,
                    DM = item._player.DM,
                    MF = item._player.MF,
                    AM = item._player.AM,
                    ST = item._player.ST,
                    WingBack = item._player.WingBack,
                    FreeRole = item._player.FreeRole,

                    Left = item._player.Left,
                    Right = item._player.Right,
                    Centre = item._player.Centre,
                },

                Attributes = new PlayerAttributeView()
                {
                    Acceleration = item._player.Acceleration,
                    Agility = item._player.Agility,
                    Balance = item._player.Balance,
                    InjuryProneness = item._player.InjuryProneness,
                    Jumping = item._player.Jumping,
                    NaturalFitness = item._player.NaturalFitness,
                    Pace = item._player.Pace,
                    Stamina = item._player.Stamina,
                    Strength = item._player.Strength,

                    Anticipation = item._player.Anticipation,
                    Decisions = item._player.Decisions,
                    Heading = item._player.Heading,

                    Creativity = item._player.Creativity,
                    Crossing = item._player.Crossing,
                    Dribbling = item._player.Dribbling,
                    Finishing = item._player.Finishing,
                    Flair = item._player.Flair,
                    LongShots = item._player.LongShots,
                    OffTheBall = item._player.OffTheBall,
                    Passing = item._player.Passing,
                    Technique = item._player.Technique,

                    Marking = item._player.Marking,
                    Positioning = item._player.Positioning,
                    Tackling = item._player.Tackling,

                    Aggression = item._player.Aggression,
                    Bravery = item._player.Bravery,
                    Consistency = item._player.Consistency,
                    Dirtiness = item._player.Dirtiness,
                    ImportantMatches = item._player.ImportantMatches,
                    Influence = item._player.Influence,
                    Teamwork = item._player.Teamwork,
                    Versitility = item._player.Versatility,
                    WorkRate = item._player.WorkRate,

                    Handling = item._player.Handling,
                    OneOnOnes = item._player.OneOnOnes,
                    Reflexes = item._player.Reflexes,

                    Corners = item._player.Corners,
                    FreeKicks = item._player.FreeKicks,
                    Penalties = item._player.Penalties,
                    ThrowIns = item._player.ThrowIns,

                    LeftFoot = item._player.LeftFoot,
                    RightFoot = item._player.RightFoot,

                    Adaptability = item._staff.Adaptability,
                    Ambition = item._staff.Ambition,
                    Determination = item._staff.Determination,
                    Loyalty = item._staff.Loyalty,
                    Pressure = item._staff.Pressure,
                    Professionalism = item._staff.Professionalism,
                    Sportsmanship = item._staff.Sportsmanship,
                    Temperament = item._staff.Temperament,
                },

                ScoutRatings = options == null ? _rater.GetRatings(item) : _rater.GetRating(item, options),
            };
        }

        private static string GetLookupString(int key, Dictionary<int, string> lookup)
        {
            if (lookup.ContainsKey(key))
            {
                return lookup[key];
            }

            return null;
        }

        private static T GetLookupString<T>(int key, Dictionary<int, T> lookup) where T : class
        {
            if (key == -1)
            {
                return null;
            }

            return lookup[key];
        }
    }
}
