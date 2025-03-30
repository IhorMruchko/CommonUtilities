namespace CommonUtilities.FluentMethods
{
    public class Pipeline<TTarget>
    {
        private readonly Optional<TTarget> _target;
        
        internal Pipeline(Optional<TTarget> target) => _target = target;

        internal Pipeline(TTarget target) => _target = target.ToOptional();
    }
}