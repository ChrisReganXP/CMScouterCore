using CMScouterFunctions;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI
{
    internal static class FileFunctions
    {
        public static SaveGameData LoadSaveGameFile(string fileName)
        {
            return SaveGameHandler.OpenSaveGameIntoMemory(fileName);
        }
    }
}
