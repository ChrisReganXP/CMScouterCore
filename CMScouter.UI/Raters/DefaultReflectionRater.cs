using CMScouter.UI.Raters;
using CMScouter.DataClasses;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CMScouter.UI
{
    internal class DefaultReflectionRater : IPlayerRater
    {
        /*
            GK: Anticipation, Decisions, Handling, Heading, Positioning, Reflexes, Tackling, One on Ones
            Full backs: Anticipation, Crossing, Decisions, Dribbling, Marking, Positioning, Tackling, Team work
            Centre Backs: Anticipation, Decisions, Heading, Marking, Positioning, Tackling, Strength
            Wingers: Agility, Balance, Crossing, Dribbling, Flair(If you please), Set pieces(If you please), Passing, Teamwork
            Centre Mids: Have pretty much everything, but can vary if you want a more defensive or more attacking player
            Strikers: Decisions, Finishing, Heading (depending on the striker), Movement (Depending on the striker) Natural Fitness, Strength (depending on the striker).
        */

        byte[][] weightings;

        private List<PropertyInfo> MentalAttributes = typeof(PlayerData).GetProperties().Where(x => CheckAttributeGroup(x, AttributeGroup.Mental)).ToList();
        private List<PropertyInfo> PhysicalAttributes = typeof(PlayerData).GetProperties().Where(x => CheckAttributeGroup(x, AttributeGroup.Physical)).ToList();
        private List<PropertyInfo> TechnicalAttributes = typeof(PlayerData).GetProperties().Where(x => CheckAttributeGroup(x, AttributeGroup.Technical)).ToList();
        private List<PropertyInfo> OffFieldAttributes = typeof(Staff).GetProperties().Where(x => CheckAttributeGroup(x, AttributeGroup.OffField)).ToList();

        private byte[] MentalWeights = new byte[] {  (byte)DP.Aggression, (byte)DP.Bravery, (byte)DP.Consistency, (byte)DP.ImportantMatches, (byte)DP.Influence, (byte)DP.Teamwork, (byte)DP.WorkRate};
        private byte[] PhysicalWeights = new byte[] { (byte)DP.Acceleration, (byte)DP.Agility, (byte)DP.Balance, (byte)DP.Jumping, (byte)DP.Pace, (byte)DP.Stamina, (byte)DP.Strength };
        private byte[] TechnicalWeights = new byte[] { (byte)DP.Anticipation, (byte)DP.Creativity, (byte)DP.Crossing, (byte)DP.Decisions, (byte)DP.Dribbling, (byte)DP.Handling, (byte)DP.LongShots, (byte)DP.Marking, (byte)DP.OffTheBall, (byte)DP.OneOnOnes, (byte)DP.Passing, (byte)DP.Positioning, (byte)DP.Reflexes, (byte)DP.Tackling };

        private IIntrinsicMasker masker = null;

        public DefaultReflectionRater()
        {
            // last one is the off field
            weightings = new byte[Enum.GetNames(typeof(Roles)).Count() + 1][];
            AddGK();
            AddDFB();
            AddAFB();
            AddCB();
            AddDM();
            AddWB();
            AddCM();
            AddWM();
            AddAM();
            AddWG();
            AddTM();
            AddST();
            AddOffField();
        }

        public bool PlaysPosition(PlayerType type, PlayerData player)
        {
            switch (type)
            {
                case PlayerType.GoalKeeper:
                    return player.GK >= 19;

                case PlayerType.RightBack:
                    return player.DF >= 15 && player.Right >= 15;

                case PlayerType.CentreHalf:
                    return player.DF >= 15 && player.Centre >= 15;

                case PlayerType.LeftBack:
                    return player.DF >= 15 && player.Left >= 15;

                case PlayerType.RightWingBack:
                    return (player.WingBack >= 15 && player.Right >= 15) || (player.DF == 20 && player.Right == 20);

                case PlayerType.DefensiveMidfielder:
                    return (player.DM >= 15 && player.Centre >= 15) || (player.Centre == 20 && (player.DF == 20 || player.MF == 20));

                case PlayerType.LeftWingBack:
                    return (player.WingBack >= 15 && player.Left >= 15) || (player.DF == 20 && player.Left == 20);

                case PlayerType.RightMidfielder:
                    return (player.MF >= 15 && player.Right >= 15) || (player.Right == 20 && player.AM == 20);

                case PlayerType.CentralMidfielder:
                    return (player.MF >= 15 && player.Centre >= 15) ||
                        (player.DM == 20 && player.Centre >= 15) || (player.AM == 20 && player.Centre == 20);

                case PlayerType.LeftMidfielder:
                    return (player.MF >= 15 && player.Left >= 15) || (player.Left == 20 && player.AM == 20);

                case PlayerType.RightWinger:
                    return (player.AM >= 15 && player.Right >= 15) || (player.Right == 20 && (player.MF == 20 || player.ST == 20));

                case PlayerType.AttackingMidfielder:
                    return (player.AM >= 15 && player.Centre >= 15) || (player.Centre == 20 && (player.MF == 20 || player.ST == 20));

                case PlayerType.LeftWinger:
                    return (player.AM >= 15 && player.Left >= 15) || (player.Left == 20 && (player.MF == 20 || player.ST == 20));

                case PlayerType.CentreForward:
                    return (player.ST >= 15 && player.Centre >= 15) || (player.ST == 20) || (player.AM == 20 && player.Centre == 20);

                default:
                    return false;
            }
        }

        public ScoutingInformation GetRatings(Player player)
        {
            byte offFieldRating = GetRatingsForPersonality(player);

            ScoutingInformation results = new ScoutingInformation();
            results.Goalkeeper = GetRatingsForPosition(player, PlayerType.GoalKeeper, offFieldRating);
            results.RightBack = GetRatingsForPosition(player, PlayerType.RightBack, offFieldRating);
            results.CentreHalf = GetRatingsForPosition(player, PlayerType.CentreHalf, offFieldRating);
            results.LeftBack = GetRatingsForPosition(player, PlayerType.LeftBack, offFieldRating);
            results.RightWingBack = GetRatingsForPosition(player, PlayerType.RightWingBack, offFieldRating);
            results.DefensiveMidfielder = GetRatingsForPosition(player, PlayerType.DefensiveMidfielder, offFieldRating);
            results.LeftWingBack = GetRatingsForPosition(player, PlayerType.LeftWingBack, offFieldRating);
            results.RightMidfielder = GetRatingsForPosition(player, PlayerType.RightMidfielder, offFieldRating);
            results.CentreMidfielder = GetRatingsForPosition(player, PlayerType.CentralMidfielder, offFieldRating);
            results.LeftMidfielder = GetRatingsForPosition(player, PlayerType.LeftMidfielder, offFieldRating);
            results.RightWinger = GetRatingsForPosition(player, PlayerType.RightWinger, offFieldRating);
            results.AttackingMidfielder = GetRatingsForPosition(player, PlayerType.AttackingMidfielder, offFieldRating);
            results.LeftWinger = GetRatingsForPosition(player, PlayerType.LeftWinger, offFieldRating);
            results.CentreForward = GetRatingsForPosition(player, PlayerType.CentreForward, offFieldRating);

            return results;
        }

        private static bool CheckAttributeGroup(PropertyInfo prop, AttributeGroup group)
        {
            if (!Attribute.IsDefined(prop, typeof(AttributeGroupAttribute)))
            {
                return false;
            }

            var val = (AttributeGroupAttribute)prop.GetCustomAttributes(typeof(AttributeGroupAttribute), false).FirstOrDefault();
            return val.Grouping == group;
        }

        private void AddGK()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            role.AW(DP.Acceleration, 0);
            role.AW(DP.Aggression, 0);
            role.AW(DP.Agility, 1);
            role.AW(DP.Anticipation, 1);
            role.AW(DP.Balance, 0);
            role.AW(DP.Bravery, 0);
            role.AW(DP.Consistency, 1);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 0);
            role.AW(DP.Decisions, 1);
            role.AW(DP.Dirtiness, 0);
            role.AW(DP.Dribbling, 0);
            role.AW(DP.Finishing, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 10);
            role.AW(DP.Heading, 0);
            role.AW(DP.ImportantMatches, 1);
            role.AW(DP.Influence, 0);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 3);
            role.AW(DP.LongShots, 0);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 0);
            role.AW(DP.OneOnOnes, 1);
            role.AW(DP.Pace, 0);
            role.AW(DP.Passing, 0);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 2);
            role.AW(DP.Reflexes, 3);
            role.AW(DP.Stamina, 0);
            role.AW(DP.Strength, 1);
            role.AW(DP.Tackling, 0);
            role.AW(DP.Teamwork, 0);
            role.AW(DP.Technique, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);
            role.AW(DP.WorkRate, 0);
            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);
            role.AW(DP.MentalityWeight, 20);
            role.AW(DP.PhysicalityWeight, 10);
            role.AW(DP.TechnicalWeight, 100);
            weightings[(int)Roles.GK] = role;
        }

        private byte[] GetBaselineMentality()
        {
            var role = new byte[Enum.GetNames(typeof(DP)).Length];

            role.AW(DP.Aggression, 1);
            role.AW(DP.Anticipation, 3);
            role.AW(DP.Bravery, 1);
            role.AW(DP.Consistency, 4);
            role.AW(DP.Decisions, 5);
            role.AW(DP.Dirtiness, 0);
            role.AW(DP.ImportantMatches, 1);
            role.AW(DP.Influence, 0);
            role.AW(DP.Teamwork, 1);
            role.AW(DP.WorkRate, 1);

            return role;
        }

        private void AddDFB()
        {
            byte[] role = GetBaselineMentality(); 
            role.AW(DP.Acceleration, 3);
            role.AW(DP.Aggression, 3);
            role.AW(DP.Agility, 1);
            role.AW(DP.Balance, 1);
            role.AW(DP.Bravery, 4);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 2);
            role.AW(DP.Dirtiness, 0);
            role.AW(DP.Dribbling, 1);
            role.AW(DP.Finishing, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 0);
            role.AW(DP.Heading, 3);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 4);
            role.AW(DP.LongShots, 0);
            role.AW(DP.Marking, 8);
            role.AW(DP.OffTheBall, 1);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 3);
            role.AW(DP.Passing, 2);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 8);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 1);
            role.AW(DP.Strength, 3);
            role.AW(DP.Tackling, 8);
            role.AW(DP.Technique, 1);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 80);
            role.AW(DP.PhysicalityWeight, 90);
            role.AW(DP.TechnicalWeight, 100);
            weightings[(int)Roles.DFB] = role;
        }

        private void AddAFB()
        {
            byte[] role = GetBaselineMentality();
            role.AW(DP.Acceleration, 5);
            role.AW(DP.Agility, 0);
            role.AW(DP.Balance, 0);
            role.AW(DP.Bravery, 2);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 1);
            role.AW(DP.Crossing, 5);
            role.AW(DP.Dirtiness, 0);
            role.AW(DP.Dribbling, 2);
            role.AW(DP.Finishing, 1);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 0);
            role.AW(DP.Heading, 1);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 2);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 4);
            role.AW(DP.OffTheBall, 2);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 5);
            role.AW(DP.Passing, 4);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 7);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 0);
            role.AW(DP.Strength, 2);
            role.AW(DP.Tackling, 4);
            role.AW(DP.Technique, 3);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);
            role.AW(DP.WorkRate, 2);
            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);
            role.AW(DP.MentalityWeight, 80);
            role.AW(DP.PhysicalityWeight, 80);
            role.AW(DP.TechnicalWeight, 100);
            weightings[(int)Roles.AFB] = role;
        }

        private void AddCB()
        {
            byte[] role = GetBaselineMentality();
            role.AW(DP.Acceleration, 1);
            role.AW(DP.Aggression, 4);
            role.AW(DP.Agility, 0);
            role.AW(DP.Balance, 0);
            role.AW(DP.Bravery, 2);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 0);
            role.AW(DP.Dribbling, 0);
            role.AW(DP.Finishing, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 0);
            role.AW(DP.Heading, 2);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 8);
            role.AW(DP.LongShots, 0);
            role.AW(DP.Marking, 6);
            role.AW(DP.OffTheBall, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 2);
            role.AW(DP.Passing, 0);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 9);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 0);
            role.AW(DP.Strength, 4);
            role.AW(DP.Tackling, 8);
            role.AW(DP.Technique, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 90);
            role.AW(DP.PhysicalityWeight, 80);
            role.AW(DP.TechnicalWeight, 90);
            weightings[(int)Roles.CB] = role;
        }

        private void AddWB()
        {
            byte[] role = GetBaselineMentality();
            role.AW(DP.Acceleration, 5);
            role.AW(DP.Agility, 1);
            role.AW(DP.Balance, 1);
            role.AW(DP.Bravery, 2);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 1);
            role.AW(DP.Crossing, 7);
            role.AW(DP.Dirtiness, 0);
            role.AW(DP.Dribbling, 3);
            role.AW(DP.Finishing, 1);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 0);
            role.AW(DP.Heading, 1);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 2);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 3);
            role.AW(DP.OffTheBall, 3);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 5);
            role.AW(DP.Passing, 5);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 5);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 3);
            role.AW(DP.Strength, 2);
            role.AW(DP.Tackling, 3);
            role.AW(DP.Technique, 3);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);
            role.AW(DP.WorkRate, 4);
            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);
            role.AW(DP.MentalityWeight, 80);
            role.AW(DP.PhysicalityWeight, 80);
            role.AW(DP.TechnicalWeight, 100);
            weightings[(int)Roles.WB] = role;
        }

        private void AddDM()
        {
            byte[] role = GetBaselineMentality();
            role.AW(DP.Acceleration, 1);
            role.AW(DP.Aggression, 4);
            role.AW(DP.Agility, 0);
            role.AW(DP.Balance, 0);
            role.AW(DP.Bravery, 3);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 0);
            role.AW(DP.Dribbling, 0);
            role.AW(DP.Finishing, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 0);
            role.AW(DP.Heading, 1);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 2);
            role.AW(DP.LongShots, 0);
            role.AW(DP.Marking, 6);
            role.AW(DP.OffTheBall, 0);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 1);
            role.AW(DP.Passing, 4);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 6);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 2);
            role.AW(DP.Strength, 4);
            role.AW(DP.Tackling, 5);
            role.AW(DP.Teamwork, 3);
            role.AW(DP.Technique, 2);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);
            role.AW(DP.WorkRate, 4);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 80);
            role.AW(DP.PhysicalityWeight, 80);
            role.AW(DP.TechnicalWeight, 100);
            weightings[(int)Roles.HM] = role;
        }

        private void AddCM()
        {
            byte[] role = GetBaselineMentality();
            role.AW(DP.Acceleration, 1);
            role.AW(DP.Aggression, 2);
            role.AW(DP.Agility, 0);
            role.AW(DP.Balance, 0);
            role.AW(DP.Bravery, 2);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 0);
            role.AW(DP.Dribbling, 0);
            role.AW(DP.Finishing, 0);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 0);
            role.AW(DP.Heading, 1);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 2);
            role.AW(DP.LongShots, 3);
            role.AW(DP.Marking, 3);
            role.AW(DP.OffTheBall, 1);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 1);
            role.AW(DP.Passing, 6);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 4);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 2);
            role.AW(DP.Strength, 2);
            role.AW(DP.Tackling, 5);
            role.AW(DP.Teamwork, 2);
            role.AW(DP.Technique, 3);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);
            role.AW(DP.WorkRate, 4);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 50);
            role.AW(DP.PhysicalityWeight, 60);
            role.AW(DP.TechnicalWeight, 100);
            weightings[(int)Roles.CM] = role;
        }

        private void AddWM()
        {
            byte[] role = new byte[Enum.GetNames(typeof(DP)).Length];
            role.AW(DP.Acceleration, 3);
            role.AW(DP.Agility, 1);
            role.AW(DP.Balance, 2);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 2);
            role.AW(DP.Crossing, 4);
            role.AW(DP.Dribbling, 3);
            role.AW(DP.Finishing, 1);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 0);
            role.AW(DP.Heading, 0);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 0);
            role.AW(DP.LongShots, 0);
            role.AW(DP.Marking, 1);
            role.AW(DP.OffTheBall, 4);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 4);
            role.AW(DP.Passing, 5);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 1);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 3);
            role.AW(DP.Strength, 0);
            role.AW(DP.Tackling, 1);
            role.AW(DP.Teamwork, 2);
            role.AW(DP.Technique, 3);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);
            role.AW(DP.WorkRate, 2);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 70);
            role.AW(DP.PhysicalityWeight, 100);
            role.AW(DP.TechnicalWeight, 100);
            weightings[(int)Roles.WM] = role;
        }

        private void AddAM()
        {
            byte[] role = GetBaselineMentality();
            role.AW(DP.Acceleration, 2);
            role.AW(DP.Agility, 2);
            role.AW(DP.Anticipation, 4);
            role.AW(DP.Balance, 2);
            role.AW(DP.Consistency, 2);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 7);
            role.AW(DP.Crossing, 0);
            role.AW(DP.Dribbling, 4);
            role.AW(DP.Finishing, 5);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 2);
            role.AW(DP.Handling, 0);
            role.AW(DP.Heading, 0);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 0);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 4);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 2);
            role.AW(DP.Passing, 3);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 0);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 0);
            role.AW(DP.Strength, 0);
            role.AW(DP.Tackling, 0);
            role.AW(DP.Technique, 4);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 90);
            role.AW(DP.PhysicalityWeight, 50);
            role.AW(DP.TechnicalWeight, 100);
            weightings[(int)Roles.AM] = role;
        }

        private void AddWG()
        {
            byte[] role = GetBaselineMentality();
            role.AW(DP.Acceleration, 4);
            role.AW(DP.Agility, 1);
            role.AW(DP.Balance, 1);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 2);
            role.AW(DP.Crossing, 12);
            role.AW(DP.Dribbling, 9);
            role.AW(DP.Finishing, 2);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 1);
            role.AW(DP.Handling, 0);
            role.AW(DP.Heading, 0);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 0);
            role.AW(DP.LongShots, 2);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 8);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 5);
            role.AW(DP.Passing, 6);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 0);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 2);
            role.AW(DP.Strength, 0);
            role.AW(DP.Tackling, 0);
            role.AW(DP.Technique, 5);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 70);
            role.AW(DP.PhysicalityWeight, 70);
            role.AW(DP.TechnicalWeight, 100);
            weightings[(int)Roles.WG] = role;
        }

        private void AddST()
        {
            byte[] role = GetBaselineMentality();
            role.AW(DP.Acceleration, 6);
            role.AW(DP.Agility, 0);
            role.AW(DP.Anticipation, 6);
            role.AW(DP.Balance, 0);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 0);
            role.AW(DP.Dribbling, 2);
            role.AW(DP.Finishing, 8);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 3);
            role.AW(DP.Heading, 0);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 1);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 8);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 4);
            role.AW(DP.Passing, 2);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 0);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 0);
            role.AW(DP.Strength, 2);
            role.AW(DP.Tackling, 0);
            role.AW(DP.Technique, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 70);
            role.AW(DP.PhysicalityWeight, 80);
            role.AW(DP.TechnicalWeight, 100);
            weightings[(int)Roles.ST] = role;
        }

        private void AddTM()
        {
            byte[] role = GetBaselineMentality();
            role.AW(DP.Acceleration, 2);
            role.AW(DP.Aggression, 3);
            role.AW(DP.Agility, 0);
            role.AW(DP.Anticipation, 5);
            role.AW(DP.Balance, 0);
            role.AW(DP.Bravery, 2);
            role.AW(DP.Corners, 0);
            role.AW(DP.Creativity, 0);
            role.AW(DP.Crossing, 0);
            role.AW(DP.Dribbling, 2);
            role.AW(DP.Finishing, 8);
            role.AW(DP.FreeKicks, 0);
            role.AW(DP.Flair, 0);
            role.AW(DP.Handling, 3);
            role.AW(DP.Heading, 0);
            role.AW(DP.InjuryProneness, 0);
            role.AW(DP.Jumping, 5);
            role.AW(DP.LongShots, 1);
            role.AW(DP.Marking, 0);
            role.AW(DP.OffTheBall, 8);
            role.AW(DP.OneOnOnes, 0);
            role.AW(DP.Pace, 1);
            role.AW(DP.Passing, 2);
            role.AW(DP.Penalties, 0);
            role.AW(DP.Positioning, 0);
            role.AW(DP.Reflexes, 0);
            role.AW(DP.Stamina, 0);
            role.AW(DP.Strength, 5);
            role.AW(DP.Tackling, 0);
            role.AW(DP.Technique, 0);
            role.AW(DP.ThrowIns, 0);
            role.AW(DP.Versatility, 0);

            role.AW(DP.LeftFoot, 0);
            role.AW(DP.RightFoot, 0);

            role.AW(DP.MentalityWeight, 70);
            role.AW(DP.PhysicalityWeight, 100);
            role.AW(DP.TechnicalWeight, 60);
            weightings[(int)Roles.TM] = role;
        }

        private void AddOffField()
        {
            byte[] person = new byte[52];
            person.AW(DP.Adaptability, 0);
            person.AW(DP.Ambition, 0);
            person.AW(DP.Determination, 3);
            person.AW(DP.Loyalty, 4);
            person.AW(DP.Pressure, 6);
            person.AW(DP.Professionalism, 4);
            person.AW(DP.Sportsmanship, 0);
            person.AW(DP.Temperament, 3);
            weightings[weightings.Length - 1] = person;
        }

        private byte[] GetValues(Player player)
        {
            byte[] values = new byte[Enum.GetNames(typeof(DP)).Length];
            values.AW(DP.Acceleration, player._player.Acceleration);
            values.AW(DP.Aggression, player._player.Aggression);
            values.AW(DP.Agility, player._player.Agility);
            values.AW(DP.Anticipation, player._player.Anticipation);
            values.AW(DP.Balance, player._player.Balance);
            values.AW(DP.Bravery, player._player.Bravery);
            values.AW(DP.Consistency, player._player.Consistency);
            values.AW(DP.Corners, player._player.Corners);
            values.AW(DP.Creativity, player._player.Creativity);
            values.AW(DP.Crossing, player._player.Crossing);
            values.AW(DP.Decisions, player._player.Decisions);
            values.AW(DP.Dirtiness, player._player.Dirtiness);
            values.AW(DP.Dribbling, player._player.Dribbling);
            values.AW(DP.Finishing, player._player.Finishing);
            values.AW(DP.FreeKicks, player._player.FreeKicks);
            values.AW(DP.Flair, player._player.Flair);
            values.AW(DP.Handling, player._player.Handling);
            values.AW(DP.Heading, player._player.Heading);
            values.AW(DP.ImportantMatches, player._player.ImportantMatches);
            values.AW(DP.Influence, player._player.Influence);
            values.AW(DP.InjuryProneness, player._player.InjuryProneness);
            values.AW(DP.Jumping, player._player.Jumping);
            values.AW(DP.LongShots, player._player.LongShots);
            values.AW(DP.Marking, player._player.Marking);
            values.AW(DP.OffTheBall, player._player.OffTheBall);
            values.AW(DP.OneOnOnes, player._player.OneOnOnes);
            values.AW(DP.Pace, player._player.Pace);
            values.AW(DP.Passing, player._player.Passing);
            values.AW(DP.Penalties, player._player.Penalties);
            values.AW(DP.Positioning, player._player.Positioning);
            values.AW(DP.Reflexes, player._player.Reflexes);
            values.AW(DP.Stamina, player._player.Stamina);
            values.AW(DP.Strength, player._player.Strength);
            values.AW(DP.Tackling, player._player.Tackling);
            values.AW(DP.Teamwork, player._player.Teamwork);
            values.AW(DP.Technique, player._player.Technique);
            values.AW(DP.ThrowIns, player._player.ThrowIns);
            values.AW(DP.Versatility, player._player.Versatility);
            values.AW(DP.WorkRate, player._player.WorkRate);
            values.AW(DP.LeftFoot, player._player.Left);
            values.AW(DP.RightFoot, player._player.Right);

            return values;
        }

        private PositionRating GetRatingsForPosition(Player player, PlayerType type, byte offFieldRating)
        {
            PositionRating rating = new PositionRating() { Position = type } ;

            List<Roles> roles = type.GetAttributeValue<LinkedRoles, List<Roles>>(x => x.Roles);
            foreach (var role in roles)
            {
                rating.Ratings.Add(GetRatingForTypeAndRole(player, type, role, offFieldRating));
            }

            return rating;
        }

        private RoleRating GetRatingForTypeAndRole(Player player, PlayerType type, Roles role, byte offFieldRating)
        {
            RatingRoleDebug roleDebug = new RatingRoleDebug();
            var rating = AdjustScoreForOffField(AdjustScoreForPosition(player, type, CalculateRating(player, type, role, ref roleDebug), roleDebug), offFieldRating, roleDebug);
            return new RoleRating() { Rating = rating, Role = role, Debug = roleDebug, };
        }

        private byte GetRatingsForPersonality(Player player)
        {
            string mentalDebugString;
            byte offField = GetGroupingScore_Reflection(player._staff, OffFieldAttributes, weightings[weightings.Length - 1], out mentalDebugString);

            return offField;
        }

        private byte CalculateRating(Player player, PlayerType type, Roles role, ref RatingRoleDebug debug)
        {
            RatingRoleDebug roleDebug;
            var weights = GetWeights(role);

            //var values = GetValues(player);
            byte result = RatePlayerInRole(player, type, role, weights, out roleDebug);

            debug = roleDebug;
            return result;
        }

        private byte[] GetWeights(Roles role)
        {
            return weightings[(int)role];
        }

        private byte RatePlayerInRole_Reflection(Player player, PlayerType type, Roles role, byte[] weights, out RatingRoleDebug debug)
        {
            string mentalDebugString, physicalDebugString, technicalDebugString;

            byte mentalWeight = weights.GW(DP.MentalityWeight);
            byte physicalWeight = weights.GW(DP.PhysicalityWeight);
            byte technicalWeight = weights.GW(DP.TechnicalWeight);

            var mental = GetGroupingScore(player._player, MentalAttributes, weights, out mentalDebugString);
            var physical = GetGroupingScore(player._player, PhysicalAttributes, weights, out physicalDebugString);
            var technical = GetGroupingScore(player._player, TechnicalAttributes, weights, out technicalDebugString);

            decimal mentalScore = Weight(mental, mentalWeight);
            decimal physicalScore = Weight(physical, physicalWeight);
            decimal technicalScore = Weight(technical, technicalWeight);
            decimal adjust = (decimal)(mentalWeight + physicalWeight + technicalWeight) / 100;

            debug = new RatingRoleDebug()
            {
                Position = type.ToString(),
                Role = role,
                Mental = $"{mentalScore} / {mentalWeight}",
                MentalDetail = mentalDebugString,
                Physical = $"{physicalScore} / {physicalWeight}",
                PhysicalDetail = physicalDebugString,
                Technical = $"{technicalScore} / {technicalWeight}",
                TechnicalDetail = technicalDebugString,
            };

            return (byte)((mentalScore + physicalScore + technicalScore) / adjust);
        }

        private byte RatePlayerInRole(Player player, PlayerType type, Roles role, byte[] weights, out RatingRoleDebug debug)
        {
            string mentalDebugString, physicalDebugString, technicalDebugString;

            byte mentalWeight = weights.GW(DP.MentalityWeight);
            byte physicalWeight = weights.GW(DP.PhysicalityWeight);
            byte technicalWeight = weights.GW(DP.TechnicalWeight);

            byte[] values = GetValues(player);

            var mental = GetGroupingScore(player._player, MentalWeights, values, weights, false, out mentalDebugString);
            var physical = GetGroupingScore(player._player, PhysicalWeights, values, weights, false, out physicalDebugString);
            var technical = GetGroupingScore(player._player, TechnicalWeights, values, weights, true, out technicalDebugString);

            decimal mentalScore = Weight(mental, mentalWeight);
            decimal physicalScore = Weight(physical, physicalWeight);
            decimal technicalScore = Weight(technical, technicalWeight);
            decimal adjust = (decimal)(mentalWeight + physicalWeight + technicalWeight) / 100;

            debug = new RatingRoleDebug()
            {
                Position = type.ToString(),
                Role = role,
                Mental = $"{mentalScore} / {mentalWeight}",
                MentalDetail = mentalDebugString,
                Physical = $"{physicalScore} / {physicalWeight}",
                PhysicalDetail = physicalDebugString,
                Technical = $"{technicalScore} / {technicalWeight}",
                TechnicalDetail = technicalDebugString,
            };

            return (byte)((mentalScore + physicalScore + technicalScore) / adjust);
        }

        private byte AdjustScoreForPosition(Player player, PlayerType type, decimal unadjustedScore, RatingRoleDebug debug)
        {
            decimal positionModifier = (decimal)PositionalFamiliarity(type, player) / 100;
            debug.Position = positionModifier.ToString("0.00");

            return (byte)(unadjustedScore * positionModifier);
        }

        private byte AdjustScoreForOffField(byte unadjustedScore, byte offFieldRating, RatingRoleDebug debug)
        {
            decimal offFieldBonus = -5 + (((decimal)offFieldRating) / 10);

            return (byte)Math.Min(99, Math.Max(0, (unadjustedScore + offFieldBonus)));
        }

        private byte GetGroupingScore_Reflection(Staff staff, List<PropertyInfo> attributes, byte[] weights, out string debugString)
        {
            decimal rating = 0;
            int combinedWeights = 0;
            byte realValue;
            debugString = string.Empty;

            foreach (var x in attributes)
            {
                byte weight = weights[(int)Enum.Parse(typeof(DP), x.Name)];
                combinedWeights += weight;
                decimal playerValue = GetPlayerValue(x, staff, out realValue);
                decimal weightedValue = playerValue * weight;
                rating += weightedValue;

                if (weightedValue > 0)
                {
                    debugString += $"{x.Name} : {playerValue}-{weight}({realValue}) ";
                }
            }

            int maxScore = 20 * combinedWeights;

            var result = (byte)((rating / maxScore) * 100);
            return result;
        }

        private byte GetGroupingScore(PlayerData player, byte[] attributes, byte[] values, byte[] weights, bool isIntrinsic, out string debugString)
        {
            decimal rating = 0;
            int combinedWeights = 0;
            byte realValue;
            debugString = string.Empty;

            if (values.Length != weights.Length || Enum.GetNames(typeof(DP)).Length != values.Length)
            {
                throw new ApplicationException("Unbalanced");
            }

            //for (int i = 0; i < weights.Length; i++)
            foreach (var i in attributes)
            {
                byte weight = weights[i];

                if (weight == 0)
                {
                    continue;
                }

                combinedWeights += weight;
                realValue = values[i];
                string attribute = Enum.GetNames(typeof(DP))[i];

                decimal value = Adj(realValue, isIntrinsic);

                decimal weightedValue = value * weight;
                rating += weightedValue;

                debugString += $"{attribute} : {value}-{weight}({realValue}) ";
            }

            int maxScore = 20 * combinedWeights;

            var result = (byte)((rating / maxScore) * 100);
            return result;
        }

        private byte GetGroupingScore(PlayerData player, List<PropertyInfo> attributes, byte[] weights, out string debugString)
        {
            decimal rating = 0;
            int combinedWeights = 0;
            byte realValue;
            debugString = string.Empty;

            foreach (var x in attributes)
            {
                realValue = 0;
                byte weight = weights[(int)Enum.Parse(typeof(DP), x.Name)];
                combinedWeights += weight;
                decimal playerValue = weight == 0 ? 0 : GetPlayerValue(x, player, out realValue);
                decimal weightedValue = playerValue * weight;
                rating += weightedValue;

                if (weightedValue > 0)
                {
                    debugString += $"{x.Name} : {playerValue}-{weight}({realValue}) ";
                }
            }

            int maxScore = 20 * combinedWeights;

            var result = (byte)((rating / maxScore) * 100);
            return result;
        }

        private decimal GetPlayerValue(PropertyInfo prop, object player, out byte realValue)
        {
            byte stat = (byte)prop.GetValue(player);
            realValue = stat;

            bool isIntrinsic = (player is PlayerData) && ((DataFileInfoAttribute)prop.GetCustomAttributes(typeof(DataFileInfoAttribute)).FirstOrDefault()).IsIntrinsic;

            return Adj(stat, isIntrinsic);
        }

        private decimal Weight(byte score, short importance)
        {
            score = Math.Min(score, (byte)100);
            return (decimal)score / 100 * importance;
        }

        private decimal Adj(byte val, bool isIntrinsic)
        {
            if (!isIntrinsic)
            {
                return val;
            }

            return masker.GetIntrinsicMask(val);
        }

        private byte PositionalFamiliarity(PlayerType type, Player player)
        {
            byte modifierForPosition = 100;
            byte modifierForVersitility = GetVersitilityModifier(player._player.Versatility);

            switch (type)
            {
                case PlayerType.GoalKeeper:
                    modifierForPosition = GetFamiliarity(player._player.GK, player._player.GK); // double down on GK position, not side
                    break;

                case PlayerType.RightBack:
                    modifierForPosition = GetFamiliarity(player._player.DF, player._player.Right);
                    break;

                case PlayerType.CentreHalf:
                    modifierForPosition = GetFamiliarity(player._player.DF, player._player.Centre);
                    break;

                case PlayerType.LeftBack:
                    modifierForPosition = GetFamiliarity(player._player.DF, player._player.Left);
                    break;

                case PlayerType.RightWingBack:
                    modifierForPosition = GetFamiliarity(player._player.WingBack, player._player.Right);
                    break;

                case PlayerType.DefensiveMidfielder:
                    modifierForPosition = GetFamiliarity(player._player.DM, player._player.Centre);
                    break;

                case PlayerType.LeftWingBack:
                    modifierForPosition = GetFamiliarity(player._player.WingBack, player._player.Left);
                    break;

                case PlayerType.RightMidfielder:
                    modifierForPosition = GetFamiliarity(player._player.MF, player._player.Right);
                    break;

                case PlayerType.CentralMidfielder:
                    modifierForPosition = GetFamiliarity(player._player.MF, player._player.Centre);
                    break;

                case PlayerType.LeftMidfielder:
                    modifierForPosition = GetFamiliarity(player._player.MF, player._player.Left);
                    break;

                case PlayerType.RightWinger:
                    modifierForPosition = GetFamiliarity(player._player.AM, player._player.Right);
                    break;

                case PlayerType.AttackingMidfielder:
                    modifierForPosition = GetFamiliarity(player._player.AM, player._player.Centre);
                    break;

                case PlayerType.LeftWinger:
                    modifierForPosition = GetFamiliarity(player._player.AM, player._player.Left);
                    break;

                case PlayerType.CentreForward:
                    modifierForPosition = GetFamiliarity(player._player.ST, player._player.Centre);
                    break;

                default:
                    modifierForPosition = 1;
                    break;
            }

            return (byte)(((decimal)modifierForPosition + modifierForVersitility) / 200 * 100);
        }

        private byte GetFamiliarity(byte position, byte channel = 20)
        {
            return (byte)((position + channel) * 2.5);
        }

        private byte GetVersitilityModifier(byte versatility)
        {
            return (byte)100;
        }
    }
}
