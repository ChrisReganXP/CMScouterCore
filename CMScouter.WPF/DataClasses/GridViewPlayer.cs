using CMScouter.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.WPF.DataClasses
{
    public class GridViewPlayer
    {
        public int PlayerId { get; set; }

        public string Name { get; set; }

        public string ClubName { get; set; }

        public byte Age { get; set; }

        public short CurrentAbility { get; set; }

        public short PotentialAbility { get; set; }

        public int Value { get; set; }

        public int WagePerWeek { get; set; }

        public DateTime? ContractExpiryDate { get; set; }

        public byte Recommendation { get; set; }

        public byte ScoutedRating { get; set; }

        public string ScoutedRole { get; set; }

        public byte BestRating { get; set; }

        public string BestPosition { get; set; }

        public Roles BestRole { get; set; }

        public byte ContractMonths { get; set; }

        public string SquadStatus { get; set; }

        public string TransferStatus { get; set; }

        public string ReleaseFee { get; set; }
    }
}
