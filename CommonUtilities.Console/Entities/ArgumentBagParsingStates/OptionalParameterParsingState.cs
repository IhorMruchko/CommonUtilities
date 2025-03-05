using System.Linq;

namespace CommonUtilities.Console.Entities.ArgumentBagParsingStates
{
    internal class OptionalParameterParsingState : ParsingState
    {
        internal override int Parse(string[] arguments, int currentArgumentIndex)
        {
            var currentArgument = arguments[currentArgumentIndex].Trim();
            var optionalSignsAmount = currentArgument.TakeWhile(c => c.Equals('-')).Count();

            if (optionalSignsAmount > 2)
            {
                Context.ChangeState(new ErrorParsingState("To many dash signs"));
                return currentArgumentIndex;
            }

            if (currentArgument.Length == optionalSignsAmount)
            {
                Context.ChangeState(new ErrorParsingState("No name provided"));
                return currentArgumentIndex;
            }

            if (char.IsDigit(currentArgument[optionalSignsAmount]))
            {
                Context.ChangeState<NegativeNumberParsingState>();
                return currentArgumentIndex;
            }

            if (currentArgument.Contains("="))
            {
                Context.ChangeState<OptionalParameterValueParsingState>();
                return currentArgumentIndex;
            }
            
            if (optionalSignsAmount == 1 && currentArgument.Length != 2)
            {
                Context.ChangeState(new ErrorParsingState("Single dash should be followed by one sign name"));
                return currentArgumentIndex;
            }
            
            if (optionalSignsAmount == 2 && currentArgument.Length == 3)
            {
                Context.ChangeState(new ErrorParsingState("Double dash should not be followed by one sign name"));
                return currentArgumentIndex;
            }
            
            Context.AddParameter(currentArgument.Trim('-'), "true");
            Context.ChangeState<InitialParsingState>();
            return ++currentArgumentIndex;
        }
    }
}