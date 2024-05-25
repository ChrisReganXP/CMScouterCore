using CMScouter.DataClasses;
using CMScouter.UI.DataClasses;
using CMScouter.UI.Raters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace CMScouter.UI
{
    public class PlayerView
    {
        public int PlayerId { get; set; }

        public int StaffId { get; set; }

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

        public PlayerShortlistEntry ShortlistData { get; set; }

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

        public string PlayingPositionDescription
        {
            get
            {
                if (Positions.GK == 20)
                {
                    return "GK";
                }

                var position = string.Empty;
                if (Positions.DF >= 15)
                {
                    position = "D";
                }
                if (Positions.DM >= 15)
                {
                    position += (!string.IsNullOrWhiteSpace(position)) ? "/DM" : "DM";
                }
                if (Positions.WingBack >= 15)
                {
                    position += (!string.IsNullOrWhiteSpace(position)) ? "/WB" : "WB";
                }
                if (Positions.MF >= 15)
                {
                    position += (!string.IsNullOrWhiteSpace(position)) ? "/M" : "M";
                }
                if (Positions.AM >= 15)
                {
                    position += (!string.IsNullOrWhiteSpace(position)) ? "/AM" : "AM";
                }
                if (Positions.ST >= 15)
                {
                    position += (!string.IsNullOrWhiteSpace(position)) ? "/F" : "ST";
                }

                position += " ";

                if (Positions.Right >= 15)
                {
                    position += "R";
                }
                if (Positions.Left >= 15)
                {
                    position += "L";
                }
                if (Positions.Centre >= 15)
                {
                    position += "C";
                }
                return position;
            }
        }

        public byte OverallRating
        {
            get { return ScoutRatings.OverallRating; }
        }

        public byte BestRating
        {
            get { return ScoutRatings.BestPosition.BestRole.AbilityRating; }
        }

        public string BestRole
        {
            get { return ScoutRatings.BestPosition.BestRole.Role.ToString(); }
        }

        public RoleRating BestRoleRatingForPlayerType(PlayerPosition type)
        {
            var bestPosition = ScoutRatings.PositionRatings.Where(x => x.SetPosition == type).OrderByDescending(y => y.RoleRatings.OrderByDescending(z => z.AbilityRating)).First();
            return bestPosition.RoleRatings.OrderByDescending(x => x.AbilityRating).First();
        }

        public string CreateCSVTextFromPlayer(List<PropertyInfo> playerValues)
        {
            StringBuilder csv = new StringBuilder();

            foreach (var prop in playerValues)
            {
                csv.Append(prop.GetValue(this) + ", ");
            }

            return csv.ToString();
        }

        public string CreateCSVTextFromAttributes(params List<PropertyInfo>[] lists)
        {
            if (lists == null)
            {
                return string.Empty;
            }

            StringBuilder csv = new StringBuilder();

            foreach (var list in lists)
            {
                foreach (var prop in list)
                {
                    csv.Append(prop.GetValue(this.Attributes) + ", ");
                }
            }

            return csv.ToString();
        }

        public string CreateCSVTextFromPositions(List<PropertyInfo> positionValues)
        {
            StringBuilder csv = new StringBuilder();

            foreach (var prop in positionValues)
            {
                csv.Append(prop.GetValue(this.Positions) + ", ");
            }

            return csv.ToString();
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
            var bestRole = ScoutRatings.BestPosition.BestRole;

            return $"{FirstName} {SecondName} - {bestRole.Role}:{bestRole.PurchaseRating} - ({ClubName})";
        }
    }
}
