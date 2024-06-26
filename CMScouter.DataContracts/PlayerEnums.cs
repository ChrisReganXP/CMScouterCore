﻿using System;
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
        [LinkedRoles(Roles.CB, Roles.LD)]
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

        [Description("Right Midfielder")]
        [LinkedRoles(Roles.WM, Roles.WG)]
        RightMidfielder,

        [Description("Central Midfielder")]
        [LinkedRoles(Roles.HM, Roles.CM, Roles.PM)]
        CentralMidfielder,

        [Description("Left Midfielder")]
        [LinkedRoles(Roles.WM, Roles.WG)]
        LeftMidfielder,

        [Description("Right Winger")]
        [LinkedRoles(Roles.WG)]
        RightWinger,

        [Description("Attacking Midfielder")]
        [LinkedRoles(Roles.AM, Roles.CM)]
        AttackingMidfielder,

        [Description("Left Winger")]
        [LinkedRoles(Roles.WG)]
        LeftWinger,

        [Description("Centre Forward")]
        [LinkedRoles(Roles.TM, Roles.ST, Roles.PO)]
        CentreForward,
    }

    public enum Roles
    {
        // 0
        [Description("Goal Keeper")]
        GK,
        
        // 1
        [Description("Sweeper")]
        SW,

        // 2
        [Description("Defensive Full Back")]
        DFB,

        // 3
        [Description("Centre Half")]
        CB,

        // 4
        [Description("Limited Defender")]
        LD,

        // 5
        [Description("Attacking Full Back")]
        AFB,

        [Description("Wing Back")]
        WB,

        [Description("Holding Midfielder")]
        HM,

        [Description("Central Midfielder")]
        CM,

        [Description("Playmaker")]
        PM,

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
        PO
    }
}
