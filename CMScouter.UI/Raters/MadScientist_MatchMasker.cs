using System;

namespace CMScouter.UI.Raters
{
    public class MadScientist_MatchMasker : IIntrinsicMasker
    {
        public byte GetIntrinsicMask(byte val, short currentAbility)
        {
            decimal valueAspect = (val - 128)/5;
            decimal abilityAspect = currentAbility / 20;
            byte otherAspect = 10;

            return (byte)Math.Min(99, Math.Max(1, Math.Round(valueAspect + abilityAspect + otherAspect)));
        }
    }
}
