using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.UI.DataClasses
{
    public class AvailabilityCriteria
    {
        public bool CriteriaANDLogic { get; set; }

        public bool TransferListed { get; set; }

        public bool UnwantedSquadStatus { get; set; }

        public bool SquadPlayerStatus { get; set; }

        public bool LoanListed { get; set; }

        public short? ContractMonths { get; set; }
    }
}
