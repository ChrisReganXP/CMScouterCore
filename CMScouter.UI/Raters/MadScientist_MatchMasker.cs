using CMScouter.DataClasses;
using CMScouterFunctions.DataClasses;
using System;

namespace CMScouter.UI.Raters
{
    public class MadScientist_MatchMasker : IIntrinsicMasker
    {
        public byte GetIntrinsicBasicMask(byte val, short currentAbility)
        {
            decimal valueAspect = (val - 128) / 5;
            decimal abilityAspect = currentAbility / 20;
            byte otherAspect = 10;

            return (byte)Math.Min(99, Math.Max(1, Math.Round(valueAspect + abilityAspect + otherAspect)));
        }

        public byte GetIntrinsicMask(PlayerData player, DP attribute, PlayerPosition setPosition, PlayerPosition movementPosition, byte val)
        {
            byte rating = GetIntrinsicBasicMask(val, player.CurrentAbility);

            AdjustForSetPosition(rating, player, attribute, setPosition);

            if (movementPosition != setPosition)
            {
                AdjustForMovementPosition(rating, player, attribute, movementPosition);
            }

            return rating;
        }

        private void AdjustForSetPosition(byte rating, PlayerData player, DP attribute, PlayerPosition position)
        {
            byte maxPenalty = 20;
            AdjustForPosition(rating, player, attribute, position, maxPenalty);
        }

        private void AdjustForMovementPosition(byte rating, PlayerData player, DP attribute, PlayerPosition position)
        {
            byte maxPenalty = 5;
            AdjustForPosition(rating, player, attribute, position, maxPenalty);
        }

        private void AdjustForPosition(byte rating, PlayerData player, DP attribute, PlayerPosition position, byte maxPenalty)
        {
            switch (attribute)
            {
                case DP.Positioning:
                case DP.Marking:
                case DP.Anticipation:
                case DP.OffTheBall:
                case DP.Creativity:
                case DP.Decisions:
                    ApplyPositionPenalty(rating, player, position, maxPenalty);
                    break;

                default:
                    return;
            }
        }

        private void ApplyPositionPenalty(byte rating, PlayerData player, PlayerPosition position, decimal penaltyReduction)
        {
            byte verticalPenalty = GetVerticalUnfamiliarity(player, position);
            byte horizontalPenalty = GetHorizontalUnfamiliarity(player, position);

            decimal initialPenalty = (verticalPenalty + horizontalPenalty) * penaltyReduction / 2;
            decimal finalPenalty = (1.079m - player.Versatility * 0.0395m) * initialPenalty;

            rating = (byte)Math.Max(1, Math.Round(rating - finalPenalty));
        }

        private byte GetHorizontalUnfamiliarity(PlayerData player, PlayerPosition position)
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

        private byte GetVerticalUnfamiliarity(PlayerData player, PlayerPosition position)
        {
            byte knowledge = 20;
            byte difficulty = 0;

            switch (position)
            {
                case PlayerPosition.GoalKeeper:
                    knowledge = player.GK;
                    difficulty = 10;
                    break;

                case PlayerPosition.LeftBack:
                case PlayerPosition.CentreHalf:
                case PlayerPosition.RightBack:
                    knowledge = player.DF;
                    difficulty = 5;
                    break;

                case PlayerPosition.LeftWingBack:
                case PlayerPosition.RightWingBack:
                    knowledge = player.WingBack;
                    difficulty = 3;
                    break;

                case PlayerPosition.LeftMidfielder:
                case PlayerPosition.CentralMidfielder:
                case PlayerPosition.RightMidfielder:
                    knowledge = player.MF;
                    difficulty = 2;
                    break;

                case PlayerPosition.LeftWinger:
                case PlayerPosition.AttackingMidfielder:
                case PlayerPosition.RightWinger:
                    knowledge = player.AM;
                    difficulty = 1;
                    break;

                case PlayerPosition.CentreForward:
                    knowledge = player.ST;
                    break;
            }

            var basicPenalty = (byte)(20 - knowledge);
            return (byte)Math.Min(20, basicPenalty + difficulty);
        }
    }
}
