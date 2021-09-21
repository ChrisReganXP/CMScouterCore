using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMScouter.WPF.DataClasses
{
    public class Settings
    {
        private List<SavedGameSettings> _savedgames;

        public List<SavedGameSettings> saveGames { get { return _savedgames; } set { _savedgames = value ?? new List<SavedGameSettings>(); } }

        public Settings()
        {
            _savedgames = new List<SavedGameSettings>();
        }

        public bool RememberSavedGame(string fileName)
        {
            bool newGame = false;

            var game = _savedgames.FirstOrDefault(x => x.FilePath.Equals(fileName, StringComparison.InvariantCultureIgnoreCase));
            if (game == null)
            {
                game = new SavedGameSettings() { FilePath = fileName };
                newGame = true;
            }
            else
            {
                _savedgames.Remove(game);
            }

            _savedgames.Insert(0, game);
            return newGame;
        }

        public SavedGameSettings GetLastSavedGame()
        {
            var game = _savedgames.FirstOrDefault();
            if (game == null || !File.Exists(game.FilePath))
            {
                return null;
            }

            return game;
        }
    }

    public class SavedGameSettings
    {
        public string FilePath { get; set; }

        public decimal ValueMultiplier { get; set; }

        public int UserManagedTeam { get; set; }

        public Guid SelectedWeighting { get; set; }

        public SavedGameSettings()
        {
            UserManagedTeam = -1;
            ValueMultiplier = 1;
        }

        public string FileName { get => Path.GetFileName(FilePath); }
    }
}
