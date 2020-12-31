using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.UI.Raters
{
    public class AttributeWeight
    {
        public DP Attribute { get; set; }

        public int Weight { get; set; }

        public bool IsIntrinsic { get; set; }
    }

    public enum AG
    {
        Impact,

        Reliability,

        Playmaking,

        Wideplay,

        Scoring,

        Defending,

        Goalkeeping,

        Speed,

        Strength,
    }
}
