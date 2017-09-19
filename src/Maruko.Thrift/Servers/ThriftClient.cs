using System;
using Maruko.Thrift.EventHandlerModel.JsonPattern;
using Thrift;
using Thrift.Protocol;

namespace Maruko.Thrift.Servers
{
    /// <summary>
    ///     客户端处理基类
    /// </summary>
    public class ThriftClient : IDisposable
    {
        public static ThriftClient InstanceObject = new ThriftClient();

        protected TProtocol Iport;
        protected TProtocol Oprot;
        protected int Seqid;

        private ThriftClient()
        {
        }

        public TProtocol InputProtocol => Iport;
        public TProtocol OutputProtocol => Oprot;

        public void Dispose()
        {
            if (Iport != null)
                ((IDisposable)Iport).Dispose();
            if (Oprot != null)
                ((IDisposable)Oprot).Dispose();
        }

        public ThriftClient Instance(TProtocol prot)
        {
            Iport = prot;
            Oprot = prot;
            return InstanceObject;
        }

        #region Json the way

        /// <summary>
        ///     json 传入入口
        /// </summary>
        /// <param name="jsonFormat">json字符串</param>
        public string JsonSend(string jsonFormat)
        {
            SendCombinationAndWrite(jsonFormat);
            return ReceiveMessage();
        }

        private void SendCombinationAndWrite(string jsonFormat)
        {
            Oprot.WriteMessageBegin(new TMessage("JsonSend", TMessageType.Call, Seqid));

            //参数组合
            var args = new JsonSendArgsL
            {
                JsonFormat = jsonFormat
            };
            //写入
            args.Write(Oprot);
            //写入消息end
            Oprot.WriteMessageEnd();
            Oprot.Transport.Flush();
        }

        private string ReceiveMessage()
        {
            var message = Iport.ReadMessageBegin();

            if (message.Type == TMessageType.Exception)
            {
                var x = TApplicationException.Read(Iport);
                Iport.ReadMessageEnd();
                throw x;
            }

            var result = new JsonResultArgs();
            result.Read(Iport);
            Iport.ReadMessageEnd();

            if (result._Isset.Success)
                return result.Ok;

            throw new TApplicationException(TApplicationException.ExceptionType.MissingResult,
                "send failed: unknown result");
        }

        #endregion
    }
}