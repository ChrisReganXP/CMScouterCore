using CMScouter.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CMScouter.WPF
{
    /// <summary>
    /// Interaction logic for PlayerView.xaml
    /// </summary>
    public partial class PlayerForm : Window
    {
        PlayerView player;
        IIntrinsicMasker masker;

        public PlayerForm(PlayerView player, IIntrinsicMasker masker)
        {
            InitializeComponent();
            this.player = player;
            this.masker = masker;

            AddPersonalDetails();
            AddPositionDetails();
            AddPersonality();

            AddTechnical();
            AddMental();
            AddPhysical();
            AddSetPieces();
            AddGoalkeeping();
        }

        protected void SetAttributeLabels(Label valueLabel, byte value, bool IsIntrinsic = false, bool IsInverted = false)
        {
            byte maskedValue = 0;
            if (IsIntrinsic)
            {
                maskedValue = value;
                value = masker.GetIntrinsicBasicMask(value, player.CurrentAbility);
            }

            Color colour = GetAttributeColour(IsInverted ? (byte)(21 - value) : value);

            valueLabel.Foreground = new SolidColorBrush(colour);
            valueLabel.Content = value.ToString();

            if (maskedValue > 0)
            {
                valueLabel.Content += $" ({maskedValue})";
            }
        }

        private static Color GetAttributeColour(byte value)
        {
            if (value >= 21)
            {
                return Colors.DarkGoldenrod;
            }

            if (value >= 18)
            {
                return Colors.Teal;
            }

            if (value >= 15)
            {
                return Colors.DarkSeaGreen;
            }

            if (value >= 12)
            {
                return Colors.DarkOliveGreen;
            }

            if (value >= 8)
            {
                return Colors.Black;
            }

            if (value >= 5)
            {
                return Colors.Maroon;
            }

            return Colors.Red;
        }

        private void AddPersonalDetails()
        {
            lblFullName.Content = player.GetKnownName();
            lblAlternateName.Content = player.GetAlternateName();
            lblNationality.Content = player.Nationality;
            lblSecondNation.Content = player.SecondaryNationality;
            lblPosition.Content = player.PlayingPositionDescription;
            lblAge.Content = player.Age;
            lblCAValue.Content = player.CurrentAbility;
            lblPAValue.Content = player.PotentialAbility;
            lblPreferredFoot.Content = player.GetFootedness();
            lblValue.Content = player.Value.ToString("c0");
            lblWage.Content = player.WagePerWeek.ToString("c0");
            lblExpiry.Content = player.ContractExpiryDate == null ? "-" : player.ContractExpiryDate.Value.ToShortDateString();
            lblClub.Content = player.ClubName;
        }

        private void AddPositionDetails()
        {
            lblGK.Content = player.Positions.GK;
            lblSW.Content = player.Positions.SW;
            lblDF.Content = player.Positions.DF;
            lblDM.Content = player.Positions.DM;
            lblWB.Content = player.Positions.WingBack;
            lblMF.Content = player.Positions.MF;
            lblAM.Content = player.Positions.AM;
            lblFW.Content = player.Positions.ST;
            lblFreeRole.Content = player.Positions.FreeRole;
            lblR.Content = player.Positions.Right;
            lblC.Content = player.Positions.Centre;
            lblL.Content = player.Positions.Left;
        }

        private void AddTechnical()
        {
            SetAttributeLabels(lblAnticipation, player.Attributes.Anticipation, true);
            SetAttributeLabels(lblCreativity, player.Attributes.Creativity, true);
            SetAttributeLabels(lblCrossing, player.Attributes.Crossing, true);
            SetAttributeLabels(lblDecisions, player.Attributes.Decisions, true);
            SetAttributeLabels(lblDribbling, player.Attributes.Dribbling, true);
            SetAttributeLabels(lblFinishing, player.Attributes.Finishing, true);
            SetAttributeLabels(lblHeading, player.Attributes.Heading, true);
            SetAttributeLabels(lblLongShots, player.Attributes.LongShots, true);
            SetAttributeLabels(lblMarking, player.Attributes.Marking, true);
            SetAttributeLabels(lblOffTheBall, player.Attributes.OffTheBall, true);
            SetAttributeLabels(lblPassing, player.Attributes.Passing, true);
            SetAttributeLabels(lblPositioning, player.Attributes.Positioning, true);
            SetAttributeLabels(lblTackling, player.Attributes.Tackling, true);
            SetAttributeLabels(lblTechnique, player.Attributes.Technique);
        }

        private void AddMental()
        {
            SetAttributeLabels(lblAggression, player.Attributes.Aggression);
            SetAttributeLabels(lblBravery, player.Attributes.Bravery);
            SetAttributeLabels(lblConsistency, player.Attributes.Consistency);
            SetAttributeLabels(lblDirtyness, player.Attributes.Dirtiness, false, true);
            SetAttributeLabels(lblFlair, player.Attributes.Flair);
            SetAttributeLabels(lblImpMatches, player.Attributes.ImportantMatches);
            SetAttributeLabels(lblInfluence, player.Attributes.Influence);
            SetAttributeLabels(lblTeamwork, player.Attributes.Teamwork);
            SetAttributeLabels(lblVersitility, player.Attributes.Versitility);
            SetAttributeLabels(lblWorkRate, player.Attributes.WorkRate);
        }

        private void AddPhysical()
        {
            SetAttributeLabels(lblAcceleration, player.Attributes.Acceleration);
            SetAttributeLabels(lblAgility, player.Attributes.Agility);
            SetAttributeLabels(lblBalance, player.Attributes.Balance);
            SetAttributeLabels(lblInjuryProne, player.Attributes.InjuryProneness, false, true);
            SetAttributeLabels(lblJumping, player.Attributes.Jumping);
            SetAttributeLabels(lblNatFitness, player.Attributes.NaturalFitness);
            SetAttributeLabels(lblPace, player.Attributes.Pace);
            SetAttributeLabels(lblStamina, player.Attributes.Stamina);
            SetAttributeLabels(lblStrength, player.Attributes.Strength);
        }
        
        private void AddSetPieces()
        {
            SetAttributeLabels(lblCorners, player.Attributes.Corners);
            SetAttributeLabels(lblFreeKicks, player.Attributes.FreeKicks);
            SetAttributeLabels(lblPenalties, player.Attributes.Penalties, true);
            SetAttributeLabels(lblThrowIns, player.Attributes.ThrowIns, true);
        }

        private void AddGoalkeeping()
        {
            SetAttributeLabels(lblHandling, player.Attributes.Handling, true);
            SetAttributeLabels(lblOneOnes, player.Attributes.OneOnOnes, true);
            SetAttributeLabels(lblReflexes, player.Attributes.Reflexes, true);
        }

        private void AddPersonality()
        {
            SetAttributeLabels(lblAdaptability, player.Attributes.Adaptability);
            SetAttributeLabels(lblAmbition, player.Attributes.Ambition);
            SetAttributeLabels(lblDetermination, player.Attributes.Determination);
            SetAttributeLabels(lblLoyalty, player.Attributes.Loyalty);
            SetAttributeLabels(lblPressure, player.Attributes.Pressure);
            SetAttributeLabels(lblProfessionalism, player.Attributes.Professionalism);
            SetAttributeLabels(lblSportsmanship, player.Attributes.Sportsmanship);
            SetAttributeLabels(lblTemperament, player.Attributes.Temperament);
        }
    }
}
