using CMScouter.UI.Raters;
using CMScouter.DataClasses;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using CMScouter.UI.DataClasses;
using CMScouter.DataContracts;

namespace CMScouter.UI
{
    internal class GroupedAttributeRater : BaseRater, IPlayerRater
    {
        public void OutputDebug(bool enabled)
        {
            outputDebug = enabled;
        }

        private bool outputDebug { get; set; }

        private void LogDebug(string debug)
        {
            if (!outputDebug)
            {
                return;
            }

            Debug.WriteLine(debug);
        }

        GroupedWeightSet weights;

        private AttributeWeights OffField { get => new AttributeWeights()
            {
                Determination = 20,
                Loyalty = 4,
                Pressure = 6,
                Professionalism = 4,
                Temperament = 3
            };
        }

        public GroupedWeightSet DefaultGroupedWeights()
        {
            GroupedWeightSet groupedWeightSet = new GroupedWeightSet("Default");

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.GK,
                ImpactPercent = 6,
                ReliabilityPercent = 14,
                PlaymakingPercent = 0,
                WideplayPercent = 0,
                ScoringPercent = 0,
                DefendingPercent = 0,
                GoalkeepingPercent = 70,
                SpeedPercent = 0,
                StrengthPercent = 10
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.DFB,
                ImpactPercent = 10,
                ReliabilityPercent = 10,
                PlaymakingPercent = 0,
                WideplayPercent = 10,
                ScoringPercent = 0,
                DefendingPercent = 45,
                GoalkeepingPercent = 0,
                SpeedPercent = 20,
                StrengthPercent = 5
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.AFB,
                ImpactPercent = 8,
                ReliabilityPercent = 8,
                PlaymakingPercent = 8,
                WideplayPercent = 15,
                ScoringPercent = 0,
                DefendingPercent = 15,
                GoalkeepingPercent = 0,
                SpeedPercent = 35,
                StrengthPercent = 5
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.CB,
                ImpactPercent = 15,
                ReliabilityPercent = 5,
                PlaymakingPercent = 0,
                WideplayPercent = 0,
                ScoringPercent = 0,
                DefendingPercent = 35,
                GoalkeepingPercent = 0,
                SpeedPercent = 15,
                StrengthPercent = 30
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.WB,
                ImpactPercent = 10,
                ReliabilityPercent = 10,
                PlaymakingPercent = 5,
                WideplayPercent = 15,
                ScoringPercent = 0,
                DefendingPercent = 25,
                GoalkeepingPercent = 0,
                SpeedPercent = 30,
                StrengthPercent = 5
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.HM,
                ImpactPercent = 15,
                ReliabilityPercent = 10,
                PlaymakingPercent = 10,
                WideplayPercent = 0,
                ScoringPercent = 0,
                DefendingPercent = 20,
                GoalkeepingPercent = 0,
                SpeedPercent = 15,
                StrengthPercent = 30
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.CM,
                ImpactPercent = 15,
                ReliabilityPercent = 15,
                PlaymakingPercent = 20,
                WideplayPercent = 5,
                ScoringPercent = 5,
                DefendingPercent = 10,
                GoalkeepingPercent = 0,
                SpeedPercent = 10,
                StrengthPercent = 20
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.WM,
                ImpactPercent = 5,
                ReliabilityPercent = 10,
                PlaymakingPercent = 15,
                WideplayPercent = 20,
                ScoringPercent = 5,
                DefendingPercent = 5,
                GoalkeepingPercent = 0,
                SpeedPercent = 35,
                StrengthPercent = 5
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.AM,
                ImpactPercent = 5,
                ReliabilityPercent = 10,
                PlaymakingPercent = 35,
                WideplayPercent = 20,
                ScoringPercent = 10,
                DefendingPercent = 0,
                GoalkeepingPercent = 0,
                SpeedPercent = 15,
                StrengthPercent = 5
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.WG,
                ImpactPercent = 5,
                ReliabilityPercent = 10,
                PlaymakingPercent = 10,
                WideplayPercent = 25,
                ScoringPercent = 15,
                DefendingPercent = 0,
                GoalkeepingPercent = 0,
                SpeedPercent = 35,
                StrengthPercent = 0
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.ST,
                ImpactPercent = 10,
                ReliabilityPercent = 10,
                PlaymakingPercent = 5,
                WideplayPercent = 0,
                ScoringPercent = 35,
                DefendingPercent = 0,
                GoalkeepingPercent = 0,
                SpeedPercent = 20,
                StrengthPercent = 20
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.PO,
                ImpactPercent = 10,
                ReliabilityPercent = 10,
                PlaymakingPercent = 0,
                WideplayPercent = 10,
                ScoringPercent = 45,
                DefendingPercent = 0,
                GoalkeepingPercent = 0,
                SpeedPercent = 20,
                StrengthPercent = 5
            });

            groupedWeightSet.RoleWeights.Add(new GroupedRoleWeights()
            {
                Role = Roles.TM,
                ImpactPercent = 15,
                ReliabilityPercent = 5,
                PlaymakingPercent = 5,
                WideplayPercent = 5,
                ScoringPercent = 30,
                DefendingPercent = 0,
                GoalkeepingPercent = 0,
                SpeedPercent = 10,
                StrengthPercent = 30
            });

            groupedWeightSet.ImpactWeights = new AttributeWeights()
            {
                Aggression = 10,
                Bravery = 10
            };

            groupedWeightSet.ReliabilityWeights = new AttributeWeights()
            {
                Consistency = 10,
                ImportantMatches = 4,
                Teamwork = 4,
                WorkRate = 4,
            };

            groupedWeightSet.PlaymakingWeights = new AttributeWeights()
            {
                Creativity = 4,
                LongShots = 1,
                Dribbling = 2,
                Passing = 10,
                Technique = 8,
                Anticipation = 4,
                Decisions = 2,
                OffTheBall = 8,
                Crossing = 2,
                Teamwork = 4,
                WorkRate = 3,
            };

            groupedWeightSet.WideplayWeights = new AttributeWeights()
            {
                Crossing = 10,
                Dribbling = 8,
                OffTheBall = 7,
                Passing = 4,
                Technique = 6,
                Flair = 4
            };

            groupedWeightSet.ScoringWeights = new AttributeWeights()
            {
                Finishing = 10,
                Heading = 10,
                LongShots = 1,
                OffTheBall = 20,
                Technique = 5,
                Flair = 2,
                Anticipation = 5
            };

            groupedWeightSet.DefendingWeights = new AttributeWeights()
            {
                Heading = 10,
                Marking = 15,
                Positioning = 30,
                Tackling = 30,
                Anticipation = 5,
            };

            groupedWeightSet.GoalkeepingWeights = new AttributeWeights()
            {
                Handling = 15,
                OneonOnes = 2,
                Reflexes = 5,
                Positioning = 5,
                Agility = 4,
            };

            groupedWeightSet.SpeedWeights = new AttributeWeights()
            {
                Acceleration = 10,
                Agility = 6,
                Pace = 10,
                Stamina = 3,
            };

            groupedWeightSet.StrengthWeights = new AttributeWeights()
            {
                Balance = 2,
                Jumping = 13,
                Stamina = 2,
                Strength = 10,
            };

            return groupedWeightSet;
        }

        public GroupedAttributeRater(IIntrinsicMasker Masker, GroupedWeightSet Weights)
        {
            base.masker = Masker;
            weights = Weights;
        }

        public ScoutingInformation GetRatings(Player player)
        {
            return GetRatings(player, null);
        }

        public ScoutingInformation GetRatings(Player player, ConstructPlayerOptions options)
        {
            byte offFieldRating = GetRatingsForPersonality(player);

            List<PositionRating> positionRatings = new List<PositionRating>();

            if (options == null || options.setPosition == null || options.movementPosition == null)
            {
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.GoalKeeper, PlayerPosition.GoalKeeper, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.RightBack, PlayerPosition.RightBack, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.CentreHalf, PlayerPosition.CentreHalf, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.LeftBack, PlayerPosition.LeftBack, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.RightWingBack, PlayerPosition.RightWingBack, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.DefensiveMidfielder, PlayerPosition.DefensiveMidfielder, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.LeftWingBack, PlayerPosition.LeftWingBack, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.RightMidfielder, PlayerPosition.RightMidfielder, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.CentralMidfielder, PlayerPosition.CentralMidfielder, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.LeftMidfielder, PlayerPosition.LeftMidfielder, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.RightWinger, PlayerPosition.RightWinger, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.AttackingMidfielder, PlayerPosition.CentreForward, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.LeftWinger, PlayerPosition.LeftWinger, offFieldRating));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.CentreForward, PlayerPosition.CentreForward, offFieldRating));
            }
            else
            {
                positionRatings.Add(GetRatingsForPosition(player, options.setPosition.Value, options.movementPosition.Value, offFieldRating));
            }

            ScoutingInformation coreResults = new ScoutingInformation(positionRatings, offFieldRating);

            return coreResults;
        }

        private PositionRating GetRatingsForPosition(Player player, PlayerPosition setPosition, PlayerPosition movementPosition, byte offFieldRating)
        {
            PositionRating positionRatings = new PositionRating(offField: offFieldRating) { SetPosition = setPosition, MovementPosition = movementPosition };
            GroupedRatings playerGroupedRatings = null;

            List<Roles> roles = setPosition.GetAttributeValue<LinkedRoles, List<Roles>>(x => x.Roles);
            foreach (var role in roles)
            {
                var rating = GetRatingForTypeAndRole(player, setPosition, movementPosition, role, ref playerGroupedRatings);
                rating.PurchaseRating = ApplyOffFieldAdjustment(rating.AbilityRating, offFieldRating, rating.Debug);
                positionRatings.RoleRatings.Add(rating);
            }

            return positionRatings;
        }

        private RoleRating GetRatingForTypeAndRole(Player player, PlayerPosition setPosition, PlayerPosition movementPosition, Roles role, ref GroupedRatings playerGroupedRatings)
        {
            RatingRoleDebug roleDebug = new RatingRoleDebug();

            var rating = CalculateRating(player, setPosition, movementPosition, role, ref playerGroupedRatings, ref roleDebug);

            return new RoleRating() { AbilityRating = rating, Role = role, Debug = roleDebug, };
        }

        private byte GetRatingsForPersonality(Player player)
        {
            byte offField = GetGroupingScore(player, PlayerPosition.GoalKeeper, PlayerPosition.GoalKeeper, OffField);

            return offField;
        }

        private byte CalculateRating(Player player, PlayerPosition setPosition, PlayerPosition movementPosition, Roles role, ref GroupedRatings playerGroupedRatings, ref RatingRoleDebug debug)
        {
            RatingRoleDebug roleDebug;

            LogDebug($"*** {role} ***");

            playerGroupedRatings = playerGroupedRatings ?? CreatePlayerGroupedRatings(player, setPosition, movementPosition);

            decimal result = RatePlayerInRole(player, setPosition, role, GetWeightsForRole(role), playerGroupedRatings, out roleDebug);

            var proportion = result;

            debug = roleDebug;

            return (byte)proportion;
        }

        private GroupedRoleWeights GetWeightsForRole(Roles role)
        {
            return weights.RoleWeights.FirstOrDefault(x => x.Role == role);
        }

        private GroupedRatings CreatePlayerGroupedRatings(Player player, PlayerPosition setPosition, PlayerPosition movementPosition)
        {
            var playerGroupedRatings = new GroupedRatings();

            if (setPosition == movementPosition)
            {
                LogDebug($"{setPosition}");
            }
            else
            {
                LogDebug($"{setPosition} -> {movementPosition}");
            }

            playerGroupedRatings.impactRating = GetGroupingScore(player, setPosition, movementPosition, weights.ImpactWeights);
            playerGroupedRatings.reliabilityRating = GetGroupingScore(player, setPosition, movementPosition, weights.ReliabilityWeights);
            playerGroupedRatings.playmakingRating = GetGroupingScore(player, setPosition, movementPosition, weights.PlaymakingWeights);
            playerGroupedRatings.wideplayRating = GetGroupingScore(player, setPosition, movementPosition, weights.WideplayWeights);
            playerGroupedRatings.scoringRating = GetGroupingScore(player, setPosition, movementPosition, weights.ScoringWeights);
            playerGroupedRatings.defendingRating = GetGroupingScore(player, setPosition, movementPosition, weights.DefendingWeights);
            playerGroupedRatings.goalkeepingRating = GetGroupingScore(player, setPosition, movementPosition, weights.GoalkeepingWeights);
            playerGroupedRatings.speedRating = GetGroupingScore(player, setPosition, movementPosition, weights.SpeedWeights);
            playerGroupedRatings.strengthRating = GetGroupingScore(player, setPosition, movementPosition, weights.StrengthWeights);


            return playerGroupedRatings;
        }

        private byte RatePlayerInRole(Player player, PlayerPosition type, Roles role, GroupedRoleWeights weights, GroupedRatings playerGroupedRatings, out RatingRoleDebug debug)
        {
            debug = new RatingRoleDebug();
            if (weights == null)
            {
                return (byte)0;
            }

            decimal mentalRating = ApplyWeightToGroup(playerGroupedRatings.impactRating, weights.ImpactPercent) + ApplyWeightToGroup(playerGroupedRatings.reliabilityRating, weights.ReliabilityPercent);
            decimal mentalWeighting = weights.ImpactPercent + weights.ReliabilityPercent;

            decimal technicalRating = ApplyWeightToGroup(playerGroupedRatings.playmakingRating, weights.PlaymakingPercent)
                + ApplyWeightToGroup(playerGroupedRatings.wideplayRating, weights.WideplayPercent)
                + ApplyWeightToGroup(playerGroupedRatings.scoringRating, weights.ScoringPercent) + ApplyWeightToGroup(playerGroupedRatings.defendingRating, weights.DefendingPercent)
                + ApplyWeightToGroup(playerGroupedRatings.goalkeepingRating, weights.GoalkeepingPercent);
            decimal technicalWeighting = weights.PlaymakingPercent + weights.WideplayPercent + weights.ScoringPercent + weights.DefendingPercent + weights.GoalkeepingPercent;

            decimal physicalRating = ApplyWeightToGroup(playerGroupedRatings.speedRating, weights.SpeedPercent) + ApplyWeightToGroup(playerGroupedRatings.strengthRating, weights.StrengthPercent);
            decimal physicalWeighting = weights.SpeedPercent + weights.StrengthPercent;


            decimal rating = mentalRating + technicalRating + physicalRating;
            rating = Math.Min(99, rating);

            debug = new RatingRoleDebug()
            {
                Position = type.ToString(),
                Role = role,
                Mental = $"{mentalRating} / {mentalWeighting} ({(100 * mentalRating / mentalWeighting).ToString("N0")})",
                MentalDetail = $"{mentalRating} / {mentalWeighting} ({(100 * mentalRating / mentalWeighting).ToString("N0")})",
                Physical = $"{physicalRating} / {physicalWeighting} ({(100 * physicalRating / physicalWeighting).ToString("N0")})",
                PhysicalDetail = $"{physicalRating} / {physicalWeighting} ({(100 * physicalRating / physicalWeighting).ToString("N0")})",
                Technical = $"{technicalRating} / {technicalWeighting} ({(100 * technicalRating / technicalWeighting).ToString("N0")})",
                TechnicalDetail = $"{technicalRating} / {technicalWeighting} ({(100 * technicalRating / technicalWeighting).ToString("N0")})",
            };

            return (byte)rating;
        }

        private byte ApplyWeightToGroup(byte groupRating, byte weight)
        {
            return (byte)Math.Min(99, Math.Round((decimal)groupRating / 100 * weight));
        }

        private byte ApplyOffFieldAdjustment(byte unadjustedScore, byte offFieldScore, RatingRoleDebug debug)
        {
            decimal adjuster;

            if (offFieldScore > 50)
            {
                adjuster = (offFieldScore - 50) * 0.0024M;
                decimal diff = 100 - unadjustedScore;
                decimal increase = diff * adjuster;
                var decScore = unadjustedScore + increase;
                debug.OffField = $"+{increase}";
                return (byte)Math.Min((decimal)byte.MaxValue, decScore);
            }

            if (offFieldScore < 50)
            {
                adjuster = 1 - (0.2M - ((decimal)(offFieldScore / 2)) / 100);
                debug.OffField = $"*{adjuster}";
                return (byte)Math.Max(1, unadjustedScore * adjuster);
            }

            return unadjustedScore;
        }

        private byte GetGroupingScore(Player player, PlayerPosition setPosition, PlayerPosition movementPosition, AttributeWeights weights)
        {
            decimal rating = 0;
            int combinedWeights = 0;

            ScoreAttribute(DP.Acceleration, player._player, setPosition, movementPosition, player._player.Acceleration, weights.Acceleration, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Aggression, player._player, setPosition, movementPosition, player._player.Aggression, weights.Aggression, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Agility, player._player, setPosition, movementPosition, player._player.Agility, weights.Agility, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Anticipation, player._player, setPosition, movementPosition, player._player.Anticipation, weights.Anticipation, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Balance, player._player, setPosition, movementPosition, player._player.Balance, weights.Balance, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Bravery, player._player, setPosition, movementPosition, player._player.Bravery, weights.Bravery, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Consistency, player._player, setPosition, movementPosition, player._player.Consistency, weights.Consistency, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Creativity, player._player, setPosition, movementPosition, player._player.Creativity, weights.Creativity, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Crossing, player._player, setPosition, movementPosition, player._player.Crossing, weights.Crossing, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Decisions, player._player, setPosition, movementPosition, player._player.Decisions, weights.Decisions, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Determination, player._player, setPosition, movementPosition, player._staff.Determination, weights.Determination, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Dribbling, player._player, setPosition, movementPosition, player._player.Dribbling, weights.Dribbling, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Finishing, player._player, setPosition, movementPosition, player._player.Finishing, weights.Finishing, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Flair, player._player, setPosition, movementPosition, player._player.Flair, weights.Flair, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Handling, player._player, setPosition, movementPosition, player._player.Handling, weights.Handling, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Heading, player._player, setPosition, movementPosition, player._player.Heading, weights.Heading, ref combinedWeights, ref rating);
            ScoreAttribute(DP.ImportantMatches, player._player, setPosition, movementPosition, player._player.ImportantMatches, weights.ImportantMatches, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Influence, player._player, setPosition, movementPosition, player._player.Influence, weights.Influence, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Jumping, player._player, setPosition, movementPosition, player._player.Jumping, weights.Jumping, ref combinedWeights, ref rating);
            ScoreAttribute(DP.LongShots, player._player, setPosition, movementPosition, player._player.LongShots, weights.LongShots, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Marking, player._player, setPosition, movementPosition, player._player.Marking, weights.Marking, ref combinedWeights, ref rating);
            ScoreAttribute(DP.NaturalFitness, player._player, setPosition, movementPosition, player._player.NaturalFitness, weights.NaturalFitness, ref combinedWeights, ref rating);
            ScoreAttribute(DP.OffTheBall, player._player, setPosition, movementPosition, player._player.OffTheBall, weights.OffTheBall, ref combinedWeights, ref rating);
            ScoreAttribute(DP.OneOnOnes, player._player, setPosition, movementPosition, player._player.OneOnOnes, weights.OneonOnes, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Pace, player._player, setPosition, movementPosition, player._player.Pace, weights.Pace, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Passing, player._player, setPosition, movementPosition, player._player.Passing, weights.Passing, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Positioning, player._player, setPosition, movementPosition, player._player.Positioning, weights.Positioning, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Pressure, player._player, setPosition, movementPosition, player._staff.Pressure, weights.Pressure, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Professionalism, player._player, setPosition, movementPosition, player._staff.Professionalism, weights.Professionalism, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Reflexes, player._player, setPosition, movementPosition, player._player.Reflexes, weights.Reflexes, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Stamina, player._player, setPosition, movementPosition, player._player.Stamina, weights.Stamina, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Strength, player._player, setPosition, movementPosition, player._player.Strength, weights.Strength, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Tackling, player._player, setPosition, movementPosition, player._player.Tackling, weights.Tackling, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Teamwork, player._player, setPosition, movementPosition, player._player.Teamwork, weights.Teamwork, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Technique, player._player, setPosition, movementPosition, player._player.Technique, weights.Technique, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Temperament, player._player, setPosition, movementPosition, player._staff.Temperament, weights.Temperament, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Versatility, player._player, setPosition, movementPosition, player._player.Versatility, weights.Versatility, ref combinedWeights, ref rating);
            ScoreAttribute(DP.WorkRate, player._player, setPosition, movementPosition, player._player.WorkRate, weights.WorkRate, ref combinedWeights, ref rating);

            int maxScore = 20 * combinedWeights;

            decimal ratio = rating / maxScore;
            int easyratio = (int)(ratio * 100);

            return Math.Min((byte)99, (byte)easyratio);
        }
    }
}