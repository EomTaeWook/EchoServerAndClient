﻿using Kosher.Collections;
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
        public void Deserialize(Vector<byte> buffer)
        {
            var bodyBytes = buffer.Read(buffer.LongCount);
            _protocolHandler.Process(bodyBytes);
        }
        public bool IsTakedCompletePacket(Vector<byte> buffer)
        {
            if (buffer.Count <= 0)
            {
                return false;
            }
            LogHelper.Debug($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] buffer size : {buffer.Count}");

            return true;
        }
    }
}