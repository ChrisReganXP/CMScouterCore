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
        public DataFileType FileType { get; set; }

        public string InternalName { get; set; }

        public int Position { get; set; }

        public int Length { get; set; }

        public override string ToString()
        {
            return $"{InternalName} [{FileType}] ({Position}/{Length})";
        }
    }
}
