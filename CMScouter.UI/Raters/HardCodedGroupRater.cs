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
    internal class HardCodededGroupRater : IPlayerRater
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

        /*
            GK: Anticipation, Decisions, Handling, Heading, Positioning, Reflexes, Tackling, One on Ones
            Full backs: Anticipation, Crossing, Decisions, Dribbling, Marking, Positioning, Tackling, Team work
            Centre Backs: Anticipation, Decisions, Heading, Marking, Positioning, Tackling, Strength
            Wingers: Agility, Balance, Crossing, Dribbling, Flair(If you please), Set pieces(If you please), Passing, Teamwork
            Centre Mids: Have pretty much everything, but can vary if you want a more defensive or more attacking player
            Strikers: Decisions, Finishing, Heading (depending on the striker), Movement (Depending on the striker) Natural Fitness, Strength (depending on the striker).
        */

        private void AddGK()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 6;
            groupWeight[(int)AG.Reliability] = 14;

            groupWeight[(int)AG.Playmaking] = 0;
            groupWeight[(int)AG.Wideplay] = 0;
            groupWeight[(int)AG.Scoring] = 0;
            groupWeight[(int)AG.Defending] = 0;
            groupWeight[(int)AG.Goalkeeping] = 70;

            groupWeight[(int)AG.Speed] = 0;
            groupWeight[(int)AG.Strength] = 10;

            groupedWeightings[(int)Roles.GK] = groupWeight;
        }

        private void AddDFB()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 10;
            groupWeight[(int)AG.Reliability] = 10;

            groupWeight[(int)AG.Playmaking] = 0;
            groupWeight[(int)AG.Wideplay] = 10;
            groupWeight[(int)AG.Scoring] = 0;
            groupWeight[(int)AG.Defending] = 45;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 20;
            groupWeight[(int)AG.Strength] = 5;

            groupedWeightings[(int)Roles.DFB] = groupWeight;
        }

        private void AddAFB()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 8;
            groupWeight[(int)AG.Reliability] = 8;

            groupWeight[(int)AG.Playmaking] = 8;
            groupWeight[(int)AG.Wideplay] = 15;
            groupWeight[(int)AG.Scoring] = 0;
            groupWeight[(int)AG.Defending] = 15;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 35;
            groupWeight[(int)AG.Strength] = 5;

            groupedWeightings[(int)Roles.AFB] = groupWeight;
        }

        private void AddCB()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 15;
            groupWeight[(int)AG.Reliability] = 5;

            groupWeight[(int)AG.Playmaking] = 0;
            groupWeight[(int)AG.Wideplay] = 0;
            groupWeight[(int)AG.Scoring] = 0;
            groupWeight[(int)AG.Defending] = 35;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 15;
            groupWeight[(int)AG.Strength] = 30;

            groupedWeightings[(int)Roles.CB] = groupWeight;
        }

        private void AddLD()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 3;
            groupWeight[(int)AG.Reliability] = 2;

            groupWeight[(int)AG.Playmaking] = 0;
            groupWeight[(int)AG.Wideplay] = 0;
            groupWeight[(int)AG.Scoring] = 0;
            groupWeight[(int)AG.Defending] = 60;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 5;
            groupWeight[(int)AG.Strength] = 30;

            groupedWeightings[(int)Roles.LD] = groupWeight;
        }

        private void AddWB()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 10;
            groupWeight[(int)AG.Reliability] = 10;

            groupWeight[(int)AG.Playmaking] = 5;
            groupWeight[(int)AG.Wideplay] = 15;
            groupWeight[(int)AG.Scoring] = 0;
            groupWeight[(int)AG.Defending] = 25;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 30;
            groupWeight[(int)AG.Strength] = 5;

            groupedWeightings[(int)Roles.WB] = groupWeight;
        }

        private void AddDM()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 15;
            groupWeight[(int)AG.Reliability] = 10;

            groupWeight[(int)AG.Playmaking] = 10;
            groupWeight[(int)AG.Wideplay] = 0;
            groupWeight[(int)AG.Scoring] = 0;
            groupWeight[(int)AG.Defending] = 20;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 15;
            groupWeight[(int)AG.Strength] = 30;

            groupedWeightings[(int)Roles.HM] = groupWeight;
        }

        private void AddCM()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 15;
            groupWeight[(int)AG.Reliability] = 15;

            groupWeight[(int)AG.Playmaking] = 20;
            groupWeight[(int)AG.Wideplay] = 5;
            groupWeight[(int)AG.Scoring] = 5;
            groupWeight[(int)AG.Defending] = 10;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 10;
            groupWeight[(int)AG.Strength] = 20;

            groupedWeightings[(int)Roles.CM] = groupWeight;
        }

        private void AddPM()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 10;
            groupWeight[(int)AG.Reliability] = 10;

            groupWeight[(int)AG.Playmaking] = 55;
            groupWeight[(int)AG.Wideplay] = 5;
            groupWeight[(int)AG.Scoring] = 5;
            groupWeight[(int)AG.Defending] = 5;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 5;
            groupWeight[(int)AG.Strength] = 5;

            groupedWeightings[(int)Roles.CM] = groupWeight;
        }

        private void AddWM()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 5;
            groupWeight[(int)AG.Reliability] = 10;

            groupWeight[(int)AG.Playmaking] = 15;
            groupWeight[(int)AG.Wideplay] = 20;
            groupWeight[(int)AG.Scoring] = 5;
            groupWeight[(int)AG.Defending] = 5;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 35;
            groupWeight[(int)AG.Strength] = 5;

            groupedWeightings[(int)Roles.WM] = groupWeight;
        }

        private void AddAM()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 5;
            groupWeight[(int)AG.Reliability] = 10;

            groupWeight[(int)AG.Playmaking] = 35;
            groupWeight[(int)AG.Wideplay] = 20;
            groupWeight[(int)AG.Scoring] = 10;
            groupWeight[(int)AG.Defending] = 0;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 15;
            groupWeight[(int)AG.Strength] = 5;

            groupedWeightings[(int)Roles.AM] = groupWeight;
        }

        private void AddWG()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 5;
            groupWeight[(int)AG.Reliability] = 10;

            groupWeight[(int)AG.Playmaking] = 10;
            groupWeight[(int)AG.Wideplay] = 25;
            groupWeight[(int)AG.Scoring] = 15;
            groupWeight[(int)AG.Defending] = 0;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 35;
            groupWeight[(int)AG.Strength] = 0;

            groupedWeightings[(int)Roles.WG] = groupWeight;
        }

        private void AddST()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 10;
            groupWeight[(int)AG.Reliability] = 10;

            groupWeight[(int)AG.Playmaking] = 5;
            groupWeight[(int)AG.Wideplay] = 0;
            groupWeight[(int)AG.Scoring] = 35;
            groupWeight[(int)AG.Defending] = 0;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 20;
            groupWeight[(int)AG.Strength] = 20;

            groupedWeightings[(int)Roles.ST] = groupWeight;
        }

        private void AddPO()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 10;
            groupWeight[(int)AG.Reliability] = 10;

            groupWeight[(int)AG.Playmaking] = 0;
            groupWeight[(int)AG.Wideplay] = 10;
            groupWeight[(int)AG.Scoring] = 45;
            groupWeight[(int)AG.Defending] = 0;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 20;
            groupWeight[(int)AG.Strength] = 5;

            groupedWeightings[(int)Roles.PO] = groupWeight;
        }

        private void AddTM()
        {
            byte[] groupWeight = new byte[Enum.GetNames(typeof(AG)).Length];

            groupWeight[(int)AG.Impact] = 15;
            groupWeight[(int)AG.Reliability] = 5;

            groupWeight[(int)AG.Playmaking] = 5;
            groupWeight[(int)AG.Wideplay] = 5;
            groupWeight[(int)AG.Scoring] = 30;
            groupWeight[(int)AG.Defending] = 0;
            groupWeight[(int)AG.Goalkeeping] = 0;

            groupWeight[(int)AG.Speed] = 10;
            groupWeight[(int)AG.Strength] = 30;

            groupedWeightings[(int)Roles.TM] = groupWeight;
        }

        private void AddOffField()
        {
            byte[] person = new byte[Enum.GetNames(typeof(DP)).Length];
            person.AW(DP.Adaptability, 0);
            person.AW(DP.Ambition, 0);
            person.AW(DP.Determination, 20);
            person.AW(DP.Loyalty, 4);
            person.AW(DP.Pressure, 6);
            person.AW(DP.Professionalism, 4);
            person.AW(DP.Sportsmanship, 0);
            person.AW(DP.Temperament, 3);
            weightings[weightings.Length - 1] = person;
        }

        byte[][] weightings;
        byte[][] groupedWeightings;

        private AttributeWeight[] ImpactAttributes = new AttributeWeight[]
            { new AttributeWeight{ Attribute = DP.Aggression, Weight = 10 }, new AttributeWeight{ Attribute = DP.Bravery, Weight = 10 } };

        private AttributeWeight[] ReliabilityAttributes = new AttributeWeight[]
        {
            new AttributeWeight{ Attribute = DP.Consistency, Weight = 10}, new AttributeWeight{ Attribute = DP.ImportantMatches, Weight = 4},
            new AttributeWeight{ Attribute = DP.Teamwork, Weight = 4}, new AttributeWeight{ Attribute = DP.WorkRate, Weight = 4},
        };

        private AttributeWeight[] PlaymakingAttributes = new AttributeWeight[]
        {
            new AttributeWeight{ Attribute = DP.Creativity, Weight = 4, IsIntrinsic = true}, new AttributeWeight{ Attribute = DP.LongShots, Weight = 1, IsIntrinsic = true }, new AttributeWeight { Attribute = DP.Dribbling, Weight = 2, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.Passing, Weight = 10, IsIntrinsic = true }, new AttributeWeight{ Attribute = DP.Technique, Weight = 8 }, new AttributeWeight{ Attribute = DP.Anticipation, Weight = 4, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.Decisions, Weight = 2, IsIntrinsic = true}, new AttributeWeight{ Attribute = DP.OffTheBall, Weight = 8, IsIntrinsic = true }, new AttributeWeight { Attribute = DP.Crossing, Weight = 2, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.Teamwork, Weight = 4 }, new AttributeWeight{ Attribute = DP.WorkRate, Weight = 3 }
        };

        private AttributeWeight[] WidePlayAttributes = new AttributeWeight[]
        {
            new AttributeWeight{ Attribute = DP.Crossing, Weight = 10, IsIntrinsic = true }, new AttributeWeight{ Attribute = DP.Dribbling, Weight = 8, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.OffTheBall, Weight = 7, IsIntrinsic = true }, new AttributeWeight{ Attribute = DP.Passing, Weight = 4, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.Technique, Weight = 6 }, new AttributeWeight{ Attribute = DP.Flair, Weight = 4 }
        };

        private AttributeWeight[] ScoringAttributes = new AttributeWeight[]
        {
            new AttributeWeight{ Attribute = DP.Finishing, Weight = 10, IsIntrinsic = true }, new AttributeWeight{ Attribute = DP.Heading, Weight = 10, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.LongShots, Weight = 1, IsIntrinsic = true }, new AttributeWeight{ Attribute = DP.OffTheBall, Weight = 20, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.Technique, Weight = 5 }, new AttributeWeight{ Attribute = DP.Flair, Weight = 2 },
            new AttributeWeight{ Attribute = DP.Anticipation, Weight = 5, IsIntrinsic = true }
        };

        private AttributeWeight[] DefendingAttributes = new AttributeWeight[]
        {
            new AttributeWeight{ Attribute = DP.Heading, Weight = 0, IsIntrinsic = true }, new AttributeWeight{ Attribute = DP.Marking, Weight = 20, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.Positioning, Weight = 30, IsIntrinsic = true }, new AttributeWeight{ Attribute = DP.Tackling, Weight = 30, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.Anticipation, Weight = 0, IsIntrinsic = true }
        };

        private AttributeWeight[] GoalkeepingAttributes = new AttributeWeight[]
        {
            new AttributeWeight{ Attribute = DP.Handling, Weight = 15, IsIntrinsic = true }, new AttributeWeight{ Attribute = DP.OneOnOnes, Weight = 2, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.Reflexes, Weight = 5, IsIntrinsic = true }, new AttributeWeight { Attribute = DP.Positioning, Weight = 5, IsIntrinsic = true },
            new AttributeWeight{ Attribute = DP.Agility, Weight = 4}
        };

        private AttributeWeight[] SpeedAttributes = new AttributeWeight[]
        {
            new AttributeWeight{ Attribute = DP.Acceleration, Weight = 10 }, new AttributeWeight{ Attribute = DP.Agility, Weight = 6 },
            new AttributeWeight{ Attribute = DP.Pace, Weight = 10 }, new AttributeWeight{ Attribute = DP.Stamina, Weight = 3 }
        };

        private AttributeWeight[] StrengthAttributes = new AttributeWeight[]
        {
            new AttributeWeight{ Attribute = DP.Balance, Weight = 2 }, new AttributeWeight{ Attribute = DP.Jumping, Weight = 13 },
            new AttributeWeight{ Attribute = DP.Stamina, Weight = 2 }, new AttributeWeight{ Attribute = DP.Strength, Weight = 10 }
        };


        private byte[] OffFieldAttributes = new byte[] { (byte)DP.Adaptability, (byte)DP.Ambition, (byte)DP.Determination, (byte)DP.Loyalty, (byte)DP.Pressure, (byte)DP.Professionalism, (byte)DP.Sportsmanship, (byte)DP.Temperament, (byte)DP.Versatility };

        private IIntrinsicMasker masker;

        // rename
        public HardCodededGroupRater(IIntrinsicMasker Masker, GroupedRoleWeights weights)
        {
            masker = Masker;

            // last one is the off field
            weightings = new byte[Enum.GetNames(typeof(Roles)).Count() + 1][];
            AddOffField();

            groupedWeightings = new byte[Enum.GetNames(typeof(Roles)).Count() + 1][];
            AddGK();
            AddDFB();
            AddAFB();
            AddCB();
            AddLD();
            AddDM();
            AddWB();
            AddCM();
            AddPM();
            AddWM();
            AddAM();
            AddWG();
            AddPO();
            AddTM();
            AddST();
        }

        public bool PlaysPosition(PlayerPosition type, PlayerData player)
        {
            switch (type)
            {
                case PlayerPosition.GoalKeeper:
                    return player.GK >= 19;

                case PlayerPosition.RightBack:
                    return player.DF >= 15 && player.Right >= 15; // add WBs

                case PlayerPosition.CentreHalf:
                    return player.DF >= 15 && player.Centre >= 15;

                case PlayerPosition.LeftBack:
                    return player.DF >= 15 && player.Left >= 15;

                case PlayerPosition.RightWingBack:
                    return (player.WingBack >= 15 && player.Right >= 15) || (player.DF == 20 && player.Right == 20);

                case PlayerPosition.DefensiveMidfielder:
                    return (player.DM >= 15 && player.Centre >= 15) || (player.Centre == 20 && (player.DF == 20 || player.MF == 20));

                case PlayerPosition.LeftWingBack:
                    return (player.WingBack >= 15 && player.Left >= 15) || (player.DF == 20 && player.Left == 20);

                case PlayerPosition.RightMidfielder:
                    return (player.MF >= 15 && player.Right >= 15) || (player.Right == 20 && player.AM == 20);

                case PlayerPosition.CentralMidfielder:
                    return (player.MF >= 15 && player.Centre >= 15) ||
                        (player.DM == 20 && player.Centre >= 15) || (player.AM == 20 && player.Centre == 20);

                case PlayerPosition.LeftMidfielder:
                    return (player.MF >= 15 && player.Left >= 15) || (player.Left == 20 && player.AM == 20);

                case PlayerPosition.RightWinger:
                    return (player.AM >= 15 && player.Right >= 15) || (player.Right == 20 && (player.MF == 20 || player.ST == 20));

                case PlayerPosition.AttackingMidfielder:
                    return (player.AM >= 15 && player.Centre >= 15) || (player.Centre == 20 && (player.MF == 20 || player.ST == 20));

                case PlayerPosition.LeftWinger:
                    return (player.AM >= 15 && player.Left >= 15) || (player.Left == 20 && (player.MF == 20 || player.ST == 20));

                case PlayerPosition.CentreForward:
                    return (player.ST >= 15 && player.Centre >= 15) || (player.ST == 20) || (player.AM == 20 && player.Centre == 20);

                default:
                    return false;
            }
        }

        public ScoutingInformation GetRatings(Player player)
        {
            return GetRatings(player, null);
        }

        public ScoutingInformation GetRatings(Player player, ConstructPlayerOptions options)
        {
            byte offFieldRating = GetRatingsForPersonality(player);
            //decimal offFieldAdjustment = GetAdjustmentByOffFieldRating(offFieldRating);

            //New object
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

            values.AW(DP.Adaptability, player._staff.Adaptability);
            values.AW(DP.Ambition, player._staff.Ambition);
            values.AW(DP.Determination, player._staff.Determination);
            values.AW(DP.Loyalty, player._staff.Loyalty);
            values.AW(DP.Pressure, player._staff.Pressure);
            values.AW(DP.Professionalism, player._staff.Professionalism);
            values.AW(DP.Sportsmanship, player._staff.Sportsmanship);
            values.AW(DP.Temperament, player._staff.Temperament);

            return values;
        }

        private PositionRating GetRatingsForPosition(Player player, PlayerPosition setPosition, PlayerPosition movementPosition, byte offFieldRating)
        {
            PositionRating positionRatings = new PositionRating(offField: offFieldRating) { SetPosition = setPosition, MovementPosition = movementPosition };

            List<Roles> roles = setPosition.GetAttributeValue<LinkedRoles, List<Roles>>(x => x.Roles);
            foreach (var role in roles)
            {
                var rating = GetRatingForTypeAndRole(player, setPosition, movementPosition, role);
                rating.PurchaseRating = ApplyOffFieldAdjustment(rating.AbilityRating, offFieldRating, rating.Debug);
                positionRatings.RoleRatings.Add(rating);
            }

            return positionRatings;
        }

        private RoleRating GetRatingForTypeAndRole(Player player, PlayerPosition setPosition, PlayerPosition movementPosition, Roles role)
        {
            RatingRoleDebug roleDebug = new RatingRoleDebug();
            GroupedRatings playerGroupedRatings = null;

            var rating = CalculateRating(player, setPosition, movementPosition, role, ref playerGroupedRatings, ref roleDebug);

            return new RoleRating() { AbilityRating = rating, Role = role, Debug = roleDebug, };
        }

        private byte GetRatingsForPersonality(Player player)
        {
            string mentalDebugString;
            byte[] values = GetValues(player);

            // TODO use a different method!
            byte offField = GetGroupingScore(OffFieldAttributes, values, weightings[weightings.Length - 1], false, player, PlayerPosition.GoalKeeper, PlayerPosition.GoalKeeper, out mentalDebugString);

            return offField;
        }

        private byte CalculateRating(Player player, PlayerPosition setPosition, PlayerPosition movementPosition, Roles role, ref GroupedRatings playerGroupedRatings, ref RatingRoleDebug debug)
        {
            RatingRoleDebug roleDebug;
            var weights = GetWeights(role);

            LogDebug($"*** {role} ***");

            playerGroupedRatings = playerGroupedRatings ?? CreatePlayerGroupedRatings(player, setPosition, movementPosition);

            //var values = GetValues(player);
            decimal result = RatePlayerInRole(player, setPosition, role, weights, playerGroupedRatings, out roleDebug);

            var proportion = result;

            debug = roleDebug;

            /*
            decimal BestRating = 90;
            decimal WorstRating = 35;
            decimal spreadForNewRatings = 98;

            result = Math.Max(result, WorstRating);
            result = Math.Min(result, BestRating);

            // function to reset to 1-99 from above expected range
            proportion = (((decimal)result - WorstRating) * spreadForNewRatings) / (BestRating - WorstRating);
            proportion += 1;*/

            return (byte)proportion;
        }

        private GroupedRatings CreatePlayerGroupedRatings(Player player, PlayerPosition setPosition, PlayerPosition movementPosition)
        {
            var playerGroupedRatings = new GroupedRatings();

            byte[] values = GetValues(player);

            if (setPosition == movementPosition)
            {
                LogDebug($"{setPosition}");
            }
            else
            {
                LogDebug($"{setPosition} -> {movementPosition}");
            }

            LogDebug("Impact");
            playerGroupedRatings.impactRating = GetGroupingScore(player, setPosition, movementPosition, ImpactAttributes, values);
            LogDebug("Reliability");
            playerGroupedRatings.reliabilityRating = GetGroupingScore(player, setPosition, movementPosition, ReliabilityAttributes, values);
            LogDebug("Playmaking");
            playerGroupedRatings.playmakingRating = GetGroupingScore(player, setPosition, movementPosition, PlaymakingAttributes, values);
            LogDebug("Wideplay");
            playerGroupedRatings.wideplayRating = GetGroupingScore(player, setPosition, movementPosition, WidePlayAttributes, values);
            LogDebug("Scoring");
            playerGroupedRatings.scoringRating = GetGroupingScore(player, setPosition, movementPosition, ScoringAttributes, values);
            LogDebug("Defending");
            playerGroupedRatings.defendingRating = GetGroupingScore(player, setPosition, movementPosition, DefendingAttributes, values);
            LogDebug("Goalkeeping");
            playerGroupedRatings.goalkeepingRating = GetGroupingScore(player, setPosition, movementPosition, GoalkeepingAttributes, values);
            LogDebug("Speed");
            playerGroupedRatings.speedRating = GetGroupingScore(player, setPosition, movementPosition, SpeedAttributes, values);
            LogDebug("Strength");
            playerGroupedRatings.strengthRating = GetGroupingScore(player, setPosition, movementPosition, StrengthAttributes, values);

            return playerGroupedRatings;
        }

        private byte RatePlayerInRole(Player player, PlayerPosition type, Roles role, byte[] weights, GroupedRatings playerGroupedRatings, out RatingRoleDebug debug)
        {
            /*
            byte mentalWeight = weights.GW(DP.MentalityWeight);
            byte physicalWeight = weights.GW(DP.PhysicalityWeight);
            byte technicalWeight = weights.GW(DP.TechnicalWeight);


            var mental = GetGroupingScore(MentalAttributes, values, weights, false, out mentalDebugString);
            var physical = GetGroupingScore(PhysicalAttributes, values, weights, false, out physicalDebugString);
            var technical = GetGroupingScore(TechnicalAttributes, values, weights, true, out technicalDebugString);

            decimal mentalScore = Weight(mental, mentalWeight);
            decimal physicalScore = Weight(physical, physicalWeight, weights.GW(DP.PhysicalInflation));
            decimal technicalScore = Weight(technical, technicalWeight, weights.GW(DP.TechnicalInflation));

            decimal adjust = (decimal)(mentalWeight + physicalWeight + technicalWeight) / 100;
            */



            decimal mentalRating = ApplyWeightToGroup(playerGroupedRatings.impactRating, weights[(int)AG.Impact]) + ApplyWeightToGroup(playerGroupedRatings.reliabilityRating, weights[(int)AG.Reliability]);
            decimal mentalWeighting = weights[(int)AG.Impact] + weights[(int)AG.Reliability];

            //mentalRating = Math.Min(mentalRating * 1.2m, mentalWeighting);

            // const decimal fb_wp = 1.1m;
            decimal wp_adj = 1;

            /*
            switch (type)
            {
                case PlayerType.RightBack:
                case PlayerType.LeftBack:
                    wp_adj = fb_wp;
                    break;
            }*/

            // const decimal pm_adj_all = 1.08m;
            decimal pm_adj = 1;

            /*
            switch (type)
            {
                default:
                    pm_adj = pm_adj_all;
            }*/

            decimal technicalRating = ApplyWeightToGroup((byte)(playerGroupedRatings.playmakingRating * pm_adj), weights[(int)AG.Playmaking])
                + ApplyWeightToGroup((byte)(playerGroupedRatings.wideplayRating * wp_adj), weights[(int)AG.Wideplay])
                + ApplyWeightToGroup(playerGroupedRatings.scoringRating, weights[(int)AG.Scoring]) + ApplyWeightToGroup(playerGroupedRatings.defendingRating, weights[(int)AG.Defending])
                + ApplyWeightToGroup(playerGroupedRatings.goalkeepingRating, weights[(int)AG.Goalkeeping]);
            decimal technicalWeighting = weights[(int)AG.Playmaking] + weights[(int)AG.Wideplay] + weights[(int)AG.Scoring] + weights[(int)AG.Defending] + weights[(int)AG.Goalkeeping];

            /*
            switch (role)
            {
                case Roles.GK:
                    technicalRating = technicalRating * 0.85m;
                    break;

                case Roles.CB:
                    technicalRating = technicalRating * 0.85m;
                    break;

                case Roles.DFB:
                    technicalRating = technicalRating * 0.9m;
                    break;

                case Roles.AFB:
                    technicalRating = technicalRating * 0.95m;
                    break;

                case Roles.TM:
                    technicalRating = technicalRating * 0.95m;
                    break;

                default:
                    technicalRating = Math.Min(technicalRating * 1.2m, technicalWeighting);
                    break;
            }*/

            decimal physicalRating = ApplyWeightToGroup(playerGroupedRatings.speedRating, weights[(int)AG.Speed]) + ApplyWeightToGroup(playerGroupedRatings.strengthRating, weights[(int)AG.Strength]);
            decimal physicalWeighting = weights[(int)AG.Speed] + weights[(int)AG.Strength];

            //physicalRating = Math.Min(physicalRating * 1.15m, physicalWeighting);

            decimal rating = mentalRating + technicalRating + physicalRating;

            /*
            const decimal fb_adj = 1.1m;
            const decimal dm_adj = 1.15m;
            const decimal cm_adj = 1.14m;
            const decimal am_adj = 1.1m;
            const decimal wm_adj = 1.06m;
            const decimal wg_adj = 1.08m;
            const decimal fw_adj = 1.15m;
            */

            decimal rat_adj = 1m;

            /*
            switch (type)
            {
                case PlayerType.RightBack:
                case PlayerType.LeftBack:
                    rat_adj = fb_adj;
                    break;

                case PlayerType.DefensiveMidfielder:
                    rat_adj = dm_adj;
                    break;

                case PlayerType.CentralMidfielder:
                    rat_adj = cm_adj;
                    break;

                case PlayerType.AttackingMidfielder:
                    rat_adj = am_adj;
                    break;

                case PlayerType.RightMidfielder:
                case PlayerType.LeftMidfielder:
                    rat_adj = wm_adj;
                    break;

                case PlayerType.RightWinger:
                case PlayerType.LeftWinger:
                    rat_adj = wg_adj;
                    break;

                case PlayerType.CentreForward:
                    switch (role)
                    {
                        case Roles.TM:
                            break;
                        default:
                            rat_adj = fw_adj;
                            break;
                    }
                    break;
            }*/

            rating = Math.Min(99, rat_adj * rating);

            /*
            switch (role)
            {
                case Roles.HM:
                    if (type == PlayerType.CentralMidfielder)
                    {
                        rating = rating * 0.95m;
                    }
                    break; 

                case Roles.PO:
                    rating = rating * 0.95m;
                    break;
            }*/

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

        private byte GetGroupingScore(Player player, PlayerPosition setPosition, PlayerPosition movementPosition, AttributeWeight[] attributes, byte[] values)
        {
            decimal rating = 0;
            int combinedWeights = 0;
            byte realValue;

            foreach (var i in attributes)
            {
                byte weight = (byte)i.Weight;

                if (weight == 0)
                {
                    continue;
                }

                combinedWeights += weight;
                realValue = values[(int)i.Attribute];
                string attribute = Enum.GetNames(typeof(DP))[(int)i.Attribute];

                decimal value = Adj(realValue, i.IsIntrinsic, i.Attribute, player._player, setPosition, movementPosition);

                /*
                decimal cappedValue = Math.Min(20, value);
                if (value > 20)
                {
                    decimal remainder = (value - 20) / 2;
                    cappedValue += remainder;
                }

                decimal weightedValue = cappedValue * weight;*/
                decimal weightedValue = value * weight;

                rating += weightedValue;

                LogDebug($"{attribute} real={realValue} masked={value} weighted={weightedValue} ({weight})");
            }

            int maxScore = 20 * combinedWeights;

            var result = GetRatingAdjustedForMaxScoreDifficulty(rating, maxScore);

            LogDebug($"{rating} out of {maxScore} = {result}");

            return result;
        }

        private byte GetRatingAdjustedForMaxScoreDifficulty(decimal rating, int maxScore)
        {
            /*
            const int maxScoreMidpoint = 300;
            const int curveWeight = 800;
            */

            decimal ratio = rating / maxScore;
            int easyratio = (int)(ratio * 100);

            return Math.Min((byte)99, (byte)easyratio);

            /*
            int flaw = (int)(100 - (100 * ratio));
            int flawweight = maxScore - maxScoreMidpoint;
            decimal adjuster = (decimal)flawweight / (curveWeight - (maxScore / 2.5m));
            int scoreadjust = (int)(flaw * adjuster);
            return Math.Min((byte)99, (byte)(easyratio + scoreadjust));*/
        }

        private byte GetGroupingScore(byte[] attributes, byte[] values, byte[] weights, bool isIntrinsic, Player player, PlayerPosition setPosition, PlayerPosition movementPosition, out string debugString)
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

                decimal value = Adj(realValue, isIntrinsic, (DP)i, player._player, setPosition, movementPosition);

                decimal cappedValue = Math.Min(20, value);
                if (value > 20)
                {
                    decimal remainder = (value - 20) / 2;
                    cappedValue += remainder;
                }

                decimal weightedValue = cappedValue * weight;

                rating += weightedValue;

                debugString += $"{attribute} : {value}-{weight}({realValue}) ";
            }

            int maxScore = 20 * combinedWeights;

            var result = (byte)((rating / maxScore) * 100);
            return Math.Min((byte)99, result);
        }

        private decimal Adj(byte val, bool isIntrinsic, DP attribute, PlayerData player, PlayerPosition setPosition, PlayerPosition movementPosition)
        {
            if (!isIntrinsic)
            {
                return Math.Min((byte)20, val);
            }

            return masker.GetIntrinsicMask(player.CurrentAbility, player, player.Versatility, attribute, setPosition, movementPosition, val);
        }

        private byte[] GetWeights(Roles role)
        {
            return groupedWeightings[(int)role];
        }

    }
}
