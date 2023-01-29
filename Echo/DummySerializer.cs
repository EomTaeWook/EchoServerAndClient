using Kosher.Sockets.Interface;

namespace Echo
{
    public class DummySerializer: IPacketSerializer 
    {
        ArraySegment<byte> IPacketSerializer.MakeSendBuffer(IPacket packet)
        {
            return Array.Empty<byte>();
        }
    }
}
