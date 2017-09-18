using System.Threading.Tasks;
using Maruko.Thrift.EventHandlerModel;

namespace Maruko.Thrift.Servers.Extension
{
    public static class LinkClientExtension
    {
        public static async Task<string> LinkServer(LinkServerModel serverModel)
        {
            return await TransportClient.Instance.ClientStart(serverModel.JsonFormate, serverModel.ServerName,
                serverModel.ZKIpAddress);
        }
    }
}
