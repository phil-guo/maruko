using System;
using System.Collections.Generic;
using System.IO;
using Maruko.Dependency;
using Maruko.Thrift.EventHandlerModel.JsonPattern;
using Thrift;
using Thrift.Protocol;
using Thrift.Transport;

namespace Maruko.Thrift.Servers
{
    public class ThriftServiceProcessor: TProcessor, IThriftService
    {
        private readonly IServiceHandler _accept;
        protected Dictionary<string, ProcessFunction> ProcessMap = new Dictionary<string, ProcessFunction>();
        protected delegate void ProcessFunction(int seqid, TProtocol iprot, TProtocol oprot);

        public ThriftServiceProcessor(IServiceHandler accept)
        {
            _accept = accept;
            ProcessMap["JsonSend"] = ReceiveProcess;
        }


        //数据传输转换
        public bool Process(TProtocol iprot, TProtocol oprot)
        {
            try
            {
                var msg = iprot.ReadMessageBegin();
                ProcessFunction fn;
                ProcessMap.TryGetValue(msg.Name, out fn);
                if (fn == null)
                {
                    TProtocolUtil.Skip(iprot, TType.Struct);
                    iprot.ReadMessageEnd();
                    var x = new TApplicationException(TApplicationException.ExceptionType.UnknownMethod,
                        "Invalid method name: '" + msg.Name + "'");
                    oprot.WriteMessageBegin(new TMessage(msg.Name, TMessageType.Exception, msg.SeqID));
                    x.Write(oprot);
                    oprot.WriteMessageEnd();
                    oprot.Transport.Flush();
                    return true;
                }
                fn(msg.SeqID, iprot, oprot);
            }
            catch (IOException)
            {
                return false;
            }
            return true;
        }

        //服务端json处理方法
        public void ReceiveProcess(int seqid, TProtocol iprot, TProtocol oprot)
        {
            var args = new JsonSendArgsL();
            args.Read(iprot);
            iprot.ReadMessageEnd();
            var result = new JsonResultArgs();
            try
            {
                result.Ok = _accept.JsonAcceptHandler(args.JsonFormat);
                oprot.WriteMessageBegin(new TMessage("JsonSend", TMessageType.Reply, seqid));
                result.Write(oprot);
            }
            catch (TTransportException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Error occurred in processor:");
                Console.Error.WriteLine(ex.ToString());
                var x = new TApplicationException(TApplicationException.ExceptionType.InternalError, " Internal error.");
                oprot.WriteMessageBegin(new TMessage("JsonSend", TMessageType.Exception, seqid));
                x.Write(oprot);
            }
            oprot.WriteMessageEnd();
            oprot.Transport.Flush();
        }
    }

    public interface IThriftService:IDependencyTransient
    {
    }
}