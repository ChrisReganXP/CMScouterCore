using CMScouter.UI;
using CMScouter.WPF.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
    /// Interaction logic for SaveGameSettingsDialog.xaml
    /// </summary>
    public partial class SaveGameSettingsDialog : Window
    {
        Settings _settings;
        SavedGameSettings _newgame;
        CMScouterUI _cmsui;
        ScoutingForm _parent;
        SettingsManager _settingsManager;
        WeightingManager _weightingManager;

        public SaveGameSettingsDialog(ScoutingForm parent, SettingsManager settingsManager, Settings settings, CMScouterUI cmsUI, WeightingManager weightingManager)
        {
            _settingsManager = settingsManager;
            _newgame = settings.GetLastSavedGame();
            _cmsui = cmsUI;
            _settings = settings;
            _parent = parent;
            _weightingManager = weightingManager;

            InitializeComponent();
            BindData();
        }

        private void BindData()
        {
            var clubNames = _cmsui.GetClubs().Select(x => new { x.ClubId, x.Name }).OrderBy(x => x.Name).ToList();
            var all = new { ClubId = -1, Name = "<None>" };
            clubNames.Insert(0, all);

            cbxClubs.DisplayMemberPath = "Name";
            cbxClubs.SelectedValuePath = "ClubId";

            cbxClubs.ItemsSource = clubNames;

            if (_newgame.UserManagedTeam >= 0)
            {
                cbxClubs.SelectedValue = _newgame.UserManagedTeam;
            }
            else
            {
                cbxClubs.SelectedIndex = 0;
            }

            if (_newgame.ValueMultiplier > 0)
            {
                tbxInflation.Text = _newgame.ValueMultiplier.ToString();
            }

            cbxWeightings.DisplayMemberPath = "Name";
            cbxWeightings.SelectedValuePath = "ID";
            var weights = _weightingManager.GetWeightings();
            var weightList = weights.GroupedWeights.Select(x => new { x.ID, x.Name }).ToList();
            weightList.AddRange(weights.IndividualWeights.Select(x => new { x.ID, x.Name }).ToList());
            cbxWeightings.ItemsSource = weightList;

            if (_newgame.SelectedWeighting != Guid.Empty)
            {
                cbxWeightings.SelectedValue = _newgame.SelectedWeighting;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            decimal selectedInflation;
            decimal.TryParse(tbxInflation.Text, out selectedInflation);
            int selectedClub = (int)cbxClubs.SelectedValue;
            Guid selectedWeighting = (Guid)cbxWeightings.SelectedValue;

            if (!ValidateSettings(selectedClub, selectedInflation))
            {
                return;
            }

            _newgame.UserManagedTeam = selectedClub;
            _newgame.ValueMultiplier = selectedInflation;
            _newgame.SelectedWeighting = selectedWeighting;

            _settingsManager.SaveUserSettings(_settings);

            _cmsui.UpdateInflationValue(_newgame.ValueMultiplier);

            var selectedGroupWeight = _weightingManager.GetWeightings().GroupedWeights.FirstOrDefault(x => x.ID == selectedWeighting);
            var selectedIndividualWeight = _weightingManager.GetWeightings().IndividualWeights.FirstOrDefault(x => x.ID == selectedWeighting);

            if (selectedGroupWeight != null)
            {
                _cmsui.UpdateGroupedWeighting(selectedGroupWeight);
            }
            else
            {
                _cmsui.UpdateIndividualWeighting(selectedIndividualWeight);
            }

            this.Close();
            _parent.RefreshAfterSettingsSave();
        }

        private bool ValidateSettings(int club, decimal inflation)
        {
            if (club < 0)
            {
                return false;
            }

            if (inflation < 1 || inflation > 10)
            {
                return false;
            }

            return true;
        }

        private void tbxInflation_ValidateTyping(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9\\.]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
