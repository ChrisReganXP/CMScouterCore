using CMScouter.DataClasses;

namespace CMScouter.UI
{
    public class ScoutingRequest
    {
        public string ClubName { get; set; }

        public byte MinAge { get; set; }

        public byte MaxAge { get; set; }

        public int? Nationality { get; set; }

        public bool EUNationalityOnly { get; set; }

        public int? MinValue { get; set; }

        public int? MaxValue { get; set; }

        public short? ContractStatus { get; set; }

        public PlayerType? PlayerType { get; set; }

        public string PlaysInCountry { get; set; }

        public string PlaysInRegion { get; set; }

        public int? PlaysInDivision { get; set; }

        public short NumberOfResults { get; set; }
    }
}
