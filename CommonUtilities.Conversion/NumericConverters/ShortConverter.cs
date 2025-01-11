using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.NumericConverters
{
    public struct ShortConverter : IConverter
    {
        public uint Priority { get; }
        public Type TargetType { get; }
        public bool CanConvert(string value)
        {
            throw new NotImplementedException();
        }

        public object Convert(string value)
        {
            throw new NotImplementedException();
        }
    }
}