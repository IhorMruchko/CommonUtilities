using System.Reflection;
using CommonUtilities.Console.Attributes;

namespace CommonUtilities.Console.Entities
{
    internal class Parameter
    {
        public ParameterInfo ParameterReference { get; set; }
        
        public ParameterAttribute[] Attributes { get; set; }
        
        public HelpAttribute Help { get; set; }
    }
}