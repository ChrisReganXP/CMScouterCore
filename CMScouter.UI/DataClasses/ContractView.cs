using CMScouter.DataClasses;

namespace CMScouter.UI
{
    public class ContractView
    {
        public byte SquadStatusValue { get; set; }

        public string SquadStatus { get; set; }

        public string TransferStatus { get; set; }

        public ReleaseClauseType ReleaseClause { get; set; }

        public int ReleaseValue { get; set; }
    }
}
