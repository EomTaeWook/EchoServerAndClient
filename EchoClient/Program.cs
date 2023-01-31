using EchoClient.Serializer;
using Kosher.Extensions.Log;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;

namespace EchoClient
{
    internal class Program
    {
        static void Main(string[] args)
        {
            LogBuilder.Configuration(LogConfigXmlReader.Load($"{AppContext.BaseDirectory}KosherLog.config"));
            LogBuilder.Build();

            var sessionCreator = new SessionCreator(() =>
            {
                return Tuple.Create<IPacketSerializer, IPacketDeserializer, ICollection<ISessionComponent>>(new DummySerializer(),
                                                                                                            new DummyDeserializer(),
                                                                                                            new List<ISessionComponent>() {  });
            });
            
            Parallel.For(0, 1000, (i) =>
            {
                try
                {
                    var client = new ClientModule(sessionCreator);
                    client.Connect("13.125.232.85", 35000);
                    //client.Connect("127.0.0.1", 31000);
                    client.Send(new Packet($"client : {i}"));
                }
                catch(Exception ex)
                {
                    LogHelper.Error(ex);
                }
                
            });

            Console.ReadLine();
        } 
    }
}