﻿using System;
using System.Collections.Generic;

namespace CMScouterFunctions.DataClasses
{
    public class SaveGameData
    {
        public string FileName { get; set; }

        public Dictionary<int, string> FirstNames { get; set; }

        public Dictionary<int, string> Surnames { get; set; }

        public Dictionary<int, string> CommonNames { get; set; }

        public Dictionary<int, Nation> Nations { get; set; }

        public Dictionary<int, Club> Clubs { get; set; }

        public Dictionary<int, Club_Comp> ClubComps { get; set; }

        public List<Player> Players { get; set; }

        public DateTime GameDate { get; set; }

        public decimal ValueMultiplier { get; set; }

        public List<string> LoadingFailures { get; set; }

        public SaveGameData()
        {
            LoadingFailures = new List<string>();
            FileName = string.Empty;
            ValueMultiplier = 1;
        }
    }
}
