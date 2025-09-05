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
            Action<TTarget> @else = null
        ) => WhenCore(
            condition,
            target =>
            {
                then(target);
                return this;
            },
            target =>
            {
                @else?.Invoke(target);
                return this;
            }
        ).Return();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="then"></param>
        /// <param name="else"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Pipe<TResult> When<TResult>(
            Func<TTarget, bool> condition,
            Func<TTarget, TResult> then,
            Func<TTarget, TResult> @else
        ) => WhenCore(condition, then, @else);
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="then"></param>
        /// <param name="else"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Pipe<TResult> When<TResult>(
            Func<TTarget, bool> condition, 
            TResult then, 
            Func<TTarget, TResult> @else
        ) => WhenCore(condition, _ => then, @else);
        
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="then"></param>
        /// <param name="else"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Pipe<TResult> When<TResult>(
            Func<TTarget, bool> condition, 
            Func<TTarget, TResult> then, 
            TResult @else
        ) => WhenCore(condition, then, _ => @else);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="then"></param>
        /// <param name="else"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Pipe<TResult> When<TResult>(
            Func<TTarget, bool> condition,
            TResult then,
            TResult @else
        ) => WhenCore(condition, _ => then, _ => @else);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="then"></param>
        /// <param name="else"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Pipe<TResult> When<TResult>(
            bool condition, 
            Func<TTarget, TResult> then, 
            Func<TTarget, TResult> @else
        ) => WhenCore(_ => condition, then, @else);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="then"></param>
        /// <param name="else"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Pipe<TResult> When<TResult>(
            bool condition,
            TResult then,
            Func<TTarget, TResult> @else
        ) => WhenCore(_ => condition, _ => then, @else);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="then"></param>
        /// <param name="else"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Pipe<TResult> When<TResult>(
            bool condition, 
            Func<TTarget, TResult> then, 
            TResult @else
        ) => WhenCore(_ => condition, then, _ => @else);
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="then"></param>
        /// <param name="else"></param>
        /// <typeparam name="TResult"></typeparam>
        /// <returns></returns>
        public Pipe<TResult> When<TResult>(
            bool condition, 
            TResult then, 
            TResult @else
        ) => WhenCore(_ => condition, _ => then, _ => @else);

        /// <summary>
        /// Performs specific <paramref name="action"/> when <paramref name="condition"/> for pipeline state
        /// is true.
        /// </summary>
        /// <param name="condition">Case when we should execute <paramref name="action"/>. </param>
        /// <param name="action">Action to perform</param>
        /// <param name="else">Action to perform otherwise.</param>
        /// <returns>Pipe in the same state</returns>
        public Pipe<TTarget> When(
            bool condition,
            Action<TTarget> action,
            Action<TTarget> @else = null
        ) => WhenCore(_ => condition, _ =>
            {
                action(_target);
                return _target;
            }, _ =>
            {
                @else?.Invoke(_target);
                return _target;
            }
        );
    }
}