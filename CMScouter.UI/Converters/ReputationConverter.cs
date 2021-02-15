using CMScouter.DataContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.UI.Converters
{
    public static class ReputationConverter
    {
        public static string Convert(int reputation)
        {
            var lastReputation = string.Empty;

            foreach (ReputationStatus status in Enum.GetValues(typeof(ReputationStatus)))
            {
                if (reputation < (int)status)
                {
                    break;
                }

                lastReputation = status.ToName();
            }

            return lastReputation;
        }
    }
}
