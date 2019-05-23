using System;
using Maruko.Application;
using Maruko.AspNetMvc.Service;

namespace Maruko.AspNetMvc
{
    public class PaymentException : Exception
    {
        public PaymentException(string msg, ServiceEnum state = ServiceEnum.Failure)
        {
            Msg = msg;
            Status = state;
        }

        public ServiceEnum Status { get; set; }
        public string Msg { get; set; }
    }
}