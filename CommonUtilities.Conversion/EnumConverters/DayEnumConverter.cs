using System;
using CommonUtilities.Conversion.Base;

namespace CommonUtilities.Conversion.EnumConverters
{
    /// <inheritdoc />
    public class DayEnumConverter : EnumConverter<DayOfWeek>
    {
        /// <inheritdoc />
        public override uint Priority => 0;
    }
}