using System;

namespace CMScouterFunctions.Converters
{
    internal interface ITupleConverter<out TTarget> where TTarget : class
    {
        Tuple<int, object> Convert(byte[] source);
    }

    internal interface ICMConverter<out TTarget>
    {
        TTarget Convert(byte[] source);
    }
}
