using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouterFunctions.DataClasses
{
    public class Contract
    {
        public int PlayerId { get; set; }

        //16
        /*
        public DateTime? DateJoined { get; set; }
        */

        public DateTime? ContractStartDate { get; set; }

        public DateTime? ContractEndDate { get; set; }

        public int WagePerWeek { get; set; }

        public int GoalBonus { get; set; }

        public int AssistBonus { get; set; }

        public bool NonPromotionReleaseClause { get; set; }

        public bool MinimumFeeReleaseClause { get; set; }

        public bool NonPlayingReleaseClause { get; set; }

        public bool RelegationReleaseClause { get; set; }

        public bool ManagerReleaseClause { get; set; }

        public int ReleaseClauseValue { get; set; }

        public byte TransferStatus { get; set; }

        public byte SquadStatus { get; set; }
    }
}
