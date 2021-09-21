using CMScouter.WPF.DataClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMScouter.WPF
{
    public class SettingsManager
    {
        private static string _assemblyPath;

        public SettingsManager(string path)
        {
            _assemblyPath = path;
        }
        
        private const string _userSettingsFilename = "savegames.json";

        private string GetSettingsFileName()
        {
            return Path.GetDirectoryName(_assemblyPath) + Path.DirectorySeparatorChar + _userSettingsFilename;
        }

        public Settings LoadSavedGameSettings()
        {
            // if default settings exist
            if (File.Exists(GetSettingsFileName()))
            {
                return Read(GetSettingsFileName());
            }
            else
            {
                return new Settings();
            }
        }

        public string SaveNewlyOpenedGame(string filePath, Settings settings, out bool neverSeenBefore)
        {
            neverSeenBefore = settings.RememberSavedGame(filePath);
            return SaveUserSettings(settings);
        }

        public string SaveUserSettings(Settings settings)
        {
            try
            {
                return Save(settings);
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private Settings Read(string path)
        {
            Settings settings = null;

            using (StreamReader sw = new StreamReader(path))
            {
                try
                {
                    settings = JsonSerializer.Deserialize<Settings>(sw.ReadToEnd());
                }
                catch
                {
                    settings = new Settings();
                }
            }

            return settings;
        }

        private string Save(Settings settings)
        {
            string settingsJson = JsonSerializer.Serialize(settings);

            try
            {
                using (StreamWriter sw = File.CreateText(GetSettingsFileName()))
                {
                    sw.Write(settingsJson);
                }
            }
            catch (Exception ex)
            {
                return ex.Message;
            }

            return null;
        }
    }
}
