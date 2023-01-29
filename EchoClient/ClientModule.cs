using Kosher.Sockets;

namespace EchoClient
{
    internal class ClientModule : BaseClient
    {
        public ClientModule(SessionCreator sessionCreator) : base(sessionCreator)
        {

        }

        protected override void OnConnected(Session session)
        {
            
        }

        protected override void OnDisconnected(Session session)
        {
            
        }
    }
}
