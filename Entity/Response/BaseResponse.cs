using System;

namespace Object.Response
{
    public class BaseResponse<T>
    {
        public T? Data { get; set; }
        public bool IsSuccess { get; set; }
        public DateTime ResponseTime { get; set; }
        public Exception? Exception { get; set; }

        public BaseResponse(T data)
        {
            Data = data;
            IsSuccess = true;
            ResponseTime = DateTime.Now;
        }

        public BaseResponse(Exception exception)
        {
            IsSuccess = false;
            ResponseTime = DateTime.Now;
            Exception = exception;
        }
    }
}
