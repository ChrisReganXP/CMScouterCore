﻿using CMScouter.DataClasses;
using CMScouter.DataContracts;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.UI.Raters
{
    class MadScientist_PenaltyCalculator : IPositionalPenaltyCalculator
    {
        public decimal ApplyPositionPenalty(decimal rating, PositionalData player, byte Versatility, PlayerPosition position, decimal penaltyReduction)
        {
            byte verticalPenalty = GetVerticalUnfamiliarity(player, position);
            byte horizontalPenalty = GetHorizontalUnfamiliarity(player, position);

            decimal initialPenalty = ((decimal)verticalPenalty + horizontalPenalty) / 40 * penaltyReduction;
            decimal finalPenalty = (1.079m - Versatility * 0.0395m) * initialPenalty;

            return Math.Max(1, rating - finalPenalty);
        }

        private byte GetHorizontalUnfamiliarity(PositionalData player, PlayerPosition position)
        {
            byte knowledge = 20;

            switch (position)
            {
                case PlayerPosition.RightBack:
                case PlayerPosition.RightWingBack:
                case PlayerPosition.RightMidfielder:
                case PlayerPosition.RightWinger:
                    knowledge = player.Right;
                    break;

                case PlayerPosition.CentreHalf:
                case PlayerPosition.DefensiveMidfielder:
                case PlayerPosition.CentralMidfielder:
                case PlayerPosition.AttackingMidfielder:
                case PlayerPosition.CentreForward:
                    knowledge = player.Centre;
                    break;

                case PlayerPosition.LeftBack:
                case PlayerPosition.LeftWingBack:
                case PlayerPosition.LeftMidfielder:
                case PlayerPosition.LeftWinger:
                    knowledge = player.Left;
                    break;

                case PlayerPosition.GoalKeeper:
                    knowledge = player.GK;
                    break;
            }

            return (byte)(20 - knowledge);
        }

        private byte GetVerticalUnfamiliarity(PositionalData player, PlayerPosition position)
        {
            byte knowledge = 20;
            decimal difficulty = 1;

            switch (position)
            {
                case PlayerPosition.GoalKeeper:
                    knowledge = player.GK;
                    difficulty = 5m;
                    break;

                case PlayerPosition.LeftBack:
                case PlayerPosition.CentreHalf:
                case PlayerPosition.RightBack:
                    knowledge = player.DF;
                    difficulty = 2m;
                    break;

                case PlayerPosition.LeftWingBack:
                case PlayerPosition.RightWingBack:
                    knowledge = player.WingBack;
                    difficulty = 1.5m;
                    break;

                case PlayerPosition.DefensiveMidfielder:
                    knowledge = player.DM;
                    difficulty = 1.5m;
                    break;

                case PlayerPosition.LeftMidfielder:
                case PlayerPosition.CentralMidfielder:
                case PlayerPosition.RightMidfielder:
                    knowledge = player.MF;
                    difficulty = 1.2m;
                    break;

                case PlayerPosition.LeftWinger:
                case PlayerPosition.AttackingMidfielder:
                case PlayerPosition.RightWinger:
                    knowledge = player.AM;
                    difficulty = 1.1m;
                    break;

                case PlayerPosition.CentreForward:
                    knowledge = player.ST;
                    break;
            }

            var basicPenalty = (byte)(20 - knowledge);
            return (byte)Math.Min(20, basicPenalty * difficulty);
        }
    }
}
