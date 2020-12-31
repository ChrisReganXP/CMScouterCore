using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI
{
    public class PlayerPositionView
    {

        public short GK { get; set; }

        public short SW { get; set; }

        public short DF { get; set; }

        public short DM { get; set; }

        public short MF { get; set; }

        public short AM { get; set; }

        public short ST { get; set; }

        public short WingBack { get; set; }

        public short FreeRole { get; set; }

        public short Right { get; set; }

        public short Left { get; set; }

        public short Centre { get; set; }

        public string DescribedPosition
        {
            get
            {
                if (GK == 20)
                {
                    return "GK";
                }

                var position = string.Empty;
                if (DF >= 15)
                {
                    position = "D";
                }
                if (DM >= 15)
                {
                    position += (!string.IsNullOrWhiteSpace(position)) ? "/DM" : "DM";
                }
                if (MF >= 15)
                {
                    position += (!string.IsNullOrWhiteSpace(position)) ? "/M" : "M";
                }
                if (AM >= 15)
                {
                    position += (!string.IsNullOrWhiteSpace(position)) ? "/AM" : "AM";
                }
                if (ST >= 15)
                {
                    position += (!string.IsNullOrWhiteSpace(position)) ? "/F" : "ST";
                }

                position += " ";

                if (Right >= 15)
                {
                    position += "R";
                }
                if (Left >= 15)
                {
                    position += "L";
                }
                if (Centre >= 15)
                {
                    position += "C";
                }
                return position;
            }
        }
    }
}
