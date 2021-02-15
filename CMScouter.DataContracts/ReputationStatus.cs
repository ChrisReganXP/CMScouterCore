using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace CMScouter.DataContracts
{
    public enum ReputationStatus
    {
        [Description("All Time Great")]
        AllTimeGreat = 9200,

        [Description("World Class")]
        WorldClass = 8500,

        [Description("International")]
        International = 8000,

        [Description("Top Division")]
        TopDivision = 7000,

        [Description("Solid Pro")]
        SolidPro = 5000,

        [Description("Journey Man")]
        Journeyman = 3500,

        [Description("Reserve Team")]
        ReserveTeam = 2200,

        [Description("Semi Pro")]
        SemiPro = 1000,
        
        [Description("Amateur")]
        Amateur = 500,

        [Description("Nobody")]
        Nobody = 0,
    }
}
