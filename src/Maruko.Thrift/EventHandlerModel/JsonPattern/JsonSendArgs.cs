using Thrift.Protocol;

namespace Maruko.Thrift.EventHandlerModel.JsonPattern
{
    public class JsonSendArgsL : TBase
    {
        public Isset _Isset;

        private string _jsonFormat;

        public string JsonFormat
        {
            get { return _jsonFormat; }
            set
            {
                _Isset.IsArgs = true;
                _jsonFormat = value;
            }
        }

        public void Write(TProtocol writeProtocol)
        {
            writeProtocol.IncrementRecursionDepth();
            try
            {
                var struc = new TStruct("SendArgs");
                writeProtocol.WriteStructBegin(struc);
                var field = new TField();
                if (JsonFormat != null && _Isset.IsArgs)
                {
                    field.Name = "jsonFormat";
                    field.Type = TType.String;
                    field.ID = 1;
                    writeProtocol.WriteFieldBegin(field);
                    writeProtocol.WriteString(JsonFormat);
                    writeProtocol.WriteFieldEnd();
                }
                writeProtocol.WriteFieldStop();
                writeProtocol.WriteStructEnd();
            }
            finally
            {
                writeProtocol.DecrementRecursionDepth();
            }
        }

        public void Read(TProtocol readProtocol)
        {
            readProtocol.IncrementRecursionDepth();
            try
            {
                TField field;
                readProtocol.ReadStructBegin();
                while (true)
                {
                    field = readProtocol.ReadFieldBegin();
                    if (field.Type == TType.Stop)
                    {
                        break;
                    }
                    switch (field.ID)
                    {
                        case 1:
                            if (field.Type == TType.String)
                            {
                                JsonFormat = readProtocol.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(readProtocol, field.Type);
                            }
                            break;
                        default:
                            TProtocolUtil.Skip(readProtocol, field.Type);
                            break;
                    }
                    readProtocol.ReadFieldEnd();
                }
                readProtocol.ReadStructEnd();
            }
            finally
            {
                readProtocol.DecrementRecursionDepth();
            }
        }

        public struct Isset
        {
            public bool IsArgs;
        }
    }
}