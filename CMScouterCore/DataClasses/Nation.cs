namespace CMScouterFunctions.DataClasses
{
    public class Nation
    {
        [DataFileInfo(0)]
        public int Id { get; set; }

        [DataFileInfo(4, 50)]
        public string Name { get; set; }

        public bool EUNation { get; set; }

        public override string ToString()
        {
            return $"{Name} ({Id})";
        }
    }
}
