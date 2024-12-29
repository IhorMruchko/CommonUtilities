using System;
using System.Collections.Generic;
using System.Linq;

namespace CommonUtilities.Conversion
{
    public static class ConversionManager
    {
        /* TODO: add those types as default converters
            uint	System.UInt32
            nint	System.IntPtr
            nuint	System.UIntPtr
            long	System.Int64
            ulong	System.UInt64
            short	System.Int16
            ushort	System.UInt16
        */
        private static readonly List<IConverter> Converters;
    
        static ConversionManager()
        {
            Converters = AppDomain.CurrentDomain
                .GetAssemblies()
                .Where(assembly => assembly.GetName() != typeof(ConversionManager).Assembly.GetName())
                .SelectMany(assembly => assembly.GetTypes())
                .Where(type => type.GetInterface(nameof(IConverter)) != null 
                            && type.GetConstructor(Array.Empty<Type>()) != null)
                .Select(type => (IConverter)Activator.CreateInstance(type))
                .OrderBy(converter => converter.Priority)
                .Append(new FloatConverter())
                .Append(new DecimalConverter())
                .Append(new DoubleConverter())
                .Append(new ByteConverter())
                .Append(new SByteConverter())
                .Append(new IntegerConverter())
                .Append(new BooleanConverter())
                .Append(new CharConverter())
                .Append(new StringConverter())
                .ToList();
        }

        public static bool TryConvert(Type source, string value, out object result)
        {
            result = null;
            var converter = Converters.FirstOrDefault(c => source == c.TargetType && c.CanConvert(value));
        
            if (converter is null) return false;

            result = converter.Convert(value);
            return true;
        }
    }
}

