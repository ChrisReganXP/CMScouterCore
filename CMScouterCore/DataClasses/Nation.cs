using CMScouter.DataContracts;
using System;

namespace CMScouterFunctions.DataClasses
{
    public class Nation
    {
        [DataFileInfo(0)]
        public int Id { get; set; }

        [DataFileInfo(4, 50)]
        public string Name { get; set; }

        [DataFileInfo(56, 25)]
        public string ShortName { get; set; }

        [DataFileInfo(83, 3)]
        public string ThreeLetterName { get; set; }

        [DataFileInfo(87, 25)]
        public string Nationality { get; set; }

        [DataFileInfo(113, 4)]
        public Continent Continent { get; set; }

        [DataFileInfo(117, 1)]
        public Region Region { get; set; }

        [DataFileInfo(118, 1)]
        public ActualRegion ActualRegion { get; set; }

        [DataFileInfo(119, 1)]
        public Language Language { get; set; }

        [DataFileInfo(120, 1)]
        public Language SecondLanguage { get; set; }

        [DataFileInfo(121, 1)]
        public Language ThirdLanguage { get; set; }

        [DataFileInfo(122, 4)]
        public int CapitalCityId { get; set; }

        [DataFileInfo(126, 1)]
        public DevelopmentLevel DevelopmentLevel { get; set; }

        [DataFileInfo(127, 1)]
        public bool EUNation { get; set; }

        [DataFileInfo(128, 4)]
        public int StadiumId { get; set; }

        [DataFileInfo(132, 1)]
        public byte GameImportance { get; set; }

        [DataFileInfo(133, 1)]
        public byte LeagueStandard { get; set; }

        [DataFileInfo(134, 2)]
        public Int16 NumberOfClubs { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}
