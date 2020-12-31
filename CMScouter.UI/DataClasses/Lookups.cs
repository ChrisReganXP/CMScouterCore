using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI
{
    public class Lookups
    {

        public Dictionary<int, string> firstNames { get; set; }

        public Dictionary<int, string> secondNames { get; set; }

        public Dictionary<int, string> commonNames { get; set; }

        public Dictionary<int, NationView> nations { get; set; }

        public Dictionary<int, string> clubNames { get; set; }
    }
}
