using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CMScouterFunctions
{
    internal static class ByteHandler
    {
        private static readonly Encoding textEncoding = Encoding.GetEncoding("ISO-8859-1");

        public static short GetShortFromBytes(byte[] bytes, int start)
        {
            return BitConverter.ToInt16(bytes.Skip(start).Take(2).ToArray(), 0);
        }

        public static int GetIntFromBytes(byte[] bytes, int start)
        {
            return BitConverter.ToInt32(bytes.Skip(start).Take(4).ToArray(), 0);
        }

        public static string GetStringFromBytes(byte[] bytes, int start, int length = 0)
        {
            length = length > 0 ? length : bytes.Length;

            return GetStringFromBytesTerminated(bytes, start, length);
        }

        public static DateTime? GetDateFromBytes(byte[] bytes, int start)
        {
            return ConvertToDate(bytes.Skip(start).Take(5).ToArray());
        }

        public static DateTime? GetSensibleDateFromBytes(byte[] bytes, int start)
        {
            try
            {
                return ConvertToDate(bytes.Skip(start).Take(5).ToArray());
            }
            catch
            {
                return null;
            }
        }

        public static byte GetByteFromBytes(byte[] bytes, int start, bool isIntrinsicValue = false)
        {
            return isIntrinsicValue ? ConvertIntrinsic(bytes[start]) : bytes[start];
        }

        public static object GetObjectFromBytes(byte[] source, int dataFilePosition, Type type, int length = 0, bool isIntrinsicValue = false)
        {
            switch (Type.GetTypeCode(type))
            {
                case TypeCode.String:
                    return GetStringFromBytes(source, dataFilePosition, length);

                case TypeCode.Int32:
                    return GetIntFromBytes(source, dataFilePosition);

                case TypeCode.Int16:
                    return GetShortFromBytes(source, dataFilePosition);

                case TypeCode.DateTime:
                    return GetDateFromBytes(source, dataFilePosition);

                case TypeCode.Byte:
                    return GetByteFromBytes(source, dataFilePosition, isIntrinsicValue);

                case TypeCode.SByte:
                    return (sbyte)source[dataFilePosition];
            }

            return null;
        }

        public static List<byte[]> GetAllDataFromFile(DataFile dataFile, string fileName, int sizeOfData)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
            BinaryReader br = new BinaryReader(fs);

            int numberOfRecords = GetNumberOfRecordsFromDataFile(dataFile, sizeOfData, br, out int startReadPosition);

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

        public static List<string> GetPossibleShortValuesFromByteArray(byte[] source)
        {
            if (source == null || source.Length < 2)
            {
                return new List<string>();
            }

            var results = new List<string>();
            for (int i = 0; i < source.Length - 1; i++)
            {
                int result = GetShortFromBytes(source, i);

                if (result <= 0)
                {
                    continue;
                }

                results.Add($"{i} - {result}");
            }

            return results;
        }

        public static List<string> GetPossibleIntValuesFromByteArray(byte[] source)
        {
            if (source == null || source.Length < 4)
            {
                return new List<string>();
            }

            var results = new List<string>();
            for (int i = 0; i < source.Length - 3; i++)
            {
                int result = GetIntFromBytes(source, i);

                if (result <= 0)
                {
                    continue;
                }

                results.Add($"{i} - {result}");
            }

            return results;
        }

        public static List<string> GetPossibleDateValuesFromByteArray(byte[] source)
        {
            if (source == null || source.Length < 4)
            {
                return new List<string>();
            }

            var results = new List<string>();
            for (int i = 0; i < source.Length - 3; i++)
            {
                try
                {
                    DateTime? result = GetDateFromBytes(source, i);

                    if (result == null)
                    {
                        continue;
                    }

                    results.Add($"{i} - {result.Value.ToShortDateString()}");
                }
                catch
                {
                    // no need to catch anything
                }
            }

            return results;
        }

        private static byte[] TrimEnd(byte[] array)
        {
            int lastIndex = Array.FindIndex(array, b => b == 0);

            if (lastIndex >= 0)
            {
                Array.Resize(ref array, lastIndex);
            }

            return array;
        }

        private static string GetStringFromBytesTerminated(byte[] bytes, int start, int length)
        {
            var lengthBytes = bytes.Skip(start).Take(length).ToArray();
            return textEncoding.GetString(TrimEnd(lengthBytes));
        }

        private static byte ConvertIntrinsic(byte b)
        {
            if (b < 128)
            {
                return (byte)(128 + b);
            }
            else
            {
                return (byte)(b - 128);
            }
        }

        private static DateTime? ConvertToDate(byte[] bytes)
        {
            var day = BitConverter.ToInt16(bytes, 0);
            var year = BitConverter.ToInt16(bytes, 2);
            if (year <= 0)
            {
                return null;
            }

            /* Something is going on with Leap Years that I can't fathom..
             * Maybe its because the exe is patched to adjust the year?
            if (bytes[4] == 1 && day > 59)
            {
                day -= 1;
            }*/

            var date = new DateTime(year, 1, 1).AddDays(day);
            return date;
        }

        private static int GetNumberOfRecordsFromDataFile(DataFile dataFile, int sizeOfData, BinaryReader br, out int startReadPosition)
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

        public static string GetByteInvestigationOutput(byte[] source)
        {
            string byteHeader = string.Empty;
            string byteOutput = string.Empty;
            int headerId = 0;
            foreach (byte b in source)
            {
                byteHeader += headerId.ToString().PadLeft(3) + ",";
                byteOutput += b.ToString().PadLeft(3) + ",";

                headerId++;
            }

            StringBuilder nationBytes = new StringBuilder();
            nationBytes.AppendLine(byteHeader);
            nationBytes.AppendLine(byteOutput);

            return nationBytes.ToString();
        }
    }
}
