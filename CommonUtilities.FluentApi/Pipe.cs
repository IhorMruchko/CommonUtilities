using System;
using System.Collections.Generic;

// TODO: Add switch cases
namespace CommonUtilities.FluentApi
{
    /// <summary>
    /// Provides access to the chain calls to transform or get the information.
    /// </summary>
    /// <typeparam name="TTarget">Type of the initial value.</typeparam>
    public partial class Pipe<TTarget>
    {
        private readonly TTarget _target;
        
        internal Pipe(TTarget target) => _target = target;

        /// <summary>
        /// Saves current value in pipeline to <paramref name="value"/>.
        /// </summary>
        /// <param name="value">Current pipeline value.</param>
        /// <returns>Current pipeline.</returns>
        public Pipe<TTarget> Save(out TTarget value)
        {
            value = _target;
            return this;
        }
        
        /// <summary>
        /// Converts pipeline from object to another one. 
        /// </summary>
        /// <param name="mapper">Contains conversion rules to apply to current object.</param>
        /// <typeparam name="TResult">Resulting type returned from <paramref name="mapper"/>.</typeparam>
        /// <returns>New pipeline with initial object of <typeparamref name="TResult"/>.</returns>
        public Pipe<TResult> Map<TResult>(Func<TTarget, TResult> mapper)
        {
            if (_target == null) return new Pipe<TResult>(default);

            try
            {
                return new Pipe<TResult>(mapper(_target));
            }
            catch
            {
                return new Pipe<TResult>(default);
            }
        }
        
        /// <summary>
        /// Converts pipeline from object to another one
        /// and saves evaluated value to <paramref name="result"/>.
        /// </summary>
        /// <param name="mapper">Contains conversion rules to apply to current object.</param>
        /// <param name="result">Result of the <paramref name="mapper"/> evaluation.</param>
        /// <typeparam name="TResult">Resulting type returned from <paramref name="mapper"/>.</typeparam>
        /// <returns>New pipeline with initial object of <typeparamref name="TResult"/>.</returns>
        public Pipe<TResult> Map<TResult>(Func<TTarget, TResult> mapper, out TResult result)
        {
            result = default;
            if (_target == null) return new Pipe<TResult>(result);

            try
            {
                result = mapper(_target);
                return new Pipe<TResult>(result);
            }
            catch
            {
                return new Pipe<TResult>(default);
            }
        }
        
        /// <summary>
        /// Evaluates <paramref name="getter"/> and
        /// store that into <paramref name="result"/>
        /// without modifying existing pipeline.
        /// </summary>
        /// <param name="getter">Produces any value of the <typeparamref name="TResult"/> type.</param>
        /// <param name="result">Contains the computed <paramref name="getter"/> value.</param>
        /// <typeparam name="TResult">Expected result from the <paramref name="getter"/>.</typeparam>
        /// <returns>The same Pipe as was before the call.</returns>
        public Pipe<TTarget> Tap<TResult>(Func<TResult> getter, out TResult result)
        {
            result = default;
            try
            {
                result = getter();
                return this;
            }
            catch
            {
                return this;
            }
        }

        /// <summary>
        /// Performs some action basing on current state of the pipe without modification.
        /// </summary>
        /// <param name="action">Action to perform.</param>
        /// <returns>The same pipeline.</returns>
        public Pipe<TTarget> Tap(Action<TTarget> action)
        {
            if (_target == null) return new Pipe<TTarget>(default);

            try
            {
                action(_target);
                return this;
            }
            catch
            {
                return this;
            }
        }
        
        /// <summary>
        /// Performs any action without changing pipeline state.
        /// </summary>
        /// <param name="action">Action to perform.</param>
        /// <returns>The same pipeline.</returns>
        public Pipe<TTarget> Tap(Action action)
        {
            try
            {
                action();
                return this;
            }
            catch
            {
                return this;
            }
        }

        /// <summary>
        /// Gets the value of the pipeline.
        /// </summary>
        /// <param name="default">Default value to get if something went wrong during the pipe.</param>
        /// <returns>Value of the pipeline or <paramref name="default"/>.</returns>
        public TTarget Return(TTarget @default = default)
            => EqualityComparer<TTarget>.Default.Equals(_target, default) 
                ? @default
                : _target;
    }
}