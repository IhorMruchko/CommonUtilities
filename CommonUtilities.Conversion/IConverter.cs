using System;

namespace CommonUtilities.Conversion
{
    public interface IConverter
    {
        uint Priority { get; }
        
        Type TargetType { get; }
    
        bool CanConvert(string value);
    
        object Convert(string value);
    }   
}