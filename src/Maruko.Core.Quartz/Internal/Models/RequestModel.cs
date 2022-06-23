using Maruko.Core.Extensions.Http;

namespace Maruko.Core.Quartz.Internal.Models
{
    public class RequestModel : IMiddleRequest<ModelResponse<object>>
    {
        public string ServiceDomainKey => AppId;
        public string UseRoutName => RequestUrl;
        public ServiceMethodRequest ServiceTypeEnum => RequestType;
        public bool AttachToken => false;

         
        public string AppId { get; set; }
        public string RequestUrl { get; set; }
        public ServiceMethodRequest RequestType { get; set; }
    }
}
