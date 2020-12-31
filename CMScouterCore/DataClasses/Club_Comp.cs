namespace CMScouterFunctions.DataClasses
{
    public class Club_Comp
    {
        [DataFileInfo(0)]
        public int Id { get; set; }

        [DataFileInfo(56, 25)]
        public string Name { get; set; }

        [DataFileInfo(4, 50)]
        public string LongName { get; set; }

        [DataFileInfo(83, 3)]
        public string Abbreviation { get; set; }

        [DataFileInfo(92)]
        public int NationId { get; set; }

    }
}
