using System;

namespace CommonUtilities.FluentApi
{
    public partial class Pipe<TTarget>
    {
        /// <summary>
        /// Performs specific <paramref name="then"/> when pipeline state
        /// satisfies <paramref name="condition"/>, <paramref name="else"/> otherwise.
        /// </summary>
        /// <param name="condition">Case when we should execute <paramref name="then"/>. </param>
        /// <param name="then">Action to perform.</param>
        /// <param name="else">Action to perform on <paramref name="condition"/> false.</param>
        /// <returns>Pipe in the same state</returns>
        public Pipe<TTarget> When(
            Func<TTarget, bool> condition, 
            Action<TTarget> then, 
            Action<TTarget> @else = null)
        {
            if (_target == null) return new Pipe<TTarget>(default);

            try
            {
                if (condition(_target))
                    then(_target);
                else
                    @else?.Invoke(_target);

                return this;
            }
            catch
            {
                return this;
            }
        }
        
        public Pipe<TResult> When<TResult>(
            Func<TTarget, bool> condition, 
            Func<TTarget, TResult> then, 
            Func<TTarget, TResult> @else)
        {
            try
            {
                return condition(_target)
                    ? new Pipe<TResult>(then(_target))
                    : new Pipe<TResult>(@else(_target));
            }
            catch
            {
                return new Pipe<TResult>(default);
            }
        }
        
        public Pipe<TResult> When<TResult>(
            Func<TTarget, bool> condition, 
            TResult then, 
            Func<TTarget, TResult> @else)
        {
            try
            {
                return condition(_target)
                    ? new Pipe<TResult>(then)
                    : new Pipe<TResult>(@else(_target));
            }
            catch
            {
                return new Pipe<TResult>(default);
            }
        }
        
        public Pipe<TResult> When<TResult>(
            Func<TTarget, bool> condition, 
            Func<TTarget, TResult> then, 
            TResult @else)
        {
            try
            {
                return condition(_target)
                    ? new Pipe<TResult>(then(_target))
                    : new Pipe<TResult>(@else);
            }
            catch
            {
                return new Pipe<TResult>(default);
            }
        }
        
        public Pipe<TResult> When<TResult>(
            Func<TTarget, bool> condition, 
            TResult then, 
            TResult @else)
        {
            try
            {
                return condition(_target)
                    ? new Pipe<TResult>(then)
                    : new Pipe<TResult>(@else);
            }
            catch
            {
                return new Pipe<TResult>(default);
            }
        }
        
        public Pipe<TResult> When<TResult>(
            bool condition, 
            Func<TTarget, TResult> then, 
            Func<TTarget, TResult> @else)
        {
            try
            {
                return condition
                    ? new Pipe<TResult>(then(_target))
                    : new Pipe<TResult>(@else(_target));
            }
            catch
            {
                return new Pipe<TResult>(default);
            }
        }
        
        public Pipe<TResult> When<TResult>(
            bool condition, 
            TResult then, 
            Func<TTarget, TResult> @else)
        {
            try
            {
                return condition
                    ? new Pipe<TResult>(then)
                    : new Pipe<TResult>(@else(_target));
            }
            catch
            {
                return new Pipe<TResult>(default);
            }
        }
        
        public Pipe<TResult> When<TResult>(
            bool condition, 
            Func<TTarget, TResult> then, 
            TResult @else)
        {
            try
            {
                return condition
                    ? new Pipe<TResult>(then(_target))
                    : new Pipe<TResult>(@else);
            }
            catch
            {
                return new Pipe<TResult>(default);
            }
        }
        
        public Pipe<TResult> When<TResult>(
            bool condition, 
            TResult then, 
            TResult @else)
        {
            try
            {
                return condition
                    ? new Pipe<TResult>(then)
                    : new Pipe<TResult>(@else);
            }
            catch
            {
                return new Pipe<TResult>(default);
            }
        }
        
        /// <summary>
        /// Performs specific <paramref name="action"/> when pipeline state
        /// when <paramref name="condition"/> is true
        /// </summary>
        /// <param name="condition">Case when we should execute <paramref name="action"/>. </param>
        /// <param name="action">Action to perform</param>
        /// <param name="else">Action to perform otherwise.</param>
        /// <returns>Pipe in the same state</returns>
        public Pipe<TTarget> When(
            bool condition, 
            Action<TTarget> action,
            Action<TTarget> @else = null)
        {
            if (_target == null) return new Pipe<TTarget>(default);

            try
            {
                if (condition)
                    action(_target);
                else
                    @else?.Invoke(_target);
                
                return this;
            }
            catch
            {
                return this;
            }
        }
    }
}