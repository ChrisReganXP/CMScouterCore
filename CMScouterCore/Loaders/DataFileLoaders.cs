using CMScouterFunctions.Converters;
using CMScouterFunctions.DataClasses;
using System.Collections.Generic;
using System.Linq;

namespace CMScouterFunctions
{
    internal static class DataFileLoaders
    {
        public static Dictionary<int, Staff> GetDataFileStaffDictionary(SaveGameFile savegame, SaveGameData gameData, out List<Staff> duplicateStaff)
        {
            Dictionary<int, Staff> dic = new Dictionary<int, Staff>();
            duplicateStaff = new List<Staff>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Staff);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            StaffConverter converter = new StaffConverter();

            foreach (var item in bytes)
            {
                var staff = converter.Convert(item);
                staff.Value = staff.Value * gameData.ValueMultiplier;
                staff.Wage = staff.Wage * gameData.ValueMultiplier;

                if (staff.StaffPlayerId != -1)
                {
                    if (dic.ContainsKey(staff.StaffPlayerId))
                    {
                        duplicateStaff.Add(staff);
                    }
                    else
                    {
                        dic.Add(staff.StaffPlayerId, staff);
                    }
                }
            }

            return dic;
        }

        public static Dictionary<int, Club> GetDataFileClubDictionary(SaveGameFile savegame)
        {
            Dictionary<int, Club> dic = new Dictionary<int, Club>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Clubs);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            ClubConverter converter = new ClubConverter();

            foreach (var item in bytes)
            {
                var club = converter.Convert(item);

                if (club.ClubId != -1)
                {
                    if (!dic.ContainsKey(club.ClubId))
                    {
                        dic.Add(club.ClubId, club);
                    }
                }
            }

            return dic;
        }

        public static Dictionary<int, Club_Comp> GetDataFileClubCompetitionDictionary(SaveGameFile savegame)
        {
            Dictionary<int, Club_Comp> dic = new Dictionary<int, Club_Comp>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Club_Comps);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            var converter = new ClubCompConverter();

            foreach (var item in bytes)
            {
                var comp = converter.Convert(item);
                dic.Add(comp.Id, comp);
            }

            return dic;
        }

        public static Dictionary<int, Nation> GetDataFileNationDictionary(SaveGameFile savegame)
        {
            Dictionary<int, Nation> dic = new Dictionary<int, Nation>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Nations);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            NationConverter converter = new NationConverter();

            foreach (var item in bytes)
            {
                var nation = converter.Convert(item);

                if (nation.Id != -1)
                {
                    if (!dic.ContainsKey(nation.Id))
                    {
                        dic.Add(nation.Id, nation);
                    }
                }
            }

            return dic;
        }

        public static List<byte[]> GetDataFileBytes(SaveGameFile savegame, DataFileType fileType, int sizeOfData)
        {
            DataFile dataFile = savegame.DataBlockNameList.First(x => x.FileType == fileType);
            return ByteHandler.GetAllDataFromFile(dataFile, savegame.FileName, sizeOfData);
        }
    }
}
