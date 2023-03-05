using Kosher.Log;
using Kosher.Sockets.Interface;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace EchoClient.Serializer
{
    internal class DummyDeserializer : IPacketDeserializer
    {
        private static readonly int _sizeToInt = sizeof(int);
        public void Deserialize(BinaryReader stream)
        {
            var length = stream.ReadInt32();
            var bodyBytes = stream.ReadBytes(length);
            var text = Encoding.UTF8.GetString(bodyBytes);
            LogHelper.Debug($"Receive Deserialize : {text}");
            if(text .Equals("string client : 92"))
            {

            }
        }
        public bool IsTakedCompletePacket(BinaryReader stream)
        {
            if (stream.BaseStream.Length < _sizeToInt)
            {
                return false;
            }
            var t = new byte[4];
            stream.Read(t, 0, _sizeToInt);
            var length = BitConverter.ToInt32(t);
            LogHelper.Debug($"Receive Length : {length}");
            if (length > 2048)
            {
                stream.BaseStream.Seek(-_sizeToInt, SeekOrigin.Current);
                return false;
            }

            LogHelper.Debug($"{Thread.GetCurrentProcessorId()}[{DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss:fff")}] buffer size : {stream.BaseStream.Length}");
            
            stream.BaseStream.Seek(-_sizeToInt, SeekOrigin.Current);
            return length + _sizeToInt <= stream.BaseStream.Length;
        }
    }
}
