using System.Reflection;
using CommonUtilities.Console.Attributes;

namespace CommonUtilities.Console.Entities
{
    internal class Overload
    {
        public MethodInfo OverloadReference { get; set; }
        
        public Parameter[] Parameters { get; set; }
        
        public HelpAttribute Help { get; set; }
    }
}