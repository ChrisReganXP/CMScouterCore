using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouterFunctions.DataClasses
{
    public class Club
    {
        [DataFileInfo(0)]
        public int ClubId { get; set; }

        [DataFileInfo(4, 50)]
        public string LongName { get; set; }

        [DataFileInfo(56, 25)]
        public string Name { get; set; }

        [DataFileInfo(83)]
        public int NationId { get; set; }

        [DataFileInfo(87)]
        public int DivisionId { get; set; }

        public Club()
        { }

        public Club(int clubId, string name)
        {
            ClubId = clubId;
            Name = name;
        }
    }
}
