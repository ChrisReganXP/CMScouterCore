using CMScouter.DataContracts;
using CMScouterFunctions;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
