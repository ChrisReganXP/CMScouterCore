using CMScouter.UI.DataClasses;
using CMScouter.WPF.DataClasses;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CMScouter.WPF
{
    public class WeightingManager
    {
        private static string _assemblyPath;

        public WeightingManager(string path)
        {
            _assemblyPath = path;
        }

        private const string _defaultWeightingFile = "DefaultWeights.json";
        private const string _customWeightingFile = "CustomWeights.json";

        private string GetDefaultWeightingFileName()
        {
            return Path.GetDirectoryName(_assemblyPath) + Path.DirectorySeparatorChar + _defaultWeightingFile;
        }

        private string GetCustomWeightingFileName()
        {
            return Path.GetDirectoryName(_assemblyPath) + Path.DirectorySeparatorChar + _customWeightingFile;
        }

        public WeightCollection GetWeightings()
        {
            WeightCollection full = new WeightCollection();

            if (File.Exists(GetDefaultWeightingFileName()))
            {
                var defaults = Read(GetDefaultWeightingFileName());
                full.GroupedWeights.AddRange(defaults.GroupedWeights);
                full.IndividualWeights.AddRange(defaults.IndividualWeights);
            }

            if (File.Exists(GetCustomWeightingFileName()))
            {
                var customs = Read(GetCustomWeightingFileName());
                full.GroupedWeights.AddRange(customs.GroupedWeights);
                full.IndividualWeights.AddRange(customs.IndividualWeights);
            }

            return full;
        }

        private WeightCollection Read(string path)
        {
            WeightCollection collection = null;

            using (StreamReader sw = new StreamReader(path))
            {
                try
                {
                    collection = JsonSerializer.Deserialize<WeightCollection>(sw.ReadToEnd());
                }
                catch
                {
                    collection = new WeightCollection();
                }
            }

            return collection;
        }
    }
}
