using System;

namespace CommonUtilities.FluentMethods
{
    public class Result<TBody, TError>
    {
        private readonly TBody _body;
        
        private readonly TError _error;
        
        public bool IsSuccess { get; }

        public TBody Body =>
            IsSuccess
                ? _body
                : throw new InvalidOperationException("Can not access body while result is not successful");
        
        public TError Error =>
            !IsSuccess
                ? _error
                : throw new InvalidOperationException("Can not access error while result is successful");

        private Result(TBody body, TError error, bool success)
            => (_body, _error, IsSuccess) = (body, error, success);
        
        public static Result<TBody, TError> Success(TBody body)
            => new Result<TBody, TError>(body, default, true);

        public static Result<TBody, TError> Failed(TError error)
            => new Result<TBody, TError>(default, error, false);
    }
}