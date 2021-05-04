using CMScouter.DataClasses;
using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI
{
    public interface IIntrinsicMasker
    {
        byte GetIntrinsicBasicMask(byte val, short currentAbility);

        decimal GetIntrinsicMask(PlayerData player, DP attribute, PlayerPosition setPosition, PlayerPosition movementPosition, byte intrinsicValue);
    }
}
