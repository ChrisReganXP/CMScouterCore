using CMScouter.DataClasses;
using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CMScouter.UI
{
    public class PlayerView
    {
        public int PlayerId { get; set; }

        public string FirstName { get; set; }

        public string SecondName { get; set; }

        public string CommonName { get; set; }

        public string Nationality { get; set; }

        public string SecondaryNationality { get; set; }

        public string ClubName { get; set; }

        public byte Age { get; set; }

        public short CurrentAbility { get; set; }

        public short PotentialAbility { get; set; }

        public int Value { get; set; }

        public int WagePerWeek { get; set; }

        public DateTime? ContractExpiryDate { get; set; }

        public string ReputationDescription { get; set; }

        public short ReputationValue { get; set; }

        public short DomesticReputation { get; set; }

        public short WorldReputation { get; set; }

        public string GetKnownName()
        {
            return string.IsNullOrWhiteSpace(CommonName) ? FirstName + " " + SecondName : CommonName;
        }

        public string GetAlternateName()
        {
            return string.IsNullOrWhiteSpace(CommonName) ? null : FirstName + " " + SecondName;
        }

        public ContractView Contract { get; set; }

        public PlayerPositionView Positions { get; set; }

        public PlayerAttributeView Attributes { get; set; }

        public ScoutingInformation ScoutRatings { get; set; }

        public string GetFootedness()
        {
            if (Attributes.LeftFoot >= 15 && Attributes.RightFoot >= 15)
            {
                return "Either";
            }

            if (Attributes.LeftFoot >= 15 && Attributes.RightFoot >= 10)
            {
                return "Left";
            }

            if (Attributes.LeftFoot >= 10 && Attributes.RightFoot >= 15)
            {
                return "Right";
            }

            if (Attributes.LeftFoot >= 15)
            {
                return "Left Only";
            }

            return "Right Only";
        }

        public byte OverallRating
        {
            get { return ScoutRatings.OverallRating; }
        }

        public byte BestRating
        {
            get { return ScoutRatings.BestPosition.BestRole().AbilityRating; }
        }

        public string BestRole
        {
            get { return ScoutRatings.BestPosition.BestRole().Role.ToString(); }
        }

        public RoleRating BestRoleRatingForPlayerType(PlayerType type)
        {
            var bestPosition = ScoutRatings.PositionRatings.Where(x => x.Position == type).OrderByDescending(y => y.Ratings.OrderByDescending(z => z.AbilityRating)).First();
            return bestPosition.Ratings.OrderByDescending(x => x.AbilityRating).First();
        }

        /*
        public byte BestRating
        {
            get
            {
                byte[] ratings = new byte[11];
                ratings[0] = GKRating;
                ratings[1] = DFBRating;
                ratings[2] = AFBRating;
                ratings[3] = CBRating;
                ratings[4] = DMRating;
                ratings[5] = CMRating;
                ratings[6] = WMRating;
                ratings[7] = WGRating;
                ratings[8] = AMRating;
                ratings[9] = TargetManRating;
                ratings[10] = PoacherRating;

                return ratings.Max();
            }
        }*/

        /*
        public byte GKRating
        {
            get { return ScoutRatings.Goalkeeper.BestRole().Rating; }
        }
        public string GKRole
        {
            get { return ScoutRatings.Goalkeeper.BestRole().Role.ToString(); }
        }

        public byte RBRating
        {
            get { return ScoutRatings.RightBack.BestRole().Rating; }
        }
        public string RBRole
        {
            get { return ScoutRatings.RightBack.BestRole().Role.ToString(); }
        }

        public byte CBRating
        {
            get { return ScoutRatings.CentreHalf.BestRole().Rating; }
        }
        public string CBRole
        {
            get { return ScoutRatings.CentreHalf.BestRole().Role.ToString(); }
        }

        public byte LBRating
        {
            get { return ScoutRatings.LeftBack.BestRole().Rating; }
        }
        public string LBRole
        {
            get { return ScoutRatings.LeftBack.BestRole().Role.ToString(); }
        }

        public byte DMRating
        {
            get { return ScoutRatings.DefensiveMidfielder.BestRole().Rating; }
        }
        public string DMRole
        {
            get { return ScoutRatings.DefensiveMidfielder.BestRole().Role.ToString(); }
        }

        public byte RMRating
        {
            get { return ScoutRatings.RightMidfielder.BestRole().Rating; }
        }
        public string RMRole
        {
            get { return ScoutRatings.RightMidfielder.BestRole().Role.ToString(); }
        }

        public byte CMRating
        {
            get { return ScoutRatings.CentreMidfielder.BestRole().Rating; }
        }
        public string CMRole
        {
            get { return ScoutRatings.CentreMidfielder.BestRole().Role.ToString(); }
        }

        public byte LMRating
        {
            get { return ScoutRatings.LeftMidfielder.BestRole().Rating; }
        }
        public string LMRole
        {
            get { return ScoutRatings.LeftMidfielder.BestRole().Role.ToString(); }
        }

        public byte RWRating
        {
            get { return ScoutRatings.RightWinger.BestRole().Rating; }
        }
        public string RWRole
        {
            get { return ScoutRatings.RightWinger.BestRole().Role.ToString(); }
        }

        public byte AMRating
        {
            get { return ScoutRatings.AttackingMidfielder.BestRole().Rating; }
        }
        public string AMRole
        {
            get { return ScoutRatings.AttackingMidfielder.BestRole().Role.ToString(); }
        }

        public byte LWRating
        {
            get { return ScoutRatings.LeftWinger.BestRole().Rating; }
        }
        public string LWRole
        {
            get { return ScoutRatings.LeftWinger.BestRole().Role.ToString(); }
        }

        public byte CFRating
        {
            get { return ScoutRatings.CentreForward.BestRole().Rating; }
        }
        public string CFRole
        {
            get { return ScoutRatings.CentreForward.BestRole().Role.ToString(); }
        }*/

        /*
        public byte BasePhysicalRating
        {
            get
            {
                return (byte)((Acceleration + Jumping + Pace + Strength) / 4);
            }
        }

        public byte BaseMentalRating
        {
            get
            {
                return (byte)((Aggression + Bravery + Consistency + ImportantMatches + Influence + Teamwork + WorkRate) / 7);
            }
        }

        public byte BaseAttackingRating
        {
            get
            {
                return (byte)((Anticipation + Creativity + Crossing + Decisions + Dribbling + Finishing + Heading + LongShots + OffTheBall + Passing) / 10);
            }

        }

        public byte BaseDefendingRating
        {
            get
            {
                return (byte)((Anticipation + Decisions + Heading + Marking + Positioning + Tackling) / 6);
            }
        }*/

        public override string ToString()
        {
            var bestRole = ScoutRatings.BestPosition.BestRole();

            return $"{FirstName} {SecondName} - {bestRole.Role}:{bestRole.PurchaseRating} - ({ClubName})";
        }
    }
}
