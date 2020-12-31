using CMScouterFunctions.Converters;
using CMScouterFunctions.DataClasses;
using System.Collections.Generic;
using System.Linq;

namespace CMScouterFunctions
{
    internal class ReflectionLoaders
    {
        public static Dictionary<int, T> GetDataFileConvertedIdDictionary<T>(ITupleConverter<T> converter, SaveGameFile savegame, DataFileType type, out List<T> duplicates) where T : class
        {
            duplicates = new List<T>();
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == type);
            var bytes = DataFileLoaders.GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);

            Dictionary<int, T> dic = new Dictionary<int, T>();
            List<T> invalidIds = new List<T>();

            foreach (var item in bytes)
            {
                var converted = converter.Convert(item);

                if (converted.Item1 == -1)
                {
                    invalidIds.Add(converted.Item2 as T);
                }
                else
                {
                    if (dic.ContainsKey(converted.Item1))
                    {
                        duplicates.Add(converted.Item2 as T);
                    }
                    else
                    {
                        dic.Add(converted.Item1, converted.Item2 as T);
                    }
                }
            }

            return dic;
        }

        private static Dictionary<int, T> GetDataFileConvertedIdDictionary<T>(SaveGameFile savegame, DataFileType type, out List<T> duplicates) where T : class
        {
            var converter = ConverterIdFactory.CreateTupleConverter<T>();
            return GetDataFileConvertedIdDictionary(converter, savegame, type, out duplicates);
        }

        private static List<T> GetDataFileConverted<T>(SaveGameFile savegame, DataFileType type)
        {
            var fileFacts = DataFileFacts.GetDataFileFacts().First(x => x.Type == type);
            var bytes = DataFileLoaders.GetDataFileBytes(savegame, fileFacts.Type, fileFacts.DataSize);
            var converter = ConverterFactory.CreateConverter<T>();
            var collect = ConvertToCMObject<T>(bytes, converter).ToList();
            return collect;
        }

        private static IEnumerable<TOutput> ConvertToCMObject<TOutput>(List<byte[]> bytes, ICMConverter<TOutput> converter)
        {
            foreach (var data in bytes)
            {
                yield return converter.Convert(data);
            }
        }
    }
}
