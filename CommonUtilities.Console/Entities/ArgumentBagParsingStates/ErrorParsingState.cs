namespace CommonUtilities.Console.Entities.ArgumentBagParsingStates
{
    internal class ErrorParsingState : CompletedParsingState
    {
        public string ErrorMessage { get; private set; }

        internal ErrorParsingState(string message)
        {
            ErrorMessage = message;
        }
    }
}