using System;
using CommonUtilities.Console.Attributes;

namespace CommonUtilities.Console.Entities
{
    internal class Command
    {
        public Type ReferenceType { get; set; }
        
        public CommandAttribute Attribute { get; set; }
        
        public Command[] SubCommands { get; set; }
        
        public Overload[] Overloads { get; set; }
    }
}