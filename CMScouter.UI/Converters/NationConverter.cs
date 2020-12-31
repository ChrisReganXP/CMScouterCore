using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Text;

namespace CMScouter.UI.Converters
{
    public static class NationConverter
    {
        public static Dictionary<int, NationView> ConvertNations(Dictionary<int, Nation> nations)
        {
            Dictionary<int, NationView> dic = new Dictionary<int, NationView>();

            foreach (var key in nations.Keys)
            {
                dic[key] = ConvertNation(nations[key]);
            }

            return dic;
        }

        public static NationView ConvertNation(Nation n)
        {
            return new NationView()
            {
                Id = n.Id,
                Name = n.Name,
                EUNation = n.EUNation,
            };
        }
    }
}
