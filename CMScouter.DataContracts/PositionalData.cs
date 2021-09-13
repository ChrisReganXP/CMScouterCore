using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.DataContracts
{
    public abstract class PositionalData
    {
        public byte GK { get; set; }

        public byte SW { get; set; }

        public byte DF { get; set; }

        public byte DM { get; set; }

        public byte MF { get; set; }

        public byte AM { get; set; }

        public byte ST { get; set; }

        public byte WingBack { get; set; }

        public byte FreeRole { get; set; }

        public byte Right { get; set; }

        public byte Left { get; set; }

        public byte Centre { get; set; }
    }
}
