using CMScouter.DataClasses;
using CMScouter.DataContracts;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.UI
{
    interface IPositionalPenaltyCalculator
    {
        decimal ApplyPositionPenalty(decimal rating, PositionalData player, byte Versatility, PlayerPosition position, decimal penaltyReduction);
    }
}
