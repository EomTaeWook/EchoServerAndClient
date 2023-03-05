using EchoClient.Serializer;
using Kosher.Extensions.Log;
using Kosher.Log;
using Kosher.Sockets;
using Kosher.Sockets.Interface;
using System.Numerics;

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

            var client = new ClientModule(sessionCreator);
            Parallel.For(0, 1, (i) =>
            {
                try
                {
                    
                    client.Connect("13.125.232.85", 31000);
                    //client.Connect("127.0.0.1", 31000);
                    for(long ii = 0; ii<1000; ++ii)
                    {
                        client.Send(new Packet($"string client : {ii}"));
                    }
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