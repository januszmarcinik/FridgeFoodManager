﻿namespace FridgeFoodManager.Api
{
    public class Result
    {
        private Result(bool isSuccess, string errorMessage)
        {
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public bool IsSuccess { get; }

        public bool IsFailure => !IsSuccess;

        public string ErrorMessage { get; }

        public static Result Success()
            => new Result(true, string.Empty);

        public static Result Failure(string errorMessage)
            => new Result(false, errorMessage);
    }
}
