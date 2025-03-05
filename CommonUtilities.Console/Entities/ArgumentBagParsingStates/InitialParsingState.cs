namespace CommonUtilities.Console.Entities.ArgumentBagParsingStates
{
    internal class InitialParsingState : ParsingState
    {
        internal override int Parse(string[] arguments, int currentArgumentIndex)
        {
            if (currentArgumentIndex >= arguments.Length)
            {
                Context.ChangeState<CompletedParsingState>();
                return currentArgumentIndex;
            }

            if (arguments[currentArgumentIndex].StartsWith("-"))
            {
                Context.ChangeState<OptionalParameterParsingState>();
                return currentArgumentIndex;
            }

            Context.AddParameter(arguments[currentArgumentIndex]);
            Context.ChangeState<InitialParsingState>();
            return ++currentArgumentIndex;
        }
    }
}