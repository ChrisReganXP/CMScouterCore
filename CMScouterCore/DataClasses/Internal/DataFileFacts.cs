using System;
using System.Collections.Generic;
using System.Linq;

namespace CMScouterFunctions.DataClasses
{
    internal enum DataFileType
    {
        Unknown,

        General,

        First_Names,

        Second_Names,

        Common_Names,

        Nations,

        Staff,

        Players,

        Clubs,

        Club_Comps,
    }

    internal class DataFileFact
    {
        public DataFileType Type { get; }

        public string Name { get; }

        public int DataSize { get; }

        public int StringLength { get; }

        public DataFileFact(DataFileType type, string name, int dataSize, int stringLength)
        {
            Type = type;
            Name = name;
            DataSize = dataSize;
            StringLength = stringLength;
        }
    }


    internal static class DataFileFacts
    {
        public static List<DataFileFact> GetDataFileFacts()
        {
            var facts = new List<DataFileFact>();
            facts.Add(new DataFileFact(DataFileType.First_Names, "first_names.dat", 60, 50));
            facts.Add(new DataFileFact(DataFileType.Second_Names, "second_names.dat", 60, 50));
            facts.Add(new DataFileFact(DataFileType.Common_Names, "common_names.dat", 60, 50));
            facts.Add(new DataFileFact(DataFileType.Staff, "staff.dat", 110, 0));
            facts.Add(new DataFileFact(DataFileType.Players, "player.dat", 70, 0));
            facts.Add(new DataFileFact(DataFileType.Clubs, "club.dat", 581, 0));
            facts.Add(new DataFileFact(DataFileType.Club_Comps, "club_comp.dat", 107, 0));
            facts.Add(new DataFileFact(DataFileType.Nations, "nation.dat", 290, 0));
            facts.Add(new DataFileFact(DataFileType.General, "general.dat", 3952, 0));

            return facts;
        }

        public static DataFileFact GetDataFileFact(string name)
        {
            var matchingFacts = GetDataFileFacts().FirstOrDefault(x => x.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
            return matchingFacts ?? new DataFileFact(DataFileType.Unknown, name, 0, 0);
        }
    }
}
