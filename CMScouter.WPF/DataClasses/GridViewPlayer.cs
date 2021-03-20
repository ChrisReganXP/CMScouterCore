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

        public byte PurchaseRating { get; set; }

        public string BestPosition { get; set; }

        public string BestRole { get; set; }

        public byte ContractMonths { get; set; }

        public string Reputation { get; set; }

        public string SquadStatus { get; set; }

        public byte SquadStatusValue { get; set; }

        public string TransferStatus { get; set; }

        public string ReleaseFee { get; set; }

        public byte GoalkeepingRating { get; set; }

        public byte DefendingRating { get; set; }

        public byte PlaymakingRating { get; set; }

        public byte WidePlayRating { get; set; }

        public byte ScoringRating { get; set; }

        public byte ImpactRating { get; set; }

        public byte ReliabilityRating { get; set; }

        public byte StrengthRating { get; set; }

        public byte SpeedRating { get; set; }
    }
}
