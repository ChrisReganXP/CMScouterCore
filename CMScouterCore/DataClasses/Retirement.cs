using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouterCore.DataClasses
{
    public class Retirement
    {
        public int StaffId { get; set; }

        public DateTime? RetirementDecisionDate { get; set; }

        public DateTime? PlannedRetirementDate { get; set; }

        public string byteArray { get; set; }
    }
}
