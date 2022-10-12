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
using System.Windows;
using System.Windows.Controls;
using CMScouter.WPF.ControlHelpers;
using CMScouter.UI.DataClasses;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Threading;
using System.Diagnostics;
using System.Threading;

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

        #endregion

        #region Reloading Form

        public void RefreshAfterSettingsSave()
        {
            ChangeMenusOnSaveGameLoad();
            ResetAndTeamSearch();
        }

        private void ResetAndTeamSearch()
        {
            PerformInitialSearch();
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

        private async void Open_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Save Game Files (*.sav)|*.sav";
            openFileDialog.DefaultExt = "sav";
            if (openFileDialog.ShowDialog() == true)
            {
                LoadSaveGameFile(openFileDialog.FileName);
            }
        }

        private async void LoadLastGame_Click(object sender, RoutedEventArgs e)
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
                lblStatusInfo.Content = string.Join(". ", cmsUI.GetLoadingFailures());
                return;
            }

            Globals.Instance.SetGameDate(cmsUI.GameDate);

            ChangeMenusOnSaveGameLoad();

            if (neverSeenBefore)
            {
                ShowSaveGameSettingsDialog();
            }
            else
            {
                string currentFileName = settings.GetLastSavedGame()?.FilePath;
                if (currentFileName == null || currentFileName != newGame.FilePath || ucScouting.NoSearchAttempted())
                {
                    ucScouting.WakeUp(cmsUI, settings);
                    ucScouting.CustomiseSearchOptions();
                    ucScouting.ResetAndTeamSearch();
                }
                else
                {
                    ucScouting.RepeatSearch();
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
            ucScouting.PerformInitialSearch();
        }

        #endregion

    }
}
