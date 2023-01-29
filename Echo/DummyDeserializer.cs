using Kosher.Log;
using Kosher.Sockets.Interface;

namespace Echo
{
    public class DummyDeserializer : IPacketDeserializer
    {
        private readonly EchoHandler _protocolHandler;
        public DummyDeserializer(EchoHandler protocolHandler)
        {
            _protocolHandler = protocolHandler;
        }

        public void Deserialize(BinaryReader stream)
        {
            var bodyBytes = stream.ReadBytes((int)stream.BaseStream.Length);
            _protocolHandler.Process(bodyBytes);
        }

        public bool IsTakedCompletePacket(BinaryReader stream)
        {
            if (_protocolHandler.GetSession().IsDispose() == true)
            {
                LogHelper.Debug($"{_protocolHandler.GetSession().Id} is closed");
            }
            if (stream.BaseStream.Length <= 0)
            {
                return false;
            }
            LogHelper.Debug($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] id : {_protocolHandler.GetSession().Id} buffer size : {stream.BaseStream.Length} {Thread.CurrentThread.ManagedThreadId}");
            return true;
        }
    }
}
