using System;

namespace CommonUtilities.FluentMethods
{
    public class Optional<TTarget> 
    {
        private TTarget _target;

        public static Optional<TTarget> Create(TTarget target)
            => target == null
                ? None()
                : Some(target);

        public static Optional<TTarget> Some(TTarget target) 
            => new Optional<TTarget> { _target = target };
        
        public static Optional<TTarget> None() => new Optional<TTarget>();

        public Optional<TResult> Map<TResult>(Func<TTarget, TResult> map)
        {
            if (_target == null) return Optional<TResult>.None();

            try
            {
                return Optional<TResult>.Some(map(_target));
            }
            catch
            {
                return Optional<TResult>.None();
            }
        }

        public TTarget Reduce(TTarget @default = default)
            => _target == null ? @default : _target;
    }
}