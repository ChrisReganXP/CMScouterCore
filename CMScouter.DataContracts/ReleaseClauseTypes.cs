using System.ComponentModel;

namespace CMScouter.DataClasses
{
    public enum ReleaseClauseType
    {
        None,

        [Description("Non Promotion")]
        NonPromotion,

        [Description("Manager Job")]
        ManagerJob,

        Relegation,

        [Description("Minimum Fee")]
        MinimumFee,

        [Description("Not Playing")]
        NotPlaying,
    }
}
