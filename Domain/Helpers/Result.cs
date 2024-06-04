using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibraryProject.Domain.Helpers
{
    public class Result<T>
    {
        public T Data { get; }
        public bool IsSuccess { get; }
        public string ErrorMessage { get; }

        private Result(T data, bool isSuccess, string errorMessage)
        {
            Data = data;
            IsSuccess = isSuccess;
            ErrorMessage = errorMessage;
        }

        public static Result<T> Success(T data) => new Result<T>(data, true, null);

        public static Result<T> Failure(string errorMessage) => new Result<T>(default(T), false, errorMessage);
    }
}
