using Kosher.Collections;
using Kosher.Sockets.Interface;

namespace EchoClient.Serializer
{
    internal class DummySerializer : IPacketSerializer
    {
        public Vector<byte> MakeSendBuffer(IPacket packet)
        {
            var sendPacket = packet as Packet;

            var sendBuffer = new Vector<byte>();

            sendBuffer.AddRange(BitConverter.GetBytes(sendPacket.GetLength()));

            sendBuffer.AddRange(sendPacket.Body);

            return sendBuffer;
        }

        ArraySegment<byte> IPacketSerializer.MakeSendBuffer(IPacket packet)
        {
            var sendPacket = packet as Packet;

            var sendBuffer = new Vector<byte>();

            sendBuffer.AddRange(BitConverter.GetBytes(sendPacket.GetLength()));

            sendBuffer.AddRange(sendPacket.Body);

            return sendBuffer.ToArray();
        }
    }
}
