using System;
using System.Reflection;

namespace CommonUtilities.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Parameter, AllowMultiple = true)]
    public abstract class ParameterAttribute : Attribute
    {
        public abstract bool Validate(ParameterInfo parameter,  string value);
    }

    public class OptionalAttribute : ParameterAttribute
    {
        protected string Name { get; }
        
        protected char? Symbol { get; }

        public OptionalAttribute() { }
        
        public OptionalAttribute(string name = null)
        {
            Name = name;    
        }
        
        public OptionalAttribute(char name = char.MinValue)
        {
            Symbol = name;    
        }
        
        public override bool Validate(ParameterInfo parameter, string value)
        {
            if (parameter.ParameterType == typeof(string))
            {
                return value.Equals($"--{Name ?? parameter.Name}");
            }

            return parameter.ParameterType == typeof(char) && value.Equals($"-{Symbol?.ToString() ?? parameter.Name}");
        }
    }
}