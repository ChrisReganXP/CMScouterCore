using CMScouterFunctions.Converters;
using CMScouterFunctions.DataClasses;
using System.Collections.Generic;
using System.IO;
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
                staff.Value = (int)(staff.Value * gameData.ValueMultiplier);
                staff.Wage = (int)(staff.Wage * gameData.ValueMultiplier);

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

        public static Dictionary<int, Contract> GetDataFileContractDictionary(SaveGameFile savegame, SaveGameData gameData)
        {
            Dictionary<int, Contract> dic = new Dictionary<int, Contract>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == DataFileType.Contracts);
            var bytes = GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            ContractConverter converter = new ContractConverter(gameData);

            for (int i = 0; i < bytes.Count; i++)
            {
                var item = bytes[i];
                var contract = converter.Convert(item);

                if (contract.PlayerId != -1)
                {
                    if (!dic.ContainsKey(contract.PlayerId))
                    {
                        dic.Add(contract.PlayerId, contract);
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
            DataFile dataFile = savegame.DataBlockNameList.First(x => x.FileFacts.Type == fileType);
            return GetAllDataFromFile(dataFile, savegame.FileName, sizeOfData);
        }

        public static List<byte[]> GetAllDataFromFile(DataFile dataFile, string fileName, int sizeOfData)
        {
            using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                using (BinaryReader br = new BinaryReader(fs))
                {
                    int numberOfRecords = DataFileLoaders.GetNumberOfRecordsFromDataFile(dataFile, sizeOfData, br, out int startReadPosition);

                    br.BaseStream.Seek(startReadPosition, SeekOrigin.Begin);

                    List<byte[]> records = new List<byte[]>();

                    for (int i = 0; i < numberOfRecords; i++)
                    {
                        byte[] buffer = new byte[sizeOfData];
                        br.BaseStream.Read(buffer, 0, sizeOfData);
                        records.Add(buffer);
                    }

                    return records;
                }
            }
        }

        public static int GetNumberOfRecordsFromDataFile(DataFile dataFile, int sizeOfData, BinaryReader br, out int startReadPosition)
        {
            int numberOfRecords = dataFile.Length / sizeOfData;
            startReadPosition = dataFile.Position;

            if (dataFile.FileFacts.HeaderOverload != null)
            {
                byte[] header = new byte[dataFile.FileFacts.HeaderOverload.MinimumHeaderLength];
                br.BaseStream.Seek(startReadPosition, SeekOrigin.Begin);
                br.BaseStream.Read(header, 0, dataFile.FileFacts.HeaderOverload.MinimumHeaderLength);
                startReadPosition += dataFile.FileFacts.HeaderOverload.MinimumHeaderLength;

                var numberHeaderRows = ByteHandler.GetIntFromBytes(header, dataFile.FileFacts.HeaderOverload.AdditionalHeaderIndicatorPosition);
                numberOfRecords = ByteHandler.GetIntFromBytes(header, dataFile.FileFacts.HeaderOverload.InitialNumberOfRecordsPosition);
                int furtherNumberOfRecords = 0;

                if (numberHeaderRows > 0)
                {
                    for (int headerLoop = 0; headerLoop < numberHeaderRows; headerLoop++)
                    {
                        header = new byte[dataFile.FileFacts.HeaderOverload.ExtraHeaderLength];
                        br.BaseStream.Seek(startReadPosition, SeekOrigin.Begin);
                        br.BaseStream.Read(header, 0, dataFile.FileFacts.HeaderOverload.ExtraHeaderLength);
                        startReadPosition += dataFile.FileFacts.HeaderOverload.ExtraHeaderLength;
                    }
                    furtherNumberOfRecords = ByteHandler.GetIntFromBytes(header, dataFile.FileFacts.HeaderOverload.FurtherNumberOfRecordsPosition);
                }
                numberOfRecords = furtherNumberOfRecords > 0 ? furtherNumberOfRecords : numberOfRecords;
            }

            return numberOfRecords;
        }
    }
}
