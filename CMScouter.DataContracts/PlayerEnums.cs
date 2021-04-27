using System;
using System.Collections.Generic;
using System.ComponentModel;
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

    public enum PlayerPosition
    {
        [LinkedRoles(Roles.GK)]
        GoalKeeper,

        [Description("Right Back")]
        [LinkedRoles(Roles.AFB, Roles.DFB)]
        RightBack,

        [Description("Centre Half")]
        [LinkedRoles(Roles.CB)]
        CentreHalf,

        [Description("Left Back")]
        [LinkedRoles(Roles.AFB, Roles.DFB)]
        LeftBack,

        [Description("Right Wing Back")]
        [LinkedRoles(Roles.AFB)]
        RightWingBack,

        [Description("Defensive Midfielder")]
        [LinkedRoles(Roles.HM, Roles.CM)]
        DefensiveMidfielder,

        [Description("Left Wing Back")]
        [LinkedRoles(Roles.AFB)]
        LeftWingBack,

        [Description("Central Midfielder")]
        [LinkedRoles(Roles.HM, Roles.CM)]
        CentralMidfielder,

        [Description("Attacking Midfielder")]
        [LinkedRoles(Roles.AM)]
        AttackingMidfielder,

        [Description("Right Midfielder")]
        [LinkedRoles(Roles.WM, Roles.WG)]
        RightMidfielder,

        [Description("Left Midfielder")]
        [LinkedRoles(Roles.WM, Roles.WG)]
        LeftMidfielder,

        [Description("Right Winger")]
        [LinkedRoles(Roles.WG)]
        RightWinger,

        [Description("Left Winger")]
        [LinkedRoles(Roles.WG)]
        LeftWinger,

        [Description("Centre Forward")]
        [LinkedRoles(Roles.TM, Roles.ST, Roles.PO, Roles.CF)]
        CentreForward,
    }

    public enum Roles
    {
        [Description("Goal Keeper")]
        GK,

        [Description("Sweeper")]
        SW,

        [Description("Defensive Full Back")]
        DFB,

        [Description("Centre Half")]
        CB,

        [Description("Attacking Full Back")]
        AFB,

        [Description("Wing Back")]
        WB,

        [Description("Holding Midfielder")]
        HM,

        [Description("Central Midfielder")]
        CM,

        [Description("Wide Midfielder")]
        WM,

        [Description("Winger")]
        WG,

        [Description("Attacking Midfielder")]
        AM,

        [Description("Target Man")]
        TM,

        [Description("Striker")]
        ST,

        [Description("Poacher")]
        PO,

        [Description("Centre Forward")]
        CF,
    }
}
