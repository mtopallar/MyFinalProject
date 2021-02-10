using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Text;

namespace Core.Utilities.Results
{
    public class Result : IResult
    {
        public Result(bool success, string message):this(success) // 2 parametre gelirse tek parametreli success i de çalıştır. this bu sınıf demek. metoddan 2 parametre geldiğinde bu sınıfın tek parametreli ctor unu da tetiklemiş olduk.
        {
            Message = message;
            // Get read only'dir. Ancak Ctor ile getter ı set edebilirsin. 
        }

        public Result(bool success)
        {
            Success = success;
        }

        public bool Success { get; }
        public string Message { get; }
    }
    
}
