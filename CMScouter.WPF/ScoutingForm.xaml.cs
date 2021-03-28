using CMScouter.UI;
using CMScouter.UI.Raters;
using CMScouter.WPF.Converters;
using CMScouter.WPF.DataClasses;
using CMScouter.DataClasses;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Markup;
using System.Globalization;
using CMScouter.WPF.ControlHelpers;
using CMScouter.UI.DataClasses;
using System.IO;

namespace CMScouter.WPF
{
    /// <summary>
    /// Interaction logic for ScoutingForm.xaml
    /// </summary>
    public partial class ScoutingForm : Window
    {
        private CMScouterUI cmsUI;

        public ScoutingForm()
        {
            InitializeComponent();
            PopulateInitialItems();
        }

        private void PopulateInitialItems()
        {
            dgvPlayers.Visibility = Visibility.Hidden;
            dgvPlayers.AutoGenerateColumns = false;

            stpSearchCriteria.Visibility = Visibility.Collapsed;

            PopulateStaticSearchControls();

            /*
            ddlSearchTypes.Items.Add(PlayerSearch);
            ddlSearchTypes.Items.Add(ClubSearch);
            ddlSearchTypes.SelectedIndex = 0;*/

            /*
            ddlContractStatus.ValueMember = "Value";
            ddlContractStatus.DisplayMember = "Text";
            var contractStatusList = new[] { new { Value = -1, Text = "<All>" }, new { Value = 6, Text = "Expires 6 Months" }, new { Value = 12, Text = "Expires 12 Months" } };
            ddlContractStatus.DataSource = contractStatusList;
            ddlContractStatus.SelectedIndex = 0;*/
        }

        private void PopulatePlayerTypes()
        {
            ddlPlayerType.Items.Add("<All>");
            ddlPlayerType.SelectedIndex = 0;
            foreach (var type in Enum.GetNames(typeof(PlayerType)))
            {
                ddlPlayerType.Items.Add(type);
            }
        }

        private void PopulateAvailabilityTypes()
        {
            var availabilityTypes = AvailabilityTypeControlHelper.GetAvailabilityTypeKeyValuePairs();

            ddlAvailability.ItemsSource = availabilityTypes;
            ddlAvailability.DisplayMemberPath = "Value";
            ddlAvailability.SelectedValuePath = "Key";
            ddlAvailability.SelectedIndex = 0;
        }

        private void PopulateReputationLevels()
        {
            var reputationTypes = ReputationLevelControlHelper.GetReputationLevelKeyValuePairs().OrderByDescending(x => x.Key).ToList();

            ddlReputation.ItemsSource = reputationTypes;
            ddlReputation.DisplayMemberPath = "Value";
            ddlReputation.SelectedValuePath = "Key";
            var all = new KeyValuePair<int, string>(-1, "<All>");
            reputationTypes.Insert(0, all);
            ddlReputation.SelectedIndex = 0;
        }

        private void PopulateNationalities()
        {
            ddlNationality.DisplayMemberPath = "Name";
            ddlNationality.SelectedValuePath = "Id";

            var nationList = cmsUI.GetAllNations().Select(x => new { x.Id, x.Name }).OrderBy(x => x.Name).ToList();
            var all = new { Id = -1, Name = "<All>" };
            nationList.Insert(0, all);

            ddlNationality.ItemsSource = nationList;
            ddlNationality.SelectedIndex = 0;
        }

        private void PopulatePlayerBased()
        {
            ddlPlayerBased.SelectedValuePath = "Value";
            ddlPlayerBased.DisplayMemberPath = "Text";
            var playsInLocationList = new List<object>
            {
                new { Value = "-1", Text = "<All>" },
                new { Value = "-2", Text = "---- Regions ----" },
                new { Value = "UKI", Text = "UK & Ireland" },
                new { Value = "SCA", Text = "Scandinavia" },
                new { Value = "OCE", Text = "Oceania" },
                new { Value = "-3", Text = "---- Competitions ----" },
            };

            var nations = cmsUI.GetAllNations();
            var clubComps = cmsUI.GetAllClubCompetitions().OrderBy(x => x.LongName);

            foreach (var comp in clubComps)
            {
                var x = new { Value = comp.Id.ToString(), Text = comp.LongName };
                playsInLocationList.Add(x);
            }

            ddlPlayerBased.ItemsSource = playsInLocationList;
            ddlPlayerBased.SelectedIndex = 0;
        }
        #region Menu Events

        private void Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Save Game Files (*.sav)|*.sav";
            openFileDialog.DefaultExt = "sav";
            if (openFileDialog.ShowDialog() == true)
            {
                LoadSaveGameFile(openFileDialog.FileName);
            }
        }

        #endregion

        #region Load Data

        private void LoadSaveGameFile(string fileName)
        {
            cmsUI = new CMScouterUI(fileName);

            CustomiseSearchOptions();

            /*DisplayInitialSearchOptions();*/
            /*}*/

            Globals.Instance.SetGameDate(cmsUI.GameDate);

            // Initial Test only. Delete me
            SearchForClub();
        }

        #endregion

        #region Execute Search
        
        private void SearchForClub()
        {
            ScoutingRequest request = new ScoutingRequest()
            {
                // Initial Test only. Delete me
                ClubName = "Schalke 04",

                /*ClubName = cbxClubName.Text,*/
            };

            var playerList = cmsUI.GetScoutResults(request);
            DisplayPlayerList(playerList, request);
        }

        #endregion
        
        #region Show Players

        private void DisplayPlayerList(List<PlayerView> playerList, ScoutingRequest request)
        {
            /*
            dgvPlayers.Columns.Clear();
            */

            /*var buttonColumn = new DataGridViewButtonColumn();
            buttonColumn.HeaderText = "View";
            buttonColumn.Name = "ViewButton";
            buttonColumn.Text = "View";
            buttonColumn.Width = 40;
            buttonColumn.UseColumnTextForButtonValue = true;
            dgvPlayers.Columns.Add(buttonColumn);*/

            /*
            dgvPlayers.Columns.Add(CreateGridViewColumn(50, "PlayerId", "Id"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(120, "Name", "Name"));

            if (request.PlayerType != null)
            {
                dgvPlayers.Columns.Add(CreateGridViewColumn(30, "ScoutedRating", "Pos Rat"));
                dgvPlayers.Columns.Add(CreateGridViewColumn(30, "ScoutedRole", "Role Rat"));
            }

            dgvPlayers.Columns.Add(CreateGridViewColumn(30, "BestRating", "Rat"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(30, "BestPosition", "Pos"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(30, "BestRole", "Role"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(100, "ClubName", "Club"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "DescribedPosition", "Position"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "Value", "Value", format: "c0"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(50, "Wage", "Wage", format: "c0"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(30, "Age", "Age"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "Nationality", "Nation"));
            dgvPlayers.Columns.Add(CreateGridViewColumn(70, "ContractExpiryDate", "Contract"));*/

            List<GridViewPlayer> playerViewList = CreatePlayerViews(playerList, request.PlayerType).ToList();
            dgvPlayers.ItemsSource = playerViewList;
            dgvPlayers.Visibility = Visibility.Visible;
        }

        private IEnumerable<GridViewPlayer> CreatePlayerViews(List<PlayerView> originalPlayers, PlayerType? playerType)
        {
            foreach (var p in originalPlayers)
            {
                yield return PlayerViewConverter.ConvertViewToGrid(p, playerType);
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            GridViewPlayer gvp = (sender as Button).DataContext as GridViewPlayer;
            PlayerView player = cmsUI.GetPlayerByPlayerId(new List<int>() { gvp.PlayerId }).First();

            /*
            PlayerViewForm details = new PlayerViewForm(player, cmsUI.IntrinsicMasker);
            DialogResult result = details.ShowDialog();*/
        }

        #endregion

        /*
        private DataGridTextColumn CreateGridViewColumn(int width, string propertyName, string headerText, bool isDecimal = false, string format = null)
        {
            var c = new DataGridTextColumn() { Width = width, Binding = propertyName, Header = headerText, ReadOnly = true, Resizable = DataGridViewTriState.False, SortMode = DataGridViewColumnSortMode.Automatic };

            if (isDecimal)
            {
                c.DefaultCellStyle.Format = "0.00";
            }

            if (!string.IsNullOrWhiteSpace(format))
            {
                c.DefaultCellStyle.Format = format;
            }

            return c;
        }
        */

        /*
        private DataTable CreateDataTable(List<PlayerView> playerList, ScoutingRequest request)
        {
            DataTable dt = new DataTable();
            dt.Columns.Add(new DataColumn() { ColumnName = "PlayerId", DataType = typeof(int), DefaultValue = null, Unique = true });
            dt.Columns.Add(new DataColumn() { ColumnName = "Name", DataType = typeof(string), DefaultValue = null });

            if (request.PlayerType != null)
            {
                dt.Columns.Add(new DataColumn() { ColumnName = "ScoutedRating", DataType = typeof(byte), DefaultValue = null });
                dt.Columns.Add(new DataColumn() { ColumnName = "ScoutedRole", DataType = typeof(string), DefaultValue = null });
            }

            dt.Columns.Add(new DataColumn() { ColumnName = "BestRating", DataType = typeof(byte), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "BestPosition", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "BestRole", DataType = typeof(string), DefaultValue = null });

            dt.Columns.Add(new DataColumn() { ColumnName = "ClubName", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "DescribedPosition", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "Value", DataType = typeof(int), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "Wage", DataType = typeof(int), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "Age", DataType = typeof(short), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "Nationality", DataType = typeof(string), DefaultValue = null });
            dt.Columns.Add(new DataColumn() { ColumnName = "ContractExpiryDate", DataType = typeof(string), DefaultValue = null });

            foreach (var player in playerList)
            {
                DataRow dr = dt.NewRow();
                dr["PlayerId"] = player.PlayerId;
                dr["Name"] = player.GetKnownName();

                if (request.PlayerType != null)
                {
                    dr["ScoutedRating"] = GetScoutedPositionRating(request.PlayerType, player.ScoutRatings);
                    dr["ScoutedRole"] = GetScoutedRole(request.PlayerType, player.ScoutRatings);
                }

                dr["BestRating"] = player.ScoutRatings.BestPosition.BestRole().Rating;
                dr["BestPosition"] = GetScoutedPosition(player.ScoutRatings.BestPosition.Position);
                dr["BestRole"] = player.ScoutRatings.BestPosition.BestRole().Role;

                dr["ClubName"] = player.ClubName;
                dr["DescribedPosition"] = player.Positions.DescribedPosition;
                dr["Value"] = player.Value;
                dr["Wage"] = player.WagePerWeek;
                dr["Age"] = player.Age;
                dr["Nationality"] = player.Nationality;
                dr["ContractExpiryDate"] = player.ContractExpiryDate == null ? string.Empty : player.ContractExpiryDate.Value.ToShortDateString();

                dt.Rows.Add(dr);
            }

            return dt;
        }*/

        private string GetScoutedPositionRating(PlayerType? scoutedPosition, ScoutingInformation ratings)
        {
            if (scoutedPosition == null)
            {
                return ratings.BestPosition.BestRole().Role.ToString();
            }

            switch (scoutedPosition)
            {
                case PlayerType.GoalKeeper:
                    return ratings.Goalkeeper.BestRole().AbilityRating.ToString();

                case PlayerType.RightBack:
                    return ratings.RightBack.BestRole().AbilityRating.ToString();

                case PlayerType.CentreHalf:
                    return ratings.CentreHalf.BestRole().AbilityRating.ToString();

                case PlayerType.LeftBack:
                    return ratings.LeftBack.BestRole().AbilityRating.ToString();

                case PlayerType.RightWingBack:
                    return ratings.RightWingBack.BestRole().AbilityRating.ToString();

                case PlayerType.DefensiveMidfielder:
                    return ratings.DefensiveMidfielder.BestRole().AbilityRating.ToString();

                case PlayerType.LeftWingBack:
                    return ratings.LeftWingBack.BestRole().AbilityRating.ToString();

                case PlayerType.RightMidfielder:
                    return ratings.RightMidfielder.BestRole().AbilityRating.ToString();

                case PlayerType.CentralMidfielder:
                    return ratings.CentreMidfielder.BestRole().AbilityRating.ToString();

                case PlayerType.LeftMidfielder:
                    return ratings.LeftMidfielder.BestRole().AbilityRating.ToString();

                case PlayerType.RightWinger:
                    return ratings.RightWinger.BestRole().AbilityRating.ToString();

                case PlayerType.AttackingMidfielder:
                    return ratings.AttackingMidfielder.BestRole().AbilityRating.ToString();

                case PlayerType.LeftWinger:
                    return ratings.LeftWinger.BestRole().AbilityRating.ToString();

                case PlayerType.CentreForward:
                    return ratings.CentreForward.BestRole().AbilityRating.ToString();

                default:
                    return ratings.BestPosition.BestRole().AbilityRating.ToString();
            }
        }

        private string GetScoutedRole(PlayerType? scoutedPosition, ScoutingInformation ratings)
        {
            if (scoutedPosition == null)
            {
                return ratings.BestPosition.BestRole().Role.ToString();
            }

            switch (scoutedPosition)
            {
                case PlayerType.GoalKeeper:
                    return ratings.Goalkeeper.BestRole().Role.ToString();

                case PlayerType.RightBack:
                    return ratings.RightBack.BestRole().Role.ToString();

                case PlayerType.CentreHalf:
                    return ratings.CentreHalf.BestRole().Role.ToString();

                case PlayerType.LeftBack:
                    return ratings.LeftBack.BestRole().Role.ToString();

                case PlayerType.RightWingBack:
                    return ratings.RightWingBack.BestRole().Role.ToString();

                case PlayerType.DefensiveMidfielder:
                    return ratings.DefensiveMidfielder.BestRole().Role.ToString();

                case PlayerType.LeftWingBack:
                    return ratings.LeftWingBack.BestRole().Role.ToString();

                case PlayerType.RightMidfielder:
                    return ratings.RightMidfielder.BestRole().Role.ToString();

                case PlayerType.CentralMidfielder:
                    return ratings.CentreMidfielder.BestRole().Role.ToString();

                case PlayerType.LeftMidfielder:
                    return ratings.LeftMidfielder.BestRole().Role.ToString();

                case PlayerType.RightWinger:
                    return ratings.RightWinger.BestRole().Role.ToString();

                case PlayerType.AttackingMidfielder:
                    return ratings.AttackingMidfielder.BestRole().Role.ToString();

                case PlayerType.LeftWinger:
                    return ratings.LeftWinger.BestRole().Role.ToString();

                case PlayerType.CentreForward:
                    return ratings.CentreForward.BestRole().Role.ToString();

                default:
                    return ratings.BestPosition.BestRole().Role.ToString();
            }
        }

        private string GetScoutedPosition(PlayerType position)
        {
            switch (position)
            {
                case PlayerType.GoalKeeper:
                    return "GK";

                case PlayerType.RightBack:
                    return "RB";

                case PlayerType.CentreHalf:
                    return "CD";

                case PlayerType.LeftBack:
                    return "LB";

                case PlayerType.RightWingBack:
                    return "RWB";

                case PlayerType.DefensiveMidfielder:
                    return "DM";

                case PlayerType.LeftWingBack:
                    return "LWB";

                case PlayerType.RightMidfielder:
                    return "RM";

                case PlayerType.CentralMidfielder:
                    return "CM";

                case PlayerType.LeftMidfielder:
                    return "LM";

                case PlayerType.RightWinger:
                    return "RW";

                case PlayerType.AttackingMidfielder:
                    return "AM";

                case PlayerType.LeftWinger:
                    return "LW";

                case PlayerType.CentreForward:
                    return "CF";

                default:
                    return "";
            }
        }

        private void PopulateStaticSearchControls()
        {
            PopulatePlayerTypes();
            PopulateAvailabilityTypes();
            PopulateReputationLevels();

            /*
            var clubNames = cmsUI.GetClubs().Select(x => x.Name).ToList();
            clubNames.Sort();
            cbxClubName.Items.AddRange(clubNames.ToArray());*/
        }

        private void CustomiseSearchOptions()
        {
            PopulateNationalities();
            PopulatePlayerBased();
            stpSearchCriteria.Visibility = Visibility.Visible;
        }

        #region Execute Search

        private void btnSearch_Click(object sender, EventArgs e)
        {
            SearchForPlayer();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportCurrentResults();
        }

        private void ExportCurrentResults()
        {
            var displayedResults = dgvPlayers.ItemsSource;

            if (!(displayedResults is List<GridViewPlayer>))
            {
                return;
            }

            var csvLines = cmsUI.CreateExportSet(((List<GridViewPlayer>)displayedResults).Select(x => x.PlayerId).ToList());

            ExportViaDialog(csvLines);
        }

        private void ExportViaDialog(string csv)
        {
            SaveFileDialog dlg = new SaveFileDialog();
            dlg.InitialDirectory = @"C:\";
            dlg.Filter = "CSV file (*.csv)|*.csv|All Files (*.*)|*.*";
            var result = dlg.ShowDialog();
            if (result.HasValue && result.Value)
            {
                //Save the file, assuming the DataContext is plain text (i.e. string)
                File.WriteAllText(dlg.FileName, csv);
            }
        }

        private void SearchForPlayer()
        {
            int playerIdNonNull;
            int? playerId;
            if (!int.TryParse(tbxPlayerId.Text, out playerIdNonNull))
            {
                playerId = null;
            }
            else
            {
                playerId = playerIdNonNull;
            }

            int maxValue;
            if (!int.TryParse(tbxMaxValue.Text, out maxValue))
            {
                maxValue = int.MaxValue;
            }

            byte maxAge;
            if (!byte.TryParse(tbxMaxAge.Text, out maxAge))
            {
                maxAge = byte.MaxValue;
            }

            long maxWage;
            if (string.IsNullOrEmpty(tbxMaxWage.Text) || !long.TryParse(tbxMaxWage.Text, out maxWage))
            {
                maxWage = long.MaxValue;
            }

            PlayerType castType;
            PlayerType? type;
            if (!Enum.TryParse(ddlPlayerType.Text, out castType))
            {
                type = null;
            }
            else
            {
                type = castType;
            }

            int? nationId = (int)ddlNationality.SelectedValue == -1 ? (int?)null : (int)ddlNationality.SelectedValue;

            string selectedPlaysInValue = (string)ddlPlayerBased.SelectedValue;
            string playsInRegion = null;
            int? playsInDivision = null;
            if (!string.IsNullOrWhiteSpace(selectedPlaysInValue) && selectedPlaysInValue.Length == 3 && selectedPlaysInValue.ToList().All(Char.IsLetter))
            {
                playsInRegion = (string)ddlPlayerBased.Text;
            }
            else
            {
                if (selectedPlaysInValue.ToList().All(Char.IsDigit))
                {
                    int parsedID = 0;
                    if (int.TryParse(selectedPlaysInValue, out parsedID))
                    {
                        playsInDivision = parsedID;
                    }
                }
            }

            AvailabilityCriteria availability;
            AvailabilityTypeControlHelper.GetSearchCriteria(ddlAvailability.SelectedValue.ToString(), out availability);

            int reputation = ReputationLevelControlHelper.GetMaxLevelCriteria((int)ddlReputation.SelectedValue);

            ScoutingRequest request = new ScoutingRequest()
            {
                PlayerId = playerId,
                PlayerType = type,
                MaxValue = maxValue,
                EUNationalityOnly = cbxEUNational.IsChecked == true,
                MaxAge = maxAge,
                MaxWage = maxWage,
                NumberOfResults = 50,
                PlaysInRegion = playsInRegion,
                PlaysInDivision = playsInDivision,
                Nationality = nationId,
                AvailabilityCriteria = availability,
                Reputation = reputation,
            };

            var playerList = cmsUI.GetScoutResults(request);
            DisplayPlayerList(playerList, request);
        }
        #endregion
    }
}
