using System;

namespace CommonUtilities.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class CommandAttribute : Attribute
    {
        public string Name { get; private set; }
        
        public CommandAttribute(string name) => Name = name;
    }
}