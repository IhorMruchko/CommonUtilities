using System;

namespace CommonUtilities.Console.Attributes
{
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Method | AttributeTargets.Parameter)]
    public class HelpAttribute : Attribute
    {
        public string HelpText { get; private set; }

        public HelpAttribute(string helpText)
        {
            HelpText = helpText;
        }
    }
}