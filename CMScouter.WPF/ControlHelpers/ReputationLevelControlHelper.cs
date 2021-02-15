using CMScouter.DataContracts;
using CMScouter.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.WPF.ControlHelpers
{
    public static class ReputationLevelControlHelper
    {
        public static List<KeyValuePair<int, string>> GetReputationLevelKeyValuePairs()
        {
            var repLevels = new List<KeyValuePair<int, string>>();

            foreach(ReputationStatus repLevel in Enum.GetValues(typeof(ReputationStatus)))
            {
                repLevels.Add(new KeyValuePair<int, string>((int)repLevel, repLevel.ToName()));
            }

            return repLevels;
        }

        public static int GetMaxLevelCriteria(int selectedReputation)
        {
            int maxReputation = 0;

            foreach (ReputationStatus status in Enum.GetValues(typeof(ReputationStatus)).Cast<ReputationStatus>().OrderByDescending(x => x))
            {
                if (selectedReputation >= (int)status)
                {
                    break;
                }

                maxReputation = (int)status;
            }

            maxReputation = maxReputation > 0 ? maxReputation - 1 : 0;

            return maxReputation;
        }
    }
}
