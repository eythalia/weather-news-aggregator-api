using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WeatherAggregator.Shared.Results
{
    public class Result<T>
    {
        public T Data { get; private set; }
        public string ErrorMessage { get; private set; }
        public bool IsSuccess { get; private set; }
        public int StatusCode { get; private set; }

        private Result(T data, string errorMessage, bool isSuccess, int statusCode)
        {
            Data = data;
            ErrorMessage = errorMessage;
            IsSuccess = isSuccess;
            StatusCode = statusCode;
        }

        public static Result<T> Success(T data, int statusCode)
        {
            return new Result<T>(data, null, true, statusCode);
        }

        public static Result<T> Failure(string errorMessage, int statusCode)
        {
            return new Result<T>(default, errorMessage, false, statusCode);
        }

    }
}
