using CMScouterCore.DataClasses;
using CMScouterFunctions.Converters;
using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CMScouterFunctions
{
    internal static class PlayerLoader
    {
        public static SaveGameData LoadPlayers(SaveGameFile savegame, decimal valueMultiplier)
        {
            SaveGameData saveData = new SaveGameData();
            saveData.ValueMultiplier = valueMultiplier;

            Dictionary<int, Club_Comp> clubcomps = DataFileLoaders.GetDataFileClubCompetitionDictionary(savegame);

            Dictionary<int, string> firstnames = GetDataFileStringsDictionary(savegame, DataFileType.First_Names);

            Dictionary<int, string> secondNames = GetDataFileStringsDictionary(savegame, DataFileType.Second_Names);

            Dictionary<int, string> commonNames = GetDataFileStringsDictionary(savegame, DataFileType.Common_Names);

            Dictionary<int, Nation> nations = DataFileLoaders.GetDataFileNationDictionary(savegame); 
            
            Dictionary<int, Club> clubs = DataFileLoaders.GetDataFileClubDictionary(savegame);

            List<Staff> duplicates = new List<Staff>();
            Dictionary<int, Staff> staffDic = DataFileLoaders.GetDataFileStaffDictionary(savegame, saveData, out duplicates);

            Dictionary<int, Retirement> playerRetirements = DataFileLoaders.GetDataFileRetirementDictionary(savegame);
            foreach (var retirement in playerRetirements)
            {
                var staff = staffDic.Values.FirstOrDefault(x => x.StaffId == retirement.Key);
                if (staff != null)
                {
                    staff.Retirement = retirement.Value;
                }
            }

            Dictionary<int, PlayerData> players = GetDataFilePlayerData(savegame);

            Dictionary<int, Contract> playerContracts = DataFileLoaders.GetDataFileContractDictionary(savegame, saveData);

            List<Player> searchablePlayers = ConstructSearchablePlayers(staffDic, players, playerContracts).ToList();

            List<Player> retiredPlayers = searchablePlayers.Where(x => x._staff.Retirement != null).ToList();

            saveData.GameDate = savegame.GameDate;
            saveData.FirstNames = firstnames;
            saveData.Surnames = secondNames;
            saveData.CommonNames = commonNames;
            saveData.Nations = nations;
            saveData.Clubs = clubs;
            saveData.Players = searchablePlayers;
            saveData.ClubComps = clubcomps;

            return saveData;
        }


        private static IEnumerable<Player> ConstructSearchablePlayers(Dictionary<int, Staff> staffDic, Dictionary<int, PlayerData> players, Dictionary<int, Contract> contracts)
        {
            foreach (var playerID in players.Keys)
            {
                if (staffDic.ContainsKey(playerID))
                {
                    var staff = staffDic[playerID];
                    Contract contract = null;
                    if (contracts.Keys.Contains(staff.StaffId))
                    {
                        contract = contracts[staff.StaffId];
                    }

                    yield return new Player(players[playerID], staff, contract);
                }
            }
        }

        private static Dictionary<int, string> GetDataFileStringsDictionary(SaveGameFile savegame, DataFileType type)
        {
            Dictionary<int, string> fileContents = new Dictionary<int, string>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == type);
            var fileData = DataFileLoaders.GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            for (int i = 0; i < fileData.Count; i++)
            {
                fileContents.Add(i, ByteHandler.GetStringFromBytes(fileData[i], 0, fileFacts.StringLength));
            }

            return fileContents;
        }

        private static Dictionary<int, PlayerData> GetDataFilePlayerData(SaveGameFile savegame)
        {
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Players);
            var bytes = DataFileLoaders.GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);
            var converter = new PlayerDataConverter();
            var collect = new Dictionary<int, PlayerData>();

            foreach (var source in bytes)
            {
                var player = converter.Convert(source);

                collect.Add(player.PlayerId, player);
            }

            return collect;
        }
    }
}
