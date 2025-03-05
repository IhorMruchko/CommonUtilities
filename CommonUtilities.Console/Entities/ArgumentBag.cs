using System;
using System.Collections.Generic;
using CommonUtilities.Console.Entities.ArgumentBagParsingStates;

namespace CommonUtilities.Console.Entities
{
    public class ArgumentBag
    {
        private readonly Dictionary<string, string> _optionalParameters = new Dictionary<string, string>();
        
        private readonly List<string> _parameters = new List<string>();
        
        internal ParsingState ParsingState { get; private set; }
        
        public ArgumentBag(params string[] arguments)
        {
            ChangeState<InitialParsingState>();

            for (var currentIndex = 0; currentIndex < arguments.Length;)
            {
                currentIndex = ParsingState.Parse(arguments, currentIndex);
            }
        }
        
        internal void ChangeState(ParsingState state)
        {
            state.Context = this;
            ParsingState = state;
        }

        internal void ChangeState<TState>() where TState : ParsingState
            => ChangeState(Activator.CreateInstance<TState>());
        
        internal void AddParameter(string name, string value) 
            => _optionalParameters.Add(name, value);
        
        internal void AddParameter(string value) 
            => _parameters.Add(value);

        public (bool hasValue, string value) this[int index] =>
            index < _parameters.Count ? (true, _parameters[index]) : (false, null);
        
        public (bool hasValue, string value) this[string parameter] 
            => _optionalParameters.ContainsKey(parameter) 
                ? (true, _optionalParameters[parameter]) 
                : (false, null);
        
        
        public IEnumerable<string> Optionals => _optionalParameters.Keys;
        
        public int Length => _parameters.Count;
        
    }
}