using System;
using System.Collections.Generic;

namespace CMScouterFunctions.DataClasses
{
    internal class SaveGameFile
    {
        public string FileName { get; set; }

        public bool IsCompressed { get; set; }

        public DateTime GameDate { get; set; }

        public List<DataFile> DataBlockNameList { get; set; }

        public SaveGameFile()
        {
            DataBlockNameList = new List<DataFile>();
        }
    }

    internal class DataFile
    {
        public DataFileFact FileFacts { get; }

        public int Position { get; }

        public int Length { get; }

        public DataFile(DataFileFact fileFacts, int position, int length)
        {
            FileFacts = fileFacts ?? new DataFileFact(DataFileType.General, string.Empty, 0, 0);
            Position = position;
            Length = length;
        }

        public override string ToString()
        {
            return $"{FileFacts.Name} [{FileFacts.Type}] ({Position}/{Length})";
        }
    }


    internal class DataFileHeaderInformation
    {
        public byte AdditionalHeaderIndicatorPosition { get; }

        public byte InitialNumberOfRecordsPosition { get; }

        public byte MinimumHeaderLength { get; }

        public byte ExtraHeaderLength { get; }

        public byte FurtherNumberOfRecordsPosition { get; }

        public DataFileHeaderInformation(byte additionalHeaderIndicatorPosition, byte initialNumberOfRecordsPosition, byte minimumHeaderLength, byte extraHeaderLength, byte furtherNumberOfRecordsPosition)
        {
            AdditionalHeaderIndicatorPosition = additionalHeaderIndicatorPosition;
            InitialNumberOfRecordsPosition = initialNumberOfRecordsPosition;
            MinimumHeaderLength = minimumHeaderLength;
            ExtraHeaderLength = extraHeaderLength;
            FurtherNumberOfRecordsPosition = furtherNumberOfRecordsPosition;
        }
    }
}
