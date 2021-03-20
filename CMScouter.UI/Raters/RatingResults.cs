using CMScouter.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMScouter.UI.Raters
{
    public class RoleRating
    {
        public Roles Role { get; set; }

        public byte AbilityRating { get; set; }

        public byte PurchaseRating { get; set; }

        public byte PhysicalRating { get; set; }

        public byte TechnicalRating { get; set; }

        public byte MentalRating { get; set; }

        public RatingRoleDebug Debug { get; set; }
    }

    public class PositionRating
    {
        public PlayerType Position { get; set; }

        public decimal PositionalAdjustment { get; }

        public byte OffFieldRating { get; }

        public List<RoleRating> Ratings { get; set; }

        public PositionRating(byte offField, decimal positionAdjust)
        {
            Ratings = new List<RoleRating>();
            OffFieldRating = offField;
            PositionalAdjustment = positionAdjust;
        }

        public byte Rating { get => Ratings.OrderByDescending(r => r.PurchaseRating).First().PurchaseRating; }

        public RoleRating BestRole()
        {
            return Ratings.OrderByDescending(x => x.AbilityRating).First();
        }
    }

    public class ScoutingInformation
    {
        private List<PositionRating> _positionRatings = new List<PositionRating>();

        public ScoutingInformation(List<PositionRating> scouting, decimal offField, GroupedRatings groupedRatings)
        {
            _positionRatings = scouting;
            PersonalityRating = offField;
            GroupedRatings = groupedRatings;
        }

        public List<PositionRating> PositionRatings { get => _positionRatings; }

        public GroupedRatings GroupedRatings { get; }

        public decimal PersonalityRating { get; }

        public byte OverallRating { get => this.ApplyPersonalityModifier(_positionRatings.OrderByDescending(r => r.PositionalAdjustment).First().Rating); }

        private byte ApplyPersonalityModifier(byte rating) => RatingHelper.ModifyByte(rating, PersonalityRating);

        public PositionRating BestPosition { get => _positionRatings.OrderByDescending(r => r.Rating).First(); }

        public PositionRating Goalkeeper { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.GoalKeeper); }

        public PositionRating RightBack { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.RightBack); }

        public PositionRating CentreHalf { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.CentreHalf); }

        public PositionRating LeftBack { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.LeftBack); }

        public PositionRating RightWingBack { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.RightWingBack); }

        public PositionRating DefensiveMidfielder { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.DefensiveMidfielder); }

        public PositionRating LeftWingBack { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.LeftWingBack); }

        public PositionRating RightMidfielder { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.RightMidfielder); }

        public PositionRating CentreMidfielder { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.CentralMidfielder); }

        public PositionRating LeftMidfielder { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.LeftMidfielder); }

        public PositionRating RightWinger { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.RightWinger); }

        public PositionRating AttackingMidfielder { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.AttackingMidfielder); }

        public PositionRating LeftWinger { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.LeftWinger); }

        public PositionRating CentreForward { get => _positionRatings.FirstOrDefault(r => r.Position == PlayerType.CentreForward); }
    }


    public class RatingRoleDebug
    {
        public string OffField { get; set; }

        public string Position { get; set; }

        public Roles Role { get; set; }

        public string Mental { get; set; }

        public string MentalDetail { get; set; }

        public string Physical { get; set; }

        public string PhysicalDetail { get; set; }

        public string Technical { get; set; }

        public string TechnicalDetail { get; set; }

    }
}
