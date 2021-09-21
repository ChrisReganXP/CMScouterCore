﻿using CMScouter.UI;
using CMScouter.UI.Raters;
using CMScouter.WPF.Converters;
using CMScouter.WPF.DataClasses;
using CMScouter.DataClasses;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using CMScouter.WPF.ControlHelpers;
using CMScouter.UI.DataClasses;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;

namespace CMScouter.WPF
{
    /// <summary>
    /// Interaction logic for ScoutingForm.xaml
    /// </summary>
    public partial class ScoutingForm : Window
    {
        private static string _assemblyPath = Process.GetCurrentProcess().MainModule.FileName;

        private SettingsManager settingsManager;

        private CMScouterUI cmsUI;

        private Settings settings;

        private static string DefaultWeightingPath { get => Path.GetDirectoryName(_assemblyPath) + "\\DefaultWeights.json"; }

        private WeightingManager weightingManager;

        public ScoutingForm()
        {
            InitializeComponent();
            HandleInitialSettings();
            PopulateInitialItems();
        }

        #region Initial Opening

        private void HandleInitialSettings()
        {
            settingsManager = new SettingsManager(_assemblyPath); 
            settings = settingsManager.LoadSavedGameSettings();
            var lastGame = settings.GetLastSavedGame();
            if (lastGame == null)
            {
                return;
            }

            AddLastGameMenuItem();

            weightingManager = new WeightingManager(DefaultWeightingPath);
        }

        private void PopulateInitialItems()
        {
            dgvPlayers.Visibility = Visibility.Hidden;
            dgvPlayers.AutoGenerateColumns = false;

            stpSearchCriteria.Visibility = Visibility.Collapsed;

            PopulateStaticSearchControls();
        }

        private void PopulateStaticSearchControls()
        {
            PopulatePlayerTypes();
            PopulateAvailabilityTypes();
            PopulateReputationLevels();
        }

        private void PopulatePlayerTypes()
        {
            ddlPlayerType.ItemsSource = null;
            ddlPlayerType.DisplayMemberPath = "Name";
            ddlPlayerType.SelectedValuePath = "Position";

            List<KeyValuePair<int, string>> positions = new List<KeyValuePair<int, string>>();
            var positionsArray = Enum.GetValues(typeof(PlayerPosition)).Cast<PlayerPosition>().Select(x => new { Position = (int)x, Name = x.ToName() }).ToList();
            positionsArray.Insert(0, new { Position = -1, Name = "<All>" });

            ddlPlayerType.ItemsSource = positionsArray;
            ddlPlayerType.SelectedIndex = 0;
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

        private void PopulateClubs()
        {
            var clubNames = cmsUI.GetClubs().Select(x => new { x.ClubId, x.Name }).OrderBy(x => x.Name).ToList();
            var all = new { ClubId = -1, Name = "<All>" };
            clubNames.Insert(0, all);

            cbxClubs.DisplayMemberPath = "Name";
            cbxClubs.SelectedValuePath = "ClubId";

            cbxClubs.ItemsSource = clubNames;
            cbxClubs.SelectedIndex = 0;
        }

        #endregion

        #region Reloading Form

        public void RefreshAfterSettingsSave()
        {
            ChangeMenusOnSaveGameLoad();
            ResetAndTeamSearch();
        }

        private void ResetAndTeamSearch()
        {
            ResetSearchFields();
            PerformInitialSearch();
        }

        private void RepeatSearch()
        {
            PerformSearch();
        }

        private bool NoSearchAttempted()
        {
            if (ddlPlayerType.SelectedIndex == 0
                && ddlAvailability.SelectedIndex == 0
                && ddlReputation.SelectedIndex == 0
                && string.IsNullOrWhiteSpace(tbxMaxAge.Text)
                && string.IsNullOrWhiteSpace(tbxMaxValue.Text)
                && string.IsNullOrWhiteSpace(tbxMaxWage.Text)
                && ddlPlayerBased.SelectedIndex == 0
                && ddlNationality.SelectedIndex == 0
                && cbxEUNational.IsChecked == false
                && cbxClubs.SelectedIndex == 0
                && string.IsNullOrWhiteSpace(tbxPlayerId.Text)
                && string.IsNullOrWhiteSpace(tbxTextSearch.Text))
            {
                return true;
            }

            return false;
        }

        private void CustomiseSearchOptions()
        {
            PopulateNationalities();
            PopulatePlayerBased();
            PopulateClubs();
            stpSearchCriteria.Visibility = Visibility.Visible;

            PopulateCustomSearches();
        }

        private void PopulateCustomSearches()
        {
            var customSearches = cmsUI.GetCustomSearchList();
            if (customSearches?.Count == 0)
            {
                cbxCustomSearch.IsEnabled = false;
                return;
            }

            customSearches.Insert(0, new Tuple<string, string>("", "<None>"));
            cbxCustomSearch.ItemsSource = customSearches;
            cbxCustomSearch.DisplayMemberPath = "Item2";
            cbxCustomSearch.SelectedIndex = 0;
        }

        #endregion

        #region Menu Handling

        private void AddLastGameMenuItem()
        {
            menLastGame.Header = $"Load Last Game ({settings.GetLastSavedGame().FileName})";
            menLastGame.Visibility = Visibility.Visible;
        }

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

        private void LoadLastGame_Click(object sender, RoutedEventArgs e)
        {
            LoadSaveGameFile(settings.GetLastSavedGame().FilePath);
        }

        private void ChangeMenusOnSaveGameLoad()
        {
            menLastGame.Header = "Reload";
            menLastGame.Visibility = Visibility.Visible;

            menSettings.IsEnabled = true;
        }

        private void SettingsMenu_Click(object sender, RoutedEventArgs e)
        {
            ShowSaveGameSettingsDialog();
        }

        #endregion

        #region Load Data

        private void LoadSaveGameFile(string fileName)
        {
            bool neverSeenBefore;
            lblStatusInfo.Content = string.Empty;

            string currentFileName = dgvPlayers.Visibility == Visibility.Hidden ? null : settings.GetLastSavedGame()?.FilePath;
            string errorResult = settingsManager.SaveNewlyOpenedGame(fileName, settings, out neverSeenBefore);

            lblStatusInfo.Content = errorResult;

            var newGame = settings.GetLastSavedGame();
            if (newGame == null)
            {
                return;
            }

            if (string.IsNullOrEmpty((string)lblStatusInfo.Content))
            {
                lblStatusInfo.Content = "Loaded : " + newGame.FilePath;
            }

            cmsUI = new CMScouterUI(fileName, newGame.ValueMultiplier, DefaultWeightingPath, newGame.SelectedWeighting);

            if (cmsUI.GetLoadingFailures().Any())
            {
                dgvPlayers.Visibility = Visibility.Hidden;
                lblStatusInfo.Content = string.Join(". ", cmsUI.GetLoadingFailures());
                return;
            }

            Globals.Instance.SetGameDate(cmsUI.GameDate);

            ChangeMenusOnSaveGameLoad();

            if (neverSeenBefore)
            {
                CustomiseSearchOptions();
                ShowSaveGameSettingsDialog();
            }
            else
            {
                if (currentFileName == null || currentFileName != newGame.FilePath || NoSearchAttempted())
                {
                    CustomiseSearchOptions();
                    ResetAndTeamSearch();
                }
                else
                {
                    RepeatSearch();
                }
            }
        }

        private void ShowSaveGameSettingsDialog()
        {
            SaveGameSettingsDialog sgd = new SaveGameSettingsDialog(this, settingsManager, settings, cmsUI, weightingManager);
            sgd.Show();
        }

        #endregion

        #region Show Players

        private void PerformInitialSearch()
        {
            if (settings.GetLastSavedGame() == null || settings.GetLastSavedGame().UserManagedTeam < 0)
            {
                return;
            }

            cbxClubs.SelectedValue = settings.GetLastSavedGame().UserManagedTeam;
            PerformSearch();
            cbxClubs.SelectedIndex = 0;
        }

        private void DisplayPlayerList(List<PlayerView> playerList, ScoutingRequest request)
        {
            List<GridViewPlayer> playerViewList = CreatePlayerViews(playerList, request.PlayerType).ToList();

            dgvPlayers.ItemsSource = playerViewList;
            dgvPlayers.Visibility = Visibility.Visible;
        }

        private IEnumerable<GridViewPlayer> CreatePlayerViews(List<PlayerView> originalPlayers, PlayerPosition? playerType)
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

            PlayerForm details = new PlayerForm(cmsUI, player);
            details.Show();
        }

        #endregion

        #region Button Handling

        private void btnReset_Click(object sender, EventArgs e)
        {
            ResetSearchFields();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            PerformSearch();
        }

        private void btnExport_Click(object sender, EventArgs e)
        {
            ExportCurrentResults();
        }

        #endregion

        #region Execute Search

        private async void PerformSearch()
        {
            LockUIDuringSearch();
            dgvPlayers.Visibility = Visibility.Hidden;
            Mouse.OverrideCursor = Cursors.Wait;
            await SearchForPlayers();
            Mouse.OverrideCursor = Cursors.Arrow;
            UnlockUIDuringSearch();
        }

        private void ResetSearchFields()
        {
            PopulateStaticSearchControls();
            CustomiseSearchOptions();
            tbxMaxAge.Clear();
            tbxMaxValue.Clear();
            tbxMaxWage.Clear();
            tbxPlayerId.Clear();
            cbxEUNational.IsChecked = false;
            tbxTextSearch.Clear();
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
                File.WriteAllText(dlg.FileName, csv);
            }
        }

        private async Task SearchForPlayers()
        {
            string customSearchID = null;
            if (cbxCustomSearch.SelectedIndex > 0)
            {
                customSearchID = ((Tuple<string, string>)cbxCustomSearch.SelectedValue).Item1;
            }

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

            PlayerPosition? type = null;

            if ((int)ddlPlayerType.SelectedValue >= 0)
            {
                type = (PlayerPosition)ddlPlayerType.SelectedValue;
            }

            int? nationId = (int)ddlNationality.SelectedValue == -1 ? (int?)null : (int)ddlNationality.SelectedValue;

            int? clubId = (int)cbxClubs.SelectedValue == -1 ? (int?)null : (int)cbxClubs.SelectedValue;

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
                CustomSearch = customSearchID,
                PlayerId = playerId,
                TextSearch = tbxTextSearch.Text,
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
                ClubId = clubId,
                OutputDebug = playerId.HasValue
            };

            pbrLoadPlayers.Visibility = Visibility.Visible;
            pbrLoadPlayers.IsIndeterminate = true;

            List<PlayerView> playerList = await Task.Run(() => cmsUI.GetScoutResults(request));

            pbrLoadPlayers.IsIndeterminate = false;
            pbrLoadPlayers.Visibility = Visibility.Collapsed;

            DisplayPlayerList(playerList, request);
        }

        private void LockUIDuringSearch()
        {
            menSettings.IsEnabled = false;
        }

        private void UnlockUIDuringSearch()
        {
            menSettings.IsEnabled = true;
        }

        #endregion

    }
}
