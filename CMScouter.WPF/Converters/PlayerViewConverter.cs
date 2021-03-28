﻿using CMScouter.DataClasses;
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
        public static GridViewPlayer ConvertViewToGrid(PlayerView source, PlayerType? playerType = null)
        {
            GridViewPlayer dest = new GridViewPlayer();

            dest.PlayerId = source.PlayerId;
            dest.Name = source.GetKnownName();
            dest.ClubName = source.ClubName;
            dest.Value = source.Value;
            dest.WagePerWeek = source.WagePerWeek;
            dest.Age = source.Age;

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
            dest.SquadStatusValue = source.Contract?.SquadStatusValue ?? (byte)255;
            dest.TransferStatus = source.Contract?.TransferStatus;
            dest.ReleaseFee = source.Contract?.ReleaseClause != CMScouter.DataClasses.ReleaseClauseType.None ? source.Contract.ReleaseClause.ToName(): string.Empty;

            dest.BestRating = playerType == null ? source.ScoutRatings.BestPosition.BestRole().AbilityRating : source.ScoutRatings.PositionRatings.Where(x => x.Position == playerType).OrderByDescending(y => y.Rating).First().Rating;
            dest.PurchaseRating = source.ScoutRatings.BestPosition.BestRole().PurchaseRating;
            dest.BestPosition = source.ScoutRatings.BestPosition.Position.ToName();
            dest.BestRole = source.ScoutRatings.BestPosition.BestRole().Role.ToName();
            dest.Recommendation = source.ScoutRatings.OverallRating;
            dest.GoalkeepingRating = source.ScoutRatings.GroupedRatings.goalkeepingRating;
            dest.DefendingRating = source.ScoutRatings.GroupedRatings.defendingRating;
            dest.PlaymakingRating = source.ScoutRatings.GroupedRatings.playmakingRating;
            dest.WidePlayRating = source.ScoutRatings.GroupedRatings.wideplayRating;
            dest.ScoringRating = source.ScoutRatings.GroupedRatings.scoringRating;
            dest.ImpactRating = source.ScoutRatings.GroupedRatings.impactRating;
            dest.ReliabilityRating = source.ScoutRatings.GroupedRatings.reliabilityRating;
            dest.StrengthRating = source.ScoutRatings.GroupedRatings.strengthRating;
            dest.SpeedRating = source.ScoutRatings.GroupedRatings.speedRating;

            return dest;
        }
    }
}