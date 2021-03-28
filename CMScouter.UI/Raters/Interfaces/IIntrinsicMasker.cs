using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI
{
    public interface IIntrinsicMasker
    {
        byte GetIntrinsicMask(byte intrinsicValue, short currentAbility);
    }
}
