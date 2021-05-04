using CMScouter.DataClasses;
using CMScouterFunctions.DataClasses;
using System;

namespace CMScouter.UI.Raters
{
    public class MadScientist_MatchMasker : IIntrinsicMasker
    {
        private IPositionalPenaltyCalculator positionalPenaltyCalculator;

        public MadScientist_MatchMasker()
        {
            positionalPenaltyCalculator = new MadScientist_PenaltyCalculator();
        }

        public byte GetIntrinsicBasicMask(byte val, short currentAbility)
        {
            decimal valueAspect = (decimal)(val - 128) / 5;
            decimal abilityAspect = (decimal)currentAbility / 20;
            byte otherAspect = 10;

            return (byte)Math.Min(99, Math.Max(1, Math.Round(valueAspect + abilityAspect + otherAspect)));
        }

        public byte GetIntrinsicMask(PlayerData player, DP attribute, PlayerPosition setPosition, PlayerPosition movementPosition, byte val)
        {
            byte rating = GetIntrinsicBasicMask(val, player.CurrentAbility);

            rating = AdjustForSetPosition(rating, player, attribute, setPosition);

            if (movementPosition != setPosition)
            {
                rating = AdjustForMovementPosition(rating, player, attribute, movementPosition);
            }

            return rating;
        }

        private byte AdjustForSetPosition(byte rating, PlayerData player, DP attribute, PlayerPosition position)
        {
            byte maxPenalty = 20;
            return AdjustForPosition(rating, player, attribute, position, maxPenalty);
        }

        private byte AdjustForMovementPosition(byte rating, PlayerData player, DP attribute, PlayerPosition position)
        {
            byte maxPenalty = 5;
            return AdjustForPosition(rating, player, attribute, position, maxPenalty);
        }

        private byte AdjustForPosition(byte rating, PlayerData player, DP attribute, PlayerPosition position, byte maxPenalty)
        {
            switch (attribute)
            {
                case DP.Positioning:
                case DP.Marking:
                case DP.Anticipation:
                case DP.OffTheBall:
                case DP.Creativity:
                case DP.Decisions:
                    return positionalPenaltyCalculator.ApplyPositionPenalty(rating, player, position, maxPenalty);

                default:
                    return rating;
            }
        }
    }
}
