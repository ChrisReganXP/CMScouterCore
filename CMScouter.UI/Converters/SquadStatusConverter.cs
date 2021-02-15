using CMScouter.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMScouter.UI.Converters
{
    public static class SquadStatusConverter
    {
        public static string ConvertSquadStatus(byte rangeStatus, short age)
        {
            var lastSquadStatusString = string.Empty;

            List<SquadStatus> statusList = Enum.GetValues(typeof(SquadStatus)).Cast<SquadStatus>().ToList();

            if (age >= 24)
            {
                statusList.Remove(SquadStatus.HotProspect);
                statusList.Remove(SquadStatus.YoungPlayer);
            }

            foreach (SquadStatus status in statusList)
            {
                lastSquadStatusString = status.ToName();

                if (rangeStatus <= (int)status)
                {
                    break;
                }
            }

            return lastSquadStatusString;
        }
    }
}
