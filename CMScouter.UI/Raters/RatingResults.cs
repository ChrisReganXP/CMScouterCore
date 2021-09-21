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
        public PlayerPosition SetPosition { get; set; }

        public PlayerPosition MovementPosition { get; set; }

        public byte OffFieldRating { get; }

        public List<RoleRating> RoleRatings { get; set; }

        public PositionRating(byte offField)
        {
            RoleRatings = new List<RoleRating>();
            OffFieldRating = offField;
        }

        public RoleRating BestRole { get => RoleRatings.OrderByDescending(r => r.AbilityRating).FirstOrDefault(); }

        public byte Rating { get => BestRole?.AbilityRating ?? 0; }
    }

    public class ScoutingInformation
    {
        private List<PositionRating> _positionRatings = new List<PositionRating>();

        public ScoutingInformation(List<PositionRating> scouting, decimal offField)
        {
            _positionRatings = scouting;
            PersonalityRating = offField;
        }

        public List<PositionRating> PositionRatings { get => _positionRatings; }

        public decimal PersonalityRating { get; }

        public byte OverallRating { get => BestPosition.BestRole.PurchaseRating; }

        public PositionRating BestPosition { get => _positionRatings.OrderByDescending(r => r.Rating).First(); }
        /*
        public PositionRating Goalkeeper { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.GoalKeeper); }

        public PositionRating RightBack { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.RightBack); }

        public PositionRating CentreHalf { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.CentreHalf); }

        public PositionRating LeftBack { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.LeftBack); }

        public PositionRating RightWingBack { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.RightWingBack); }

        public PositionRating DefensiveMidfielder { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.DefensiveMidfielder); }

        public PositionRating LeftWingBack { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.LeftWingBack); }

        public PositionRating RightMidfielder { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.RightMidfielder); }

        public PositionRating CentreMidfielder { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.CentralMidfielder); }

        public PositionRating LeftMidfielder { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.LeftMidfielder); }

        public PositionRating RightWinger { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.RightWinger); }

        public PositionRating AttackingMidfielder { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.AttackingMidfielder); }

        public PositionRating LeftWinger { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.LeftWinger); }

        public PositionRating CentreForward { get => _positionRatings.FirstOrDefault(r => r.SetPosition == PlayerPosition.CentreForward); }*/
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
