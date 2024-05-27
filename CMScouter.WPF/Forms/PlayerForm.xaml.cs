using CMScouter.DataClasses;
using CMScouter.DataContracts;
using CMScouter.UI;
using CMScouterFunctions.DataClasses;
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
        CMScouterUI cmsUI;

        public PlayerForm(CMScouterUI cmsui, PlayerView player)
        {
            this.cmsUI = cmsui;
            InitializeComponent();
            this.player = player;
            this.masker = cmsUI.IntrinsicMasker;

            BindPlayerDetails();
        }

        private void BindPlayerDetails()
        {
            InitialiseRatingPanel();

            AddPersonalDetails();
            AddPositionDetails();
            AddPersonality();

            AddTechnical();
            AddMental();
            AddPhysical();
            AddSetPieces();
            AddGoalkeeping();

            AddRating();
            AddScouting();
        }

        protected void SetAttributeLabels(Label valueLabel, byte value, DP attribute, bool IsIntrinsic = false, bool IsInverted = false)
        {
            decimal maskedValue = 0;
            if (IsIntrinsic)
            {
                maskedValue = value;
                //value = masker.GetIntrinsicBasicMask(value, player.CurrentAbility);
                value = (byte)masker.GetIntrinsicMask(player.CurrentAbility, player.Positions, player.Attributes.Versatility, attribute, (PlayerPosition)ddlPosition.SelectedValue, (PlayerPosition)ddlRunTo.SelectedValue, value);
            }

            Color colour = GetAttributeColour(IsInverted ? (21 - value) :value);

            valueLabel.Foreground = new SolidColorBrush(colour);
            valueLabel.Content = value.ToString();

            if (maskedValue > 0)
            {
                valueLabel.Content += $" ({maskedValue})";
            }
        }

        private static Color GetAttributeColour(decimal value)
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
            this.Title = player.GetKnownName() + " - " + player.PlayerId + $" ({player.StaffId})";
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
            SetAttributeLabels(lblAnticipation, player.Attributes.Anticipation, DP.Anticipation, true);
            SetAttributeLabels(lblCreativity, player.Attributes.Creativity, DP.Creativity, true);
            SetAttributeLabels(lblCrossing, player.Attributes.Crossing, DP.Crossing, true);
            SetAttributeLabels(lblDecisions, player.Attributes.Decisions, DP.Decisions, true);
            SetAttributeLabels(lblDribbling, player.Attributes.Dribbling, DP.Dribbling, true);
            SetAttributeLabels(lblFinishing, player.Attributes.Finishing, DP.Finishing, true);
            SetAttributeLabels(lblHeading, player.Attributes.Heading, DP.Heading, true);
            SetAttributeLabels(lblLongShots, player.Attributes.LongShots, DP.LongShots, true);
            SetAttributeLabels(lblMarking, player.Attributes.Marking, DP.Marking, true);
            SetAttributeLabels(lblOffTheBall, player.Attributes.OffTheBall, DP.OffTheBall, true);
            SetAttributeLabels(lblPassing, player.Attributes.Passing, DP.Passing, true);
            SetAttributeLabels(lblPositioning, player.Attributes.Positioning, DP.Positioning, true);
            SetAttributeLabels(lblTackling, player.Attributes.Tackling, DP.Tackling, true);
            SetAttributeLabels(lblTechnique, player.Attributes.Technique, DP.Technique);
        }

        private void AddMental()
        {
            SetAttributeLabels(lblAggression, player.Attributes.Aggression, DP.Aggression);
            SetAttributeLabels(lblBravery, player.Attributes.Bravery, DP.Bravery);
            SetAttributeLabels(lblConsistency, player.Attributes.Consistency, DP.Consistency);
            SetAttributeLabels(lblDirtyness, player.Attributes.Dirtiness, DP.Dirtiness, false, true);
            SetAttributeLabels(lblFlair, player.Attributes.Flair, DP.Flair);
            SetAttributeLabels(lblImpMatches, player.Attributes.ImportantMatches, DP.ImportantMatches);
            SetAttributeLabels(lblInfluence, player.Attributes.Influence, DP.Influence);
            SetAttributeLabels(lblTeamwork, player.Attributes.Teamwork, DP.Teamwork);
            SetAttributeLabels(lblVersitility, player.Attributes.Versatility, DP.Versatility);
            SetAttributeLabels(lblWorkRate, player.Attributes.WorkRate, DP.WorkRate);
        }

        private void AddPhysical()
        {
            SetAttributeLabels(lblAcceleration, player.Attributes.Acceleration, DP.Acceleration);
            SetAttributeLabels(lblAgility, player.Attributes.Agility, DP.Agility);
            SetAttributeLabels(lblBalance, player.Attributes.Balance, DP.Balance);
            SetAttributeLabels(lblInjuryProne, player.Attributes.InjuryProneness, DP.InjuryProneness, false, true);
            SetAttributeLabels(lblJumping, player.Attributes.Jumping, DP.Jumping);
            SetAttributeLabels(lblNatFitness, player.Attributes.NaturalFitness, DP.NaturalFitness);
            SetAttributeLabels(lblPace, player.Attributes.Pace, DP.Pace);
            SetAttributeLabels(lblStamina, player.Attributes.Stamina, DP.Stamina);
            SetAttributeLabels(lblStrength, player.Attributes.Strength, DP.Strength);
        }
        
        private void AddSetPieces()
        {
            SetAttributeLabels(lblCorners, player.Attributes.Corners, DP.Corners);
            SetAttributeLabels(lblFreeKicks, player.Attributes.FreeKicks, DP.FreeKicks);
            SetAttributeLabels(lblPenalties, player.Attributes.Penalties, DP.Penalties, true);
            SetAttributeLabels(lblThrowIns, player.Attributes.ThrowIns, DP.ThrowIns, true);
        }

        private void AddGoalkeeping()
        {
            SetAttributeLabels(lblHandling, player.Attributes.Handling, DP.Handling, true);
            SetAttributeLabels(lblOneOnes, player.Attributes.OneOnOnes, DP.OneOnOnes, true);
            SetAttributeLabels(lblReflexes, player.Attributes.Reflexes, DP.Reflexes, true);
        }

        private void AddPersonality()
        {
            SetAttributeLabels(lblAdaptability, player.Attributes.Adaptability, DP.Adaptability);
            SetAttributeLabels(lblAmbition, player.Attributes.Ambition, DP.Ambition);
            SetAttributeLabels(lblDetermination, player.Attributes.Determination, DP.Determination);
            SetAttributeLabels(lblLoyalty, player.Attributes.Loyalty, DP.Loyalty);
            SetAttributeLabels(lblPressure, player.Attributes.Pressure, DP.Pressure);
            SetAttributeLabels(lblProfessionalism, player.Attributes.Professionalism, DP.Professionalism);
            SetAttributeLabels(lblSportsmanship, player.Attributes.Sportsmanship, DP.Sportsmanship);
            SetAttributeLabels(lblTemperament, player.Attributes.Temperament, DP.Temperament);
        }

        private void AddRating()
        {
            lblRating.Content = player.ScoutRatings.BestPosition.BestRole.AbilityRating;
        }

        private void AddScouting()
        {
            var role = player.ScoutRatings.BestPosition.BestRole;
            lblScoutRole.Content = role.Role.ToName();
            lblPhysical.Content = role.Debug.PhysicalDetail;
            lblTechnical.Content = role.Debug.TechnicalDetail;
            lblMental.Content = role.Debug.MentalDetail;

            lblGoalKeeping.Content = role.Debug.GoalkeepingDetail;
            lblDefending.Content = role.Debug.DefendingDetail;
            lblPlaymaking.Content = role.Debug.PlaymakingDetail;
            lblWidePlay.Content = role.Debug.WideplayDetail;
            lblScoring.Content = role.Debug.ScoringDetail;
            
            lblSpeed.Content = role.Debug.SpeedDetail;
            lblStrengthDetail.Content = role.Debug.StrengthDetail;

            lblImpact.Content = role.Debug.ImpactDetail;
            lblReliability.Content = role.Debug.ReliabilityDetail;
        }

        private void InitialiseRatingPanel()
        {
            ddlPosition.ItemsSource = null;
            ddlPosition.DisplayMemberPath = "Name";
            ddlPosition.SelectedValuePath = "Position";

            ddlRunTo.ItemsSource = null;
            ddlRunTo.DisplayMemberPath = "Name";
            ddlRunTo.SelectedValuePath = "Position";

            var positionsArray = Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>().Select(x => new { Position = (int)x, Name = x == player.ScoutRatings.BestPosition.SetPosition ? x.ToName() + " (Best)" : x.ToName() }).ToList();

            ddlPosition.ItemsSource = positionsArray;
            ddlPosition.SelectedIndex = positionsArray.First(x => x.Position == (int)player.ScoutRatings.BestPosition.SetPosition).Position;

            var movementsArray = Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>().Select(x => new { Position = (int)x, Name = x.ToName() }).ToList();

            ddlRunTo.ItemsSource = movementsArray;
            ddlRunTo.SelectedIndex = movementsArray.First(x => x.Position == (int)player.ScoutRatings.BestPosition.MovementPosition).Position;
        }

        private void ddlPosition_DropDownClosed(object sender, EventArgs e)
        {
            ConstructPlayerOptions options = new ConstructPlayerOptions();
            options.setPosition = (PlayerPosition)ddlPosition.SelectedValue;

            if ((ComboBox)sender == ddlPosition)
            {
                ddlRunTo.SelectedValue = ddlPosition.SelectedValue;
            }

            options.movementPosition = (PlayerPosition)ddlRunTo.SelectedValue;

            if (options.setPosition >= 0 && options.movementPosition >= 0)
            {
                player = cmsUI.GetPlayerByPlayerId(player.PlayerId, options);

                BindPlayerDetails();
            }
        }

        private void cbxPotential_Checked(object sender, RoutedEventArgs e)
        {
            if (!(sender is CheckBox))
            {
                return;
            }

            if (((CheckBox)sender).IsChecked == true)
            {
                this.player = cmsUI.GetPlayerPotential(this.player.PlayerId);
            }
            else
            {
                this.player = cmsUI.GetPlayerByPlayerId(this.player.PlayerId);
            }

            BindPlayerDetails();
        }
    }
}
