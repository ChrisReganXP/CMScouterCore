using CMScouter.DataClasses;
using CMScouterFunctions.DataClasses;
using System;
using System.Diagnostics;

namespace CMScouter.UI.Raters
{
    public class MadScientist_MatchMasker : IIntrinsicMasker
    {
        private IPositionalPenaltyCalculator positionalPenaltyCalculator;

        private bool outputDebug;

        private void LogDebug(string debug)
        {
            if (!outputDebug)
            {
                return;
            }

            Debug.WriteLine(debug);
        }

        public MadScientist_MatchMasker()
        {
            positionalPenaltyCalculator = new MadScientist_PenaltyCalculator();
        }

        public byte GetIntrinsicBasicMask(byte val, short currentAbility)
        {
            return (byte)Math.Round(GetIntrinsicMaskedByAbility(val, currentAbility));
        }

        private decimal GetIntrinsicMaskedByAbility(byte val, short currentAbility)
        {
            decimal valueAspect = (decimal)(val - 128) / 5;
            decimal abilityAspect = (decimal)currentAbility / 20;
            byte otherAspect = 10;

            return Math.Min(99, Math.Max(1, valueAspect + abilityAspect + otherAspect));
        }

        public decimal GetIntrinsicMask(PlayerData player, DP attribute, PlayerPosition setPosition, PlayerPosition movementPosition, byte val)
        {
            decimal ratingCA = GetIntrinsicMaskedByAbility(val, player.CurrentAbility);

            decimal ratingPos = AdjustForSetPosition(ratingCA, player, attribute, setPosition);

            decimal ratingMove = -1;
            if (movementPosition != setPosition)
            {
               ratingMove = AdjustForMovementPosition(ratingPos, player, attribute, movementPosition);
            }

            LogDebug($"CA rating = {ratingCA}, Pos Rating = {ratingPos}, Move Rating {(ratingMove == -1 ? ratingPos : ratingMove)}");
            return Math.Min(45, ratingMove == -1 ? ratingPos : ratingMove);
        }

        private decimal AdjustForSetPosition(decimal rating, PlayerData player, DP attribute, PlayerPosition position)
        {
            byte maxPenalty = 20;
            return AdjustForPosition(rating, player, attribute, position, maxPenalty);
        }

        private decimal AdjustForMovementPosition(decimal rating, PlayerData player, DP attribute, PlayerPosition position)
        {
            byte maxPenalty = 5;
            return AdjustForPosition(rating, player, attribute, position, maxPenalty);
        }

        private decimal AdjustForPosition(decimal rating, PlayerData player, DP attribute, PlayerPosition position, byte maxPenalty)
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
