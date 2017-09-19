using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Rabbit.Zookeeper;
using Rabbit.Zookeeper.Implementation;
using Thrift.Protocol;
using Thrift.Transport;

namespace Maruko.Thrift.Servers
{
    /// <summary>
    ///     thrift 通讯客户端
    /// </summary>
    public class TransportClient

    {
        public static TransportClient Instance = new TransportClient();


        private TransportClient()
        {
        }

        public async Task<string> ClientStart(string jsonFormate, string serverName, string zkIpAddress, string domain = "", int port = 8080)
        {

            var resultData = await ZkConnect(zkIpAddress, serverName);

            var ipAndPort = resultData.Split(':');

            domain = ipAndPort[0];
            port = Convert.ToInt32(ipAndPort[1]);

            TTransport transport = new TSocket(domain, port);
            TProtocol protocol = new TBinaryProtocol(transport);

            var client = ThriftClient.InstanceObject.Instance(protocol);

            client.InputProtocol.Transport.Open();

            var result=client.JsonSend(jsonFormate);

            transport.Close();

            return result;
        }

        private async Task<string> ZkConnect(string zkIpAddress, string serverName)
        {
            // *1、连接到zk，查询当前zk下提供的服务列表中是否有自己需要的服务名称（queryUserDetailService）
            // *2、如果没有找到需要的服务名称，则客户端终止工作
            //* 3、如果找到了服务，则通过服务给出的ip，port，基于Thrift进行正式请求

            IZookeeperClient clientZk = new ZookeeperClient(new ZookeeperClientOptions()
            {
                ConnectionString = zkIpAddress + ":2181",//zookeeper的默认监听端口是2181
                BasePath = "/", //default value
                ConnectionTimeout = TimeSpan.FromSeconds(10), //default value
                SessionTimeout = TimeSpan.FromSeconds(20), //default value
                OperatingTimeout = TimeSpan.FromSeconds(60), //default value
                ReadOnly = false, //default value
                SessionId = 0, //default value
                SessionPasswd = null //default value
            });

            var servers = await clientZk.GetDataAsync("/" + serverName);//"/" + serverName
            var resultData = Encoding.UTF8.GetString(servers.ToArray());

            return resultData;
        }
    }
}