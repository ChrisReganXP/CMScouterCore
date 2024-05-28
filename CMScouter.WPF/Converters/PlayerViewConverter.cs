using CMScouter.DataClasses;
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
        public static GridViewPlayer ConvertViewToGrid(PlayerView source, PlayerPosition? playerType = null)
        {
            GridViewPlayer dest = new GridViewPlayer();

            dest.PlayerId = source.PlayerId;
            dest.Name = source.GetKnownName();
            dest.ClubName = source.ClubName;
            dest.Value = source.Value;
            dest.WagePerWeek = source.WagePerWeek;
            dest.Age = source.Age;
            dest.Nationality = source.Nationality;
            if (!string.IsNullOrEmpty(source.SecondaryNationality))
            {
                dest.Nationality += " / " + source.SecondaryNationality;
            }

            dest.CurrentAbility = source.CurrentAbility;
            dest.PotentialAbility = source.PotentialAbility;

            dest.PlayingPositionDescription = source.PlayingPositionDescription;

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
            dest.Reputation = source.ReputationDescription;

            dest.SquadStatus = source.Contract?.SquadStatus;
            dest.SquadStatusValue = source.Contract?.SquadStatusValue ?? 255;
            dest.TransferStatus = source.Contract?.TransferStatus;

            dest.ReleaseFee = string.Empty;

            if (source.Contract?.ReleaseClause != ReleaseClauseType.None)
            {
                dest.ReleaseFee = $"{source.Contract.ReleaseValue.ToString("c0")} ({source.Contract.ReleaseClause.ToName()})";
            }

            dest.BestRating = playerType == null ? source.ScoutRatings.BestPosition.BestRole.AbilityRating : source.ScoutRatings.PositionRatings.Where(x => x.SetPosition == playerType).OrderByDescending(y => y.Rating).First().Rating;
            dest.PotentialRating = playerType == null ? source.ScoutRatings.BestPosition.BestRole.PotentialRating : source.ScoutRatings.PositionRatings.Where(x => x.SetPosition == playerType).OrderByDescending(y => y.PotentialRating).First().PotentialRating;
            dest.PurchaseRating = source.ScoutRatings.BestPosition.BestRole.PurchaseRating;
            dest.BestPosition = source.ScoutRatings.BestPosition.SetPosition.ToName();
            dest.BestRole = source.ScoutRatings.BestPosition.BestRole.Role.ToName();
            dest.Recommendation = source.ScoutRatings.OverallRating;

            dest.Penalties = source.Attributes.Penalties;
            dest.Corners = source.Attributes.Corners;
            dest.FreeKicks = source.Attributes.FreeKicks;
            dest.Influence = source.Attributes.Influence;

            return dest;
        }
    }
}
