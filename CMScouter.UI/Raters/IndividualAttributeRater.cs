using CMScouter.DataClasses;
using CMScouter.DataContracts;
using CMScouter.UI.DataClasses;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI.Raters
{
    internal class IndividualAttributeRater : BaseRater, IPlayerRater
    {
        IndividualWeightSet weights;

        public IndividualAttributeRater(IIntrinsicMasker Masker, IndividualWeightSet Weights)
        {
            masker = Masker;
            weights = Weights;
        }

        public IndividualWeightSet GetDefaultWeights()
        {
            IndividualWeightSet weights = new IndividualWeightSet("Default");

            weights.GKWeights = new AttributeWeights()
            {
                Anticipation = 6,
                Decisions = 12,
                Positioning = 6,
                Handling = 255,
                OneonOnes = 36,
                Reflexes = 60,
                Acceleration = 3,
                Agility = 60,
                Jumping = 12,
                Strength = 1,
                Bravery = 6,
                Consistency = 150,
                Determination = 120,
                ImportantMatches = 150,
                Pressure = 150,
                Professionalism = 3,
                Temperament = 3
            };

            weights.RBWeights = new AttributeWeights()
            {
                Anticipation = 6,
                Decisions = 6,
                Heading = 1,
                Marking = 15,
                Positioning = 255,
                Tackling = 15,
                Jumping = 6,
                Pace = 3,
                Strength = 12,
                Aggression = 12,
                Bravery = 12,
                Consistency = 50,
                Determination = 12,
                ImportantMatches = 150,
                Pressure = 15
            };

            weights.CBWeights = new AttributeWeights()
            {
                Anticipation = 6,
                Decisions = 6,
                Heading = 1,
                Marking = 15,
                Positioning = 255,
                Tackling = 15,
                Jumping = 6,
                Pace = 3,
                Strength = 12,
                Aggression = 12,
                Bravery = 12,
                Consistency = 50,
                Determination = 12,
                ImportantMatches = 150,
                Pressure = 15
            };

            weights.LBWeights = new AttributeWeights()
            {
                Anticipation = 6,
                Decisions = 6,
                Heading = 1,
                Marking = 15,
                Positioning = 255,
                Tackling = 15,
                Jumping = 6,
                Pace = 3,
                Strength = 12,
                Aggression = 12,
                Bravery = 12,
                Consistency = 50,
                Determination = 12,
                ImportantMatches = 150,
                Pressure = 15
            };

            weights.WBWeights = new AttributeWeights()
            {
                Anticipation = 1,
                Creativity = 5,
                Crossing = 255,
                Decisions = 3,
                Dribbling = 20,
                LongShots = 1,
                Marking = 2,
                Passing = 20,
                Positioning = 255,
                Tackling = 7,
                Acceleration = 3,
                Pace = 20,
                Stamina = 1,
                Strength = 4,
                Technique = 1,
                WorkRate = 1,
                Aggression = 6,
                Bravery = 3,
                Consistency = 10,
                Determination = 1,
                ImportantMatches = 1,
            };

            weights.DMWeights = new AttributeWeights()
            {
                Anticipation = 3,
                Decisions = 3,
                Marking = 5,
                Passing = 5,
                Positioning = 100,
                Tackling = 15,
                Jumping = 2,
                Strength = 12,
                WorkRate = 1,
                Aggression = 36,
                Bravery = 6,
                Consistency = 5,
                Determination = 1,
                Pressure = 1
            };

            weights.CMWeights = new AttributeWeights()
            {
                Creativity = 3,
                Decisions = 25,
                Dribbling = 25,
                Finishing = 1,
                LongShots = 1,
                OffTheBall = 3,
                Passing = 255,
                Positioning = 3,
                Tackling = 1,
                Strength = 1,
                Technique = 50,
                Consistency = 1,
                ImportantMatches = 1
            };

            weights.RMWeights = new AttributeWeights()
            {
                Creativity = 3,
                Decisions = 25,
                Dribbling = 25,
                Finishing = 1,
                LongShots = 1,
                OffTheBall = 3,
                Passing = 255,
                Positioning = 3,
                Tackling = 1,
                Strength = 1,
                Technique = 50,
                Consistency = 1,
                ImportantMatches = 1
            };

            weights.LMWeights = new AttributeWeights()
            {
                Creativity = 3,
                Decisions = 25,
                Dribbling = 25,
                Finishing = 1,
                LongShots = 1,
                OffTheBall = 3,
                Passing = 255,
                Positioning = 3,
                Tackling = 1,
                Strength = 1,
                Technique = 50,
                Consistency = 1,
                ImportantMatches = 1
            };

            weights.AMWeights = new AttributeWeights()
            {
                Creativity = 255,
                Decisions = 1,
                Dribbling = 255,
                Finishing = 10,
                LongShots = 50,
                OffTheBall = 25,
                Passing = 50,
                Acceleration = 3,
                Flair = 10,
                Pace = 25,
                Technique = 10,
                Determination = 60
            };

            weights.LWWeights = new AttributeWeights()
            {
                Creativity = 255,
                Decisions = 1,
                Dribbling = 255,
                Finishing = 10,
                LongShots = 50,
                OffTheBall = 25,
                Passing = 50,
                Acceleration = 3,
                Flair = 10,
                Pace = 25,
                Technique = 10,
                Determination = 60
            };

            weights.RWWeights = new AttributeWeights()
            {
                Creativity = 255,
                Decisions = 1,
                Dribbling = 255,
                Finishing = 10,
                LongShots = 50,
                OffTheBall = 25,
                Passing = 50,
                Acceleration = 3,
                Flair = 10,
                Pace = 25,
                Technique = 10,
                Determination = 60
            };

            weights.CFWeights = new AttributeWeights()
            {
                Anticipation = 3,
                Dribbling = 50,
                Finishing = 255,
                Heading = 20,
                OffTheBall = 255,
                Acceleration = 1,
                Flair = 10,
                Jumping = 2,
                Pace = 10,
                Technique = 1,
                Consistency = 15,
                Determination = 30,
                ImportantMatches = 10,
                Pressure = 10,
            };

            return weights;
        }

        public ScoutingInformation GetRatings(Player item)
        {
            return GetRatings(item, null);
        }

        public ScoutingInformation GetRatings(Player player, ConstructPlayerOptions options)
        {
            List<PositionRating> positionRatings = new List<PositionRating>();

            if (options == null || options.setPosition == null || options.movementPosition == null)
            {
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.GoalKeeper, PlayerPosition.GoalKeeper));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.RightBack, PlayerPosition.RightBack));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.CentreHalf, PlayerPosition.CentreHalf));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.LeftBack, PlayerPosition.LeftBack));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.RightWingBack, PlayerPosition.RightWingBack));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.DefensiveMidfielder, PlayerPosition.DefensiveMidfielder));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.LeftWingBack, PlayerPosition.LeftWingBack));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.RightMidfielder, PlayerPosition.RightMidfielder));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.CentralMidfielder, PlayerPosition.CentralMidfielder));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.LeftMidfielder, PlayerPosition.LeftMidfielder));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.RightWinger, PlayerPosition.RightWinger));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.AttackingMidfielder, PlayerPosition.CentreForward));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.LeftWinger, PlayerPosition.LeftWinger));
                positionRatings.Add(GetRatingsForPosition(player, PlayerPosition.CentreForward, PlayerPosition.CentreForward));
            }
            else
            {
                positionRatings.Add(GetRatingsForPosition(player, options.setPosition.Value, options.movementPosition.Value));
            }

            ScoutingInformation coreResults = new ScoutingInformation(positionRatings, 0);

            return coreResults;
        }

        private PositionRating GetRatingsForPosition(Player player, PlayerPosition setPosition, PlayerPosition movementPosition)
        {
            PositionRating rating = new PositionRating(0) { SetPosition = setPosition, MovementPosition = movementPosition };

            switch (setPosition)
            {
                case PlayerPosition.GoalKeeper:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.GoalKeeper, Roles.GK, weights.GKWeights));
                    break;
                case PlayerPosition.RightBack:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.RightBack, Roles.DFB, weights.RBWeights));
                    break;
                case PlayerPosition.CentreHalf:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.CentreHalf, Roles.CB, weights.CBWeights));
                    break;
                case PlayerPosition.LeftBack:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.LeftBack, Roles.DFB, weights.LBWeights));
                    break;
                case PlayerPosition.RightWingBack:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.RightWingBack, Roles.WB, weights.WBWeights));
                    break;
                case PlayerPosition.DefensiveMidfielder:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.DefensiveMidfielder, Roles.HM, weights.DMWeights));
                    break;
                case PlayerPosition.LeftWingBack:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.LeftWingBack, Roles.WB, weights.WBWeights));
                    break;
                case PlayerPosition.RightMidfielder:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.RightMidfielder, Roles.WM, weights.RMWeights));
                    break;
                case PlayerPosition.CentralMidfielder:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.CentralMidfielder, Roles.CM, weights.CMWeights));
                    break;
                case PlayerPosition.LeftMidfielder:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.LeftMidfielder, Roles.WM, weights.LMWeights));
                    break;
                case PlayerPosition.RightWinger:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.RightWinger, Roles.WG, weights.RWWeights));
                    break;
                case PlayerPosition.AttackingMidfielder:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.AttackingMidfielder, Roles.AM, weights.AMWeights));
                    break;
                case PlayerPosition.LeftWinger:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.LeftWinger, Roles.WG, weights.LWWeights));
                    break;
                case PlayerPosition.CentreForward:
                    rating.RoleRatings.Add(GetRoleRating(player, PlayerPosition.CentreForward, Roles.ST, weights.CFWeights));
                    break;
            }

            return rating;
        }

        private RoleRating GetRoleRating(Player player, PlayerPosition position, Roles role, AttributeWeights weights)
        {
            RatingRoleDebug roleDebug = new RatingRoleDebug();
            var rating = CalculateRating(player, position, weights, ref roleDebug);

            return new RoleRating() { AbilityRating = rating, Role = role, Debug = roleDebug, };
        }

        private byte CalculateRating(Player player, PlayerPosition position, AttributeWeights weights, ref RatingRoleDebug debug)
        {
            decimal rating = 0;
            int combinedWeights = 0;

            ScoreAttribute(DP.Acceleration, player._player, position, player._player.Acceleration, weights.Acceleration, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Aggression, player._player, position, player._player.Aggression, weights.Aggression, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Agility, player._player, position, player._player.Agility, weights.Agility, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Anticipation, player._player, position, player._player.Anticipation, weights.Anticipation, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Balance, player._player, position, player._player.Balance, weights.Balance, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Bravery, player._player, position, player._player.Bravery, weights.Bravery, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Consistency, player._player, position, player._player.Consistency, weights.Consistency, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Creativity, player._player, position, player._player.Creativity, weights.Creativity, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Crossing, player._player, position, player._player.Crossing, weights.Crossing, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Decisions, player._player, position, player._player.Decisions, weights.Decisions, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Determination, player._player, position, player._staff.Determination, weights.Determination, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Dribbling, player._player, position, player._player.Dribbling, weights.Dribbling, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Finishing, player._player, position, player._player.Finishing, weights.Finishing, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Flair, player._player, position, player._player.Flair, weights.Flair, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Handling, player._player, position, player._player.Handling, weights.Handling, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Heading, player._player, position, player._player.Heading, weights.Heading, ref combinedWeights, ref rating);
            ScoreAttribute(DP.ImportantMatches, player._player, position, player._player.ImportantMatches, weights.ImportantMatches, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Influence, player._player, position, player._player.Influence, weights.Influence, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Jumping, player._player, position, player._player.Jumping, weights.Jumping, ref combinedWeights, ref rating);
            ScoreAttribute(DP.LongShots, player._player, position, player._player.LongShots, weights.LongShots, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Marking, player._player, position, player._player.Marking, weights.Marking, ref combinedWeights, ref rating);
            ScoreAttribute(DP.NaturalFitness, player._player, position, player._player.NaturalFitness, weights.NaturalFitness, ref combinedWeights, ref rating);
            ScoreAttribute(DP.OffTheBall, player._player, position, player._player.OffTheBall, weights.OffTheBall, ref combinedWeights, ref rating);
            ScoreAttribute(DP.OneOnOnes, player._player, position, player._player.OneOnOnes, weights.OneonOnes, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Pace, player._player, position, player._player.Pace, weights.Pace, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Passing, player._player, position, player._player.Passing, weights.Passing, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Positioning, player._player, position, player._player.Positioning, weights.Positioning, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Pressure, player._player, position, player._staff.Pressure, weights.Pressure, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Professionalism, player._player, position, player._staff.Professionalism, weights.Professionalism, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Reflexes, player._player, position, player._player.Reflexes, weights.Reflexes, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Stamina, player._player, position, player._player.Stamina, weights.Stamina, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Strength, player._player, position, player._player.Strength, weights.Strength, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Tackling, player._player, position, player._player.Tackling, weights.Tackling, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Teamwork, player._player, position, player._player.Teamwork, weights.Teamwork, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Technique, player._player, position, player._player.Technique, weights.Technique, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Temperament, player._player, position, player._staff.Temperament, weights.Temperament, ref combinedWeights, ref rating);
            ScoreAttribute(DP.Versatility, player._player, position, player._player.Versatility, weights.Versatility, ref combinedWeights, ref rating);
            ScoreAttribute(DP.WorkRate, player._player, position, player._player.WorkRate, weights.WorkRate, ref combinedWeights, ref rating);

            int maxScore = 20 * combinedWeights;

            decimal ratio = rating / maxScore;
            int easyratio = (int)(ratio * 100);

            return Math.Min((byte)99, (byte)easyratio);
        }

        private void ScoreAttribute(DP attribute, PlayerData player, PlayerPosition position, byte playerAttribute, byte weight, ref int combinedWeight, ref decimal rating)
        {
            if (weight == 0)
            {
                return;
            }

            combinedWeight += weight;
            var value = Adj(playerAttribute, IsAttributeIntrinsic(attribute), attribute, player, position, position);
            decimal weightedValue = value * weight;

            rating += weightedValue;
        }

        public void OutputDebug(bool enabled)
        {
        }
    }
}
