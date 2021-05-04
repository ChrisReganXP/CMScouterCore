using System.ComponentModel;

namespace CMScouter.DataClasses
{
    public enum ReleaseClauseType
    {
        None,

        [Description("NP")]
        NonPromotion,

        [Description("MJ")]
        ManagerJob,

        [Description("RG")]
        Relegation,

        [Description("MF")]
        MinimumFee,

        [Description("NPl")]
        NotPlaying,
    }
}
