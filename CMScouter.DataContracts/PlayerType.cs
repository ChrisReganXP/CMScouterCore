using System;
using System.Collections.Generic;
using System.Linq;

namespace CMScouter.DataClasses
{
    public class LinkedRoles : Attribute
    {
        public List<Roles> Roles { get; set; }

        public LinkedRoles(params Roles[] roles)
        {
            Roles = roles.ToList();
        }
    }

    public enum PlayerType
    {
        [LinkedRoles(Roles.GK)]
        GoalKeeper,

        [LinkedRoles(Roles.AFB, Roles.DFB)]
        RightBack,

        [LinkedRoles(Roles.CB)]
        CentreHalf,

        [LinkedRoles(Roles.AFB, Roles.DFB)]
        LeftBack,

        [LinkedRoles(Roles.AFB)]
        RightWingBack,

        [LinkedRoles(Roles.HM, Roles.CM)]
        DefensiveMidfielder,

        [LinkedRoles(Roles.AFB)]
        LeftWingBack,

        [LinkedRoles(Roles.HM, Roles.CM)]
        CentralMidfielder,

        [LinkedRoles(Roles.AM)]
        AttackingMidfielder,

        [LinkedRoles(Roles.WM, Roles.WG)]
        RightMidfielder,

        [LinkedRoles(Roles.WM, Roles.WG)]
        LeftMidfielder,

        [LinkedRoles(Roles.WG)]
        RightWinger,

        [LinkedRoles(Roles.WG)]
        LeftWinger,

        [LinkedRoles(Roles.TM, Roles.ST, Roles.PO, Roles.CF)]
        CentreForward,
    }

    public enum Roles
    {
        GK,
        SW,
        DFB,
        CB,
        AFB,
        WB,
        HM,
        CM,
        WM,
        WG,
        AM,
        TM,
        ST,
        PO,
        CF,
    }
}
