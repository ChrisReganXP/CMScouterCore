using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CMScouter.DataContracts
{
    public enum ActualRegion
    {
        [Description("Unknown")]
        Unknown,

        [Description("Africa")]
        Africa,

        [Description("Asia")]
        Asia,

        [Description("Caribbean")]
        Caribbean,

        [Description("Central America")]
        CentralAmerica,

        [Description("Central Europe")]
        CentralEurope,

        [Description("Eastern Europe")]
        EasternEurope,

        [Description("Middle East")]
        MiddleEast,

        [Description("North Africa")]
        NorthAfrica,

        [Description("North America")]
        NorthAmerica,

        [Description("Oceania")]
        Oceania,

        [Description("Scandinavia")]
        Scandinavia,

        [Description("South America")]
        SouthAmerica,

        [Description("Southern Europe")]
        SouthernEurope,

        [Description("UK & Ireland")]
        UKandIreland,
    }
}
