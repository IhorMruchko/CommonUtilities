using System.Linq;

namespace CommonUtilities.Console.Entities.ArgumentBagParsingStates
{
    internal class NegativeNumberParsingState : ParsingState
    {
        internal override int Parse(string[] arguments, int currentArgumentIndex)
        {
            var currentArgument = arguments[currentArgumentIndex].Trim();

            if (currentArgument.TakeWhile(c => c.Equals('-')).Count() > 1)
            {
                Context.ChangeState(new ErrorParsingState("Number can not has more than one minus sign"));
                return currentArgumentIndex;
            } 
            
            Context.AddParameter(currentArgument);
            Context.ChangeState<InitialParsingState>();
            return ++currentArgumentIndex;
        }
    }
}