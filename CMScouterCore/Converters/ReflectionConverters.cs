using CMScouterFunctions.DataClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace CMScouterFunctions.Converters
{
    internal class NationTupleConverter : ITupleConverter<Nation>
    {
        private static readonly List<string> EUNations = new List<string>() {
            "AUSTRIA", "BELGIUM", "BULGARIA", "CROATIA", "CYRPUS", "CZECH REPUBLIC", "DENMARK", "ENGLAND", "ESTONIA", "SPAIN", "FINLAND", "FRANCE",
            "GERMANY", "GREECE", "HUNGARY", "REPUBLIC OF IRELAND", "ICELAND", "ITALY", "LATVIA", "LITHUANIA", "LUXEMBOURG", "MALTA", "NETHERLANDS", "NORTHERN IRELAND",
            "NORWAY", "POLAND", "PORTUGAL", "ROMANIA", "SCOTLAND", "SLOVAKIA", "SWITZERLAND", "SLOVENIA", "SWEDEN", "WALES"
        };

        Tuple<int, object> ITupleConverter<Nation>.Convert(byte[] source)
        {
            var nation = new Nation();
            ConverterReflection.SetConversionProperties(nation, source);

            nation.EUNation = EUNations.Contains(nation.Name, StringComparer.InvariantCultureIgnoreCase);

            return new Tuple<int, object>(nation.Id, nation);
        }
        public class StaffTupleConverter : ITupleConverter<Staff>
        {
            Tuple<int, object> ITupleConverter<Staff>.Convert(byte[] source)
            {
                var staff = new Staff();

                PropertyInfo[] props = staff.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                DataFileInfoAttribute[] attribs = new DataFileInfoAttribute[props.Length];

                for (int i = 0; i < attribs.Length; i++)
                {
                    attribs[i] = (DataFileInfoAttribute)props[i].GetCustomAttributes(typeof(DataFileInfoAttribute), true).FirstOrDefault();
                }

                ConverterReflection.SetConversionProperties(staff, source);
                //ConverterReflection.SetConversionProperties(staff, props, attribs, source);

                return new Tuple<int, object>(staff.StaffPlayerId, staff);
            }
        }
        internal class ClubTupleConverter : ITupleConverter<Club>
        {
            private static byte[] bytes;

            Tuple<int, object> ITupleConverter<Club>.Convert(byte[] sourceOfData)
            {
                bytes = sourceOfData;
                Club club = new Club();

                PropertyInfo[] props = club.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                DataFileInfoAttribute[] attribs = new DataFileInfoAttribute[props.Length];

                if (props == null)
                {
                    props = typeof(Club).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
                    attribs = new DataFileInfoAttribute[props.Length];

                    for (int i = 0; i < attribs.Length; i++)
                    {
                        attribs[i] = (DataFileInfoAttribute)props[i].GetCustomAttributes(typeof(DataFileInfoAttribute), true).FirstOrDefault();
                    }
                }
                
                ConverterReflection.SetConversionProperties(club, /*props, attribs,*/ bytes);

                var result = new Tuple<int, object>(club.ClubId, club);
                return result;
            }
        }
    }

    internal class PlayerConverter : ICMConverter<PlayerData>
    {
        public PlayerData Convert(byte[] source)
        {
            var player = new PlayerData();
            ConverterReflection.SetConversionProperties(player, source);
            return player;
        }
    }
    internal static class ConverterReflection
    {
        public static void SetConversionProperties(object target, byte[] source)
        {
            foreach (var prop in target.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly))
            {
                var positionAttribute = (DataFileInfoAttribute)prop.GetCustomAttributes(typeof(DataFileInfoAttribute), true).FirstOrDefault();

                if (positionAttribute != null)
                {
                    prop.SetValue(target, ByteHandler.GetObjectFromBytes(source, positionAttribute.DataFilePosition, prop.PropertyType, positionAttribute.Length, positionAttribute.IsIntrinsic));
                }
            }
        }
        public static void SetConversionProperties(object target, PropertyInfo[] props, DataFileInfoAttribute[] attribs, byte[] source)
        {
            for (int i = 0; i < props.Length; i++)
            {
                var prop = props[i];
                var positionAttribute = attribs[i];

                if (prop != null && positionAttribute != null)
                {
                    prop.SetValue(target, ByteHandler.GetObjectFromBytes(source, positionAttribute.DataFilePosition, prop.PropertyType, positionAttribute.Length, positionAttribute.IsIntrinsic));
                }
            }
        }
    }
}
