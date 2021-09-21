using CMScouter.DataClasses;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI.Raters
{
    public abstract class BaseRater
    {
        protected IIntrinsicMasker masker;

        protected void ScoreAttribute(DP attribute, PlayerData player, PlayerPosition setPosition, PlayerPosition movementPosition, byte playerAttribute, byte weight, ref int combinedWeight, ref decimal rating)
        {
            if (weight == 0)
            {
                return;
            }

            combinedWeight += weight;
            var value = Adj(playerAttribute, IsAttributeIntrinsic(attribute), attribute, player, setPosition, movementPosition);
            decimal weightedValue = value * weight;

            rating += weightedValue;
        }

        protected bool IsAttributeIntrinsic(DP attribute)
        {
            switch (attribute)
            {
                case DP.Anticipation:
                case DP.Creativity:
                case DP.Crossing:
                case DP.Decisions:
                case DP.Dribbling:
                case DP.Finishing:
                case DP.Heading:
                case DP.LongShots:
                case DP.Marking:
                case DP.OffTheBall:
                case DP.Passing:
                case DP.Positioning:
                case DP.Tackling:
                case DP.Penalties:
                case DP.ThrowIns:
                case DP.Handling:
                case DP.OneOnOnes:
                case DP.Reflexes:
                    return true;
                default:
                    return false;
            }
        }

        protected decimal Adj(byte val, bool isIntrinsic, DP attribute, PlayerData player, PlayerPosition setPosition, PlayerPosition movementPosition)
        {
            if (!isIntrinsic)
            {
                return Math.Min((byte)20, val);
            }

            return masker.GetIntrinsicMask(player.CurrentAbility, player, player.Versatility, attribute, setPosition, movementPosition, val);
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
    }
}
