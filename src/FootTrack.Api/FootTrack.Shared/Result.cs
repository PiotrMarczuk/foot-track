using System;
using NullGuard;

namespace FootTrack.Shared
{
    public class Result
    {
        public bool IsSuccess { get; }

        public Error Error { get; }

        public bool IsFailure => !IsSuccess;

        protected Result(bool isSuccess, [AllowNull] Error error)
        {
            if (isSuccess && error != null)
            {
                throw new InvalidOperationException();
            }

            if (!isSuccess && error == null)
            {
                throw new InvalidOperationException();
            }

            IsSuccess = isSuccess;
            Error = error;
        }

        public static Result<T> Fail<T>(Error error)
        {
            return new Result<T>(default, false, error);
        }

        public static Result Ok()
        {
            return new Result(true, default);
        }

        public static Result<T> Ok<T>(T value)
        {
            return new Result<T>(value, true, default);
        }

        public static Result Combine(params Result[] results)
        {
            foreach (Result result in results)
            {
                if (result.IsFailure)
                {
                    return result;
                }
            }

            return Ok();
        }
    }


    public class Result<T> : Result
    {
        private readonly T _value;

        public T Value
        {
            get
            {
                if (!IsSuccess)
                {
                    throw new InvalidOperationException();
                }

                return _value;
            }
        }

        protected internal Result([AllowNull] T value, bool isSuccess, [AllowNull] Error error)
            : base(isSuccess, error)
        {
            _value = value;
        }
    }
}