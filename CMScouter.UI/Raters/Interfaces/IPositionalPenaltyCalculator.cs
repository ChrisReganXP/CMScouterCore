using CMScouter.DataClasses;
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
        decimal ApplyPositionPenalty(decimal rating, PlayerData player, PlayerPosition position, decimal penaltyReduction);
    }
}
