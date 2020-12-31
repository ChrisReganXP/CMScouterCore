using CMScouterFunctions.DataClasses;
using System;
using static CMScouterFunctions.Converters.NationTupleConverter;

namespace CMScouterFunctions.Converters
{
    internal static class ConverterIdFactory
    {
        public static ITupleConverter<T> CreateTupleConverter<T>() where T : class
        {
            if (typeof(T) == typeof(Staff))
            {
                return (ITupleConverter<T>)new StaffTupleConverter();
            }

            if (typeof(T) == typeof(Club))
            {
                return (ITupleConverter<T>)new ClubTupleConverter();
            }
            
            if (typeof(T) == typeof(Nation))
            {
                return (ITupleConverter<T>)new NationTupleConverter();
            }
            
            throw new NotImplementedException("No Class Converter");
        }
    }

    internal static class ConverterFactory
    {
        public static ICMConverter<T> CreateConverter<T>()
        {
            if (typeof(T) == typeof(PlayerData))
            {
                return (ICMConverter<T>)new PlayerConverter();
            }

            throw new NotImplementedException("Unknown Object Converter Needed");
        }
    }
}
