using System.ComponentModel;

namespace CMScouter.DataClasses
{
    public enum SquadStatus
    {
        [Description("Indispensible")]
        Indispensible = 26,

        [Description("First Team")]
        FirstTeam = 39,

        [Description("Squad Rotation")]
        SquadRotation = 58,

        [Description("Backup")]
        Backup = 74,

        [Description("Hot Prospect")]
        HotProspect = 90,

        [Description("Young Player")]
        YoungPlayer = 106,

        [Description("Not Needed")]
        NotNeeded = 122,
    }
}
