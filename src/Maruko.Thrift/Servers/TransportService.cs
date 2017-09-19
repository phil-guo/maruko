using System;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Rabbit.Zookeeper;
using Rabbit.Zookeeper.Implementation;
using Thrift.Server;
using Thrift.Transport;

namespace Maruko.Thrift.Servers
{
    /// <summary>
    ///     服务开始
    /// </summary>
    public class TransportService
    {
        public static TransportService Instance = new TransportService();

        private TransportService()
        {
        }

        /// <summary>
        ///     开始服务
        /// </summary>
        /// <param name="port">端口</param>
        /// <param name="domian">zookeeper域名</param>
        /// <param name="serverName">zookeeper的主节点名称，格式：genService</param>
        /// <param name="recureName">zookeeper的次级阶段名称格式：/servers/</param>
        /// <param name="ipAndPort">ip地址和端口，用“，”隔开</param>
        /// <param name="serviceHandler">服务实例</param>
        public async Task<TServer> ServerStart(int port, string domian, string serverName, string recureName, string ipAndPort, IServiceHandler serviceHandler)
        {

            var serverTransport = new TServerSocket(port, 0, false); //10代表超时时间
            var processor = new ThriftServiceProcessor(serviceHandler);
            TServer server = new TSimpleServer(processor, serverTransport);

            IZookeeperClient client = new ZookeeperClient(new ZookeeperClientOptions()
            {
                ConnectionString = domian + ":2181",
                BasePath = "/", //default value
                ConnectionTimeout = TimeSpan.FromSeconds(10), //default value
                SessionTimeout = TimeSpan.FromSeconds(20), //default value
                OperatingTimeout = TimeSpan.FromSeconds(60), //default value
                ReadOnly = false, //default value
                SessionId = 0, //default value
                SessionPasswd = null //default value
            });

            //创建一个父级节点Service
            var data = Encoding.UTF8.GetBytes(ipAndPort);

            if (!await client.ExistsAsync("/" + serverName))
            {
                //创建永久性的节点
                await client.CreatePersistentAsync("/Service", data);

                //创建临时节点
                //await client.CreatePersistentAsync("/" + serverName, data, ZooDefs.Ids.OPEN_ACL_UNSAFE);
            }

            var type = typeof(IServiceHandler);

            var methods = type.GetMethods();

            foreach (MethodInfo methodInfo in methods)
            {
                var str = string.Empty;

                if (!string.IsNullOrEmpty(recureName))
                {
                    str = serverName + recureName;
                }

                //递归格式：/genService/servers/
                await client.CreateRecursiveAsync("/" + str + methodInfo.Name, Encoding.UTF8.GetBytes(ipAndPort));
            }

            return server;
        }
    }
}