using Kosher.Collections;
using Kosher.Log;
using Kosher.Sockets.Interface;
using System;
using System.Text;

namespace EchoClient.Serializer
{
    internal class DummyDeserializer : IPacketDeserializer
    {
        public void Deserialize(Vector<byte> buffer)
        {
            var bytes = buffer.Read(sizeof(int));
            var length = BitConverter.ToInt32(bytes);
            var bodyBytes = buffer.Read(length);
            var text = Encoding.UTF8.GetString(bodyBytes);
            LogHelper.Debug($"Receive Deserialize : {text}");
        }

        public void Deserialize(BinaryReader stream)
        {
            var bodyBytes = stream.ReadBytes((int)stream.BaseStream.Length);
            LogHelper.Debug($"Receive Deserialize : {Encoding.UTF8.GetString(bodyBytes)}");
        }

        public bool IsTakedCompletePacket(Vector<byte> buffer)
        {
            if (buffer.Count < sizeof(int))
            {
                return false;
            }
            LogHelper.Debug($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] buffer size : {buffer.Count}");
            var bytes = buffer.Peek(sizeof(int));
            var length = BitConverter.ToInt32(bytes);
            return length + sizeof(int) <= buffer.Count;
        }

        public bool IsTakedCompletePacket(BinaryReader stream)
        {
            if (stream.BaseStream.Length < sizeof(int))
            {
                return false;
            }

            LogHelper.Debug($"[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] buffer size : {stream.BaseStream.Length}");
            var length = stream.ReadInt32();
            stream.BaseStream.Seek(-sizeof(int), SeekOrigin.Current);
            return length + sizeof(int) <= stream.BaseStream.Length;
        }
    }
}
