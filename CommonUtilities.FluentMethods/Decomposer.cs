using System;

namespace CommonUtilities.FluentMethods
{
    public class Decomposer<TTarget>
    {
        private readonly TTarget _target;

        internal Decomposer(TTarget target) => _target = target;

        public Decomposer<TTarget> Get<TResult>(
            Func<TTarget, TResult> selector, 
            out TResult result
        ) 
        {
            try
            {
                result = selector(_target);
            }
            catch
            {
                result = default;
            }

            return this;
        }

        public Decomposer<TResult> Transform<TResult>(Func<TTarget, TResult> converter)
        {
            try
            {
                return new Decomposer<TResult>(converter(_target));
            }
            catch
            {
                return new Decomposer<TResult>(default);
            }
        }
    }
}