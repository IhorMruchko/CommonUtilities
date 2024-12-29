using System.Reflection;

namespace CommonUtilities.Console.Entities
{
    internal class Overload
    {
        public MethodInfo OverloadReference { get; set; }
        
        public Parameter[] Parameters { get; set; }
    }
}