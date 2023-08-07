using CMScouter.DataContracts;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CMScouterFunctions
{

    public static class SaveGameHandler
    {
        const int ByteBlockSize = 268;

        public static SaveGameData OpenSaveGameIntoMemory(string fileName, decimal valueMultiplier)
        {
            SaveGameFile savegame = new SaveGameFile();
            savegame.FileName = fileName;

            using (var sr = new StreamReader(fileName))
            {
                ReadFileHeaders(sr, savegame);
            }

            LoadGameData(savegame);

            var data = PlayerLoader.LoadPlayers(savegame, valueMultiplier);
            data.FileName = fileName;

            return data;
        }

        public static List<int> GetCountriesInRegion(Dictionary<int, Nation> nations, ActualRegion region)
        {
            return nations.Where(x => x.Value.ActualRegion == region).Select(y => y.Key).ToList();
        }

        private static void ReadFileHeaders(StreamReader sr, SaveGameFile savegame)
        {
            using (var br = new BinaryReader(sr.BaseStream))
            {
                if (br.ReadInt32() == 4)
                {
                    savegame.IsCompressed = true;

                    throw new LoadSaveFileException("Save Game Is Compressed And Cannot Be Read. Please Re-Save Uncompressed.");
                }

                sr.BaseStream.Seek(4, SeekOrigin.Current);

                var blockCount = br.ReadInt32();
                for (int j = 0; j < blockCount; j++)
                {
                    byte[] fileHeader = new byte[ByteBlockSize];
                    br.Read(fileHeader, 0, ByteBlockSize);
                    var internalName = ByteHandler.GetStringFromBytes(fileHeader, 8);

                    var fileFacts = DataFileFacts.GetDataFileFact(internalName);

                    savegame.DataBlockNameList.Add(new DataFile(fileFacts, ByteHandler.GetIntFromBytes(fileHeader, 0), ByteHandler.GetIntFromBytes(fileHeader, 4)));
                }
            }
        }

        private static void LoadGameData(SaveGameFile savegame)
        {
            var general = savegame.DataBlockNameList.First(x => x.FileFacts.Type == DataFileType.General);
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.General);

            ByteHandler.GetAllDataFromFile(general, savegame.FileName, fileFacts.DataSize);

            var fileData = ByteHandler.GetAllDataFromFile(general, savegame.FileName, fileFacts.DataSize);
            savegame.GameDate = ByteHandler.GetDateFromBytes(fileData[0], fileFacts.DataSize - 8).Value;
        }
    }
}
