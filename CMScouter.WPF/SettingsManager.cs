using CMScouter.WPF.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMScouter.WPF
{
    public static class SettingsManager
    {
        private const string _userSettingsFilename = "savegames.json";

        private static string _assemblyPath = System.AppDomain.CurrentDomain.BaseDirectory;

        private static string GetSettingsFileName()
        {
            return Path.GetDirectoryName(_assemblyPath) + Path.DirectorySeparatorChar + _userSettingsFilename;
        }

        public static Settings LoadSavedGameSettings()
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

        public static string SaveNewlyOpenedGame(string filePath, Settings settings, out bool neverSeenBefore)
        {
            neverSeenBefore = settings.RememberSavedGame(filePath);
            return SaveUserSettings(settings);
        }

        public static string SaveUserSettings(Settings settings)
        {
            try
            {
                return Save(settings);
            }
            catch(Exception ex)
            {
                return ex.Message;
            }
        }

        private static Settings Read(string path)
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

        private static string Save(Settings settings)
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
