using Thrift.Protocol;

namespace Maruko.Thrift.EventHandlerModel.JsonPattern
{
    public class JsonResultArgs : TBase
    {
        public Isset _Isset;
        private string ok;

        public string Ok
        {
            get { return ok; }

            set
            {
                _Isset.Success = true;
                ok = value;
            }
        }

        public void Write(TProtocol write)
        {
            write.IncrementRecursionDepth();
            try
            {
                var struc = new TStruct("ResultArgs");
                write.WriteStructBegin(struc);

                if (_Isset.Success && !string.IsNullOrEmpty(Ok))
                {
                    var field = new TField
                    {
                        Name = "Success",
                        Type = TType.String,
                        ID = 0
                    };

                    write.WriteFieldBegin(field);
                    write.WriteString(Ok);
                    write.WriteFieldEnd();
                }

                write.WriteFieldStop();
                write.WriteStructEnd();
            }
            finally
            {
                write.DecrementRecursionDepth();
            }
        }

        public void Read(TProtocol read)
        {
            read.IncrementRecursionDepth();
            try
            {
                read.ReadStructBegin();
                while (true)
                {
                    var field = read.ReadFieldBegin();
                    if (field.Type == TType.Stop)
                    {
                        break;
                    }
                    switch (field.ID)
                    {
                        case 0:
                            if (field.Type == TType.String)
                            {
                                Ok = read.ReadString();
                            }
                            else
                            {
                                TProtocolUtil.Skip(read, field.Type);
                            }
                            break;
                        default:
                            TProtocolUtil.Skip(read, field.Type);
                            break;
                    }
                    read.ReadFieldEnd();
                }
                read.ReadStructEnd();
            }
            finally
            {
                read.DecrementRecursionDepth();
            }
        }

        public struct Isset
        {
            public bool Success;
        }
    }
}