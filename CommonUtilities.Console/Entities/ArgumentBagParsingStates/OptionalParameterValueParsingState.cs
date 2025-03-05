using System;
using System.Linq;

namespace CommonUtilities.Console.Entities.ArgumentBagParsingStates
{
    internal class OptionalParameterValueParsingState : ParsingState
    {
        internal override int Parse(string[] arguments, int currentArgumentIndex)
        {
            var currentArgument = arguments[currentArgumentIndex];

            var splitArguments = currentArgument.Split(new[] { '=' }, StringSplitOptions.RemoveEmptyEntries);

            var nameValue = splitArguments[0].Trim();
            var optionalSignsAmount = splitArguments[0].TakeWhile(c => c == '-').Count();
            
            // TODO: think about moving this code to the base class
            if (optionalSignsAmount == 1 && nameValue.Length != 2)
            {
                Context.ChangeState(new ErrorParsingState("Single dash should be followed by one sign name"));
                return currentArgumentIndex;
            }
            
            if (optionalSignsAmount == 2 && nameValue.Length == 3)
            {
                Context.ChangeState(new ErrorParsingState("Double dash should not be followed by one sign name"));
                return currentArgumentIndex;
            }
            // END
            if (splitArguments.Length == 1 && currentArgumentIndex + 1 < arguments.Length
                                     && arguments[currentArgumentIndex + 1].TakeWhile(c => c == '-').Count() <= 1)
            {
                Context.AddParameter(splitArguments[0].Trim(' ', '=', '-'), arguments[currentArgumentIndex + 1]);
                Context.ChangeState<InitialParsingState>();
                return currentArgumentIndex + 2;
            }

            if (splitArguments.Length > 2)
            {
                Context.AddParameter(
                    nameValue.Trim(' ', '=', '-'), 
                    currentArgument.Substring(currentArgument.IndexOf('=') + 1)
                );
                Context.ChangeState<InitialParsingState>();
                return currentArgumentIndex + 1;
            }
            
            Context.AddParameter(
                nameValue.Trim(' ', '=', '-'), 
                splitArguments[1].Trim(' ', '=', '-')
            );
            Context.ChangeState<InitialParsingState>();
            return currentArgumentIndex + 1;
        }
    }
}