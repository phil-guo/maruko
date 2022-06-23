using System.Threading.Tasks;

namespace Maruko.Core.Extensions.Http
{
    public interface IMiddleClient
    {
        /// <summary>
        /// 默认单体模型请求
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<T> ExecuteAsync<T>(IMiddleRequest<T> request) where T : MiddleResponse;

    }
}
