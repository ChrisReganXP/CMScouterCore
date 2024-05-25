using CMScouter.DataContracts;
using CMScouter.UI.DataClasses;
using CMScouterFunctions;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;

namespace CMScouter.UI
{
    internal static class FileFunctions
    {
        public static SaveGameData LoadSaveGameFile(string fileName, decimal valueMultiplier)
        {
            try
            {
                return SaveGameHandler.OpenSaveGameIntoMemory(fileName, valueMultiplier);
            }
            catch (LoadSaveFileException ex)
            {
                var data = new SaveGameData();
                data.LoadingFailures.Add(ex.Message);
                return data;
            }
        }

        internal static void WriteShortlistFile(string filePath, List<PlayerShortlistEntry> shortlist, out string failureMessage)
        {
            failureMessage = null;
            byte[] shortlistData = ShortlistHelper.GetShortlistBytes(shortlist);

            try
            {
                File.WriteAllBytes(filePath, shortlistData);
            }
            catch (Exception ex)
            {
                failureMessage = ex.Message;
            }
        }
    }
}
