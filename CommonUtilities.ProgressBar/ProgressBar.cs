using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace CommonUtilities.ProgressBar
{
    public struct ProgressInfoArgs
    {
        public int TotalProgress { get; internal set; }
        
        public int CurrentStage { get; internal set; }

        
        public float Percentage => CurrentStage / (float)TotalProgress * 100f;
    }
    
    public delegate void ProgressBarUpdated(ProgressInfoArgs infoArgs);
    
    public class ProgressBar : IEnumerable
    {
        private readonly IEnumerable _elements;
        
        public event ProgressBarUpdated OnExit;
        public event ProgressBarUpdated OnEnter;
        
        public ProgressBar(IEnumerable source)
        {
            _elements = source;
        }
        
        public IEnumerator GetEnumerator()
        {
            // TODO: Find better way to count total
            var totalElements = _elements.Cast<object>().Count();
            var current = 0;
            
            foreach (var element in _elements)
            {
                Console.WriteLine(Console.CursorTop);
                var currentProgressEvent = new ProgressInfoArgs
                {
                    CurrentStage = ++current, 
                    TotalProgress = totalElements 
                };
                OnEnter?.Invoke(currentProgressEvent); 
                yield return element; 
                OnExit?.Invoke(currentProgressEvent);
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
        
    }
}