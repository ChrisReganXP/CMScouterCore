using CMScouter.DataClasses;
using CMScouter.UI.DataClasses;

namespace CMScouter.UI
{
    public class ScoutingRequest
    {
        public int? PlayerId { get; set; }

        public int? ClubId { get; set; }

        public byte MinAge { get; set; }

        public byte MaxAge { get; set; }

        public long? MaxWage { get; set; }

        public int? Nationality { get; set; }

        public bool EUNationalityOnly { get; set; }

        public int? MinValue { get; set; }

        public int? MaxValue { get; set; }

        public short? ContractStatus { get; set; }

        public AvailabilityCriteria AvailabilityCriteria { get; set; }

        public PlayerPosition? PlayerType { get; set; }

        public string PlaysInCountry { get; set; }

        public string PlaysInRegion { get; set; }

        public int? PlaysInDivision { get; set; }

        public short NumberOfResults { get; set; }

        public int Reputation { get; set; }

        public bool OutputDebug { get; set; }
    }
}
