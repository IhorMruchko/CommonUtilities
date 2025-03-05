using System;
using System.Reflection;

namespace CommonUtilities.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public abstract class ParameterAttribute : Attribute
    {
        public virtual bool Validate(ParameterInfo parameter,  string value) => true;
        
        public virtual object Convert(object value) => value;
    }

    public class OptionalAttribute : ParameterAttribute
    {
        public string Name { get; }
        
        public char? Symbol { get; }

        public OptionalAttribute() { }
        
        public OptionalAttribute(string name = null)
        {
            Name = name;    
        }
        
        public OptionalAttribute(char name = char.MinValue)
        {
            Symbol = name;    
        }
    }
}