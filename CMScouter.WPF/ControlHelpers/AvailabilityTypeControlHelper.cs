using CMScouter.DataClasses;
using CMScouter.UI.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.WPF.ControlHelpers
{
    public static class AvailabilityTypeControlHelper
    {
        private const string UnwantedKey = "UWA";

        private const string LoanListedKey = "LFL";

        private const string SixMonthExpiry = "6MO";

        private const string TwelveMonthExpiry = "12M";

        public static List<KeyValuePair<string, string>> GetAvailabilityTypeKeyValuePairs()
        {
            var availabilityTypes = new List<KeyValuePair<string, string>>();
            availabilityTypes.Add(new KeyValuePair<string, string>("ALL", "<All>"));
            availabilityTypes.Add(new KeyValuePair<string, string>(UnwantedKey, "Transfer Listed / Unwanted"));
            availabilityTypes.Add(new KeyValuePair<string, string>(SixMonthExpiry, "Expires 6 Months"));
            availabilityTypes.Add(new KeyValuePair<string, string>(TwelveMonthExpiry, "Expires 12 Months"));
            availabilityTypes.Add(new KeyValuePair<string, string>(LoanListedKey, "Listed For Loan"));

            return availabilityTypes;
        }

        public static void GetSearchCriteria(string key, out AvailabilityCriteria criteria/*, out SquadStatus squadStatus, out TransferStatus transferStatus, out short? contractMonths*/)
        {
            criteria = new AvailabilityCriteria();
            /*
            squadStatus = SquadStatus.Indispensible;
            transferStatus = TransferStatus.NotSet;
            contractMonths = null;*/

            switch (key)
            {
                case UnwantedKey:
                    criteria.TransferListed = true;
                    criteria.UnwantedSquadStatus = true;
                    /*
                    squadStatus = SquadStatus.Backup;
                    transferStatus = TransferStatus.TransferListed;*/
                    break;

                case LoanListedKey:
                    criteria.LoanListed = true;
                    /*
                    transferStatus = TransferStatus.ListedForLoan;*/
                    break;

                case SixMonthExpiry:
                    criteria.ContractMonths = 6;
                    /*
                    contractMonths = 6;*/
                    break;

                case TwelveMonthExpiry:
                    criteria.ContractMonths = 12;
                    /*
                    contractMonths = 12;*/
                    break;
            }
        }
    }
}
