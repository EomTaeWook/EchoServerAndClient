﻿using Dignus.Collections;
using Dignus.Sockets.Interfaces;

namespace EchoClient.Serializer
{
    internal class DummySerializer : IPacketSerializer
    {
        public ArrayQueue<byte> MakeSendBuffer(IPacket packet)
        {
            var sendPacket = packet as Packet;

            var sendBuffer = new ArrayQueue<byte>();

            sendBuffer.AddRange(BitConverter.GetBytes(sendPacket.GetLength()));

            sendBuffer.AddRange(sendPacket.Body);

            return sendBuffer;
        }

        ArraySegment<byte> IPacketSerializer.MakeSendBuffer(IPacket packet)
        {
            var sendPacket = packet as Packet;

            var sendBuffer = new ArrayQueue<byte>();

            sendBuffer.AddRange(BitConverter.GetBytes(sendPacket.GetLength()));

            sendBuffer.AddRange(sendPacket.Body);

            return sendBuffer.ToArray();
        }
    }
}
