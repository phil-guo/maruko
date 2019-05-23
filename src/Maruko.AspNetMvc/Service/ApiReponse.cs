using Maruko.Application;

namespace Maruko.AspNetMvc.Service
{
    public class ApiReponse<T>
    {
        public ApiReponse(T datas, string msg = "", ServiceEnum status = ServiceEnum.Success)
        {
            Result = datas;
            Status = status;
            Msg = msg;
        }

        public ApiReponse(string msg, ServiceEnum status = ServiceEnum.Success)
        {
            Msg = msg;
            Status = status;
        }

        public ApiReponse()
        {
        }

        public T Result { get; set; }

        public string Msg { get; set; }

        public ServiceEnum Status { get; set; }
    }
}