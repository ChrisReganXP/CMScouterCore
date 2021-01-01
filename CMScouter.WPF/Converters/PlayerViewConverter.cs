using CMScouter.UI;
using CMScouter.WPF.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.WPF.Converters
{
    public static class PlayerViewConverter
    {
        public static GridViewPlayer ConvertViewToGrid(PlayerView source)
        {
            GridViewPlayer dest = new GridViewPlayer();

            dest.PlayerId = source.PlayerId;
            dest.Name = source.GetKnownName();
            dest.ClubName = source.ClubName;
            dest.Value = source.Value;
            dest.WagePerWeek = source.WagePerWeek;
            dest.Age = source.Age;

            byte months = 0;
            if (source.ContractExpiryDate != null)
            {
                double diffDays = source.ContractExpiryDate.Value.Subtract(Globals.Instance.GameDate()).TotalDays;
                if (diffDays > 0)
                {
                    double doubleMonths = diffDays / 30;
                    months = doubleMonths > byte.MaxValue ? byte.MaxValue : (byte)doubleMonths;
                }
            }
            dest.ContractMonths = months;

            dest.SquadStatus = source.Contract?.SquadStatus;
            dest.TransferStatus = source.Contract?.TransferStatus;
            dest.ReleaseFee = source.Contract?.ReleaseClause != CMScouter.DataClasses.ReleaseClauseType.None ? source.Contract.ReleaseClause.ToString(): string.Empty;

            dest.BestRating = source.ScoutRatings.BestPosition.BestRole().Rating;
            dest.BestPosition = source.ScoutRatings.BestPosition.Position.ToString();
            dest.BestRole = source.ScoutRatings.BestPosition.BestRole().Role;
            dest.Recommendation = source.ScoutRatings.OverallRating;

            return dest;
        }
    }
}
