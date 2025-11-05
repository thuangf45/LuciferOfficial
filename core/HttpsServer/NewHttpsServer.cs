using LuciferCore.NetCoreServer;
using Yourspace.Session;
using LuciferCore.Server;
using System.Net;
using LuciferCore.Attributes;

namespace Yourspace.Server
{
    [Server("HttpsServer")]
    public partial class NewHttpsServer : HttpsServer, IServer
    {
        public NewHttpsServer(SslContext context, IPAddress address, int port) : base(context, address, port) { }
        public NewHttpsServer() : base(CreateSslContext(), IPAddress.Any, GetPortFromEnv()) => AddStaticContent();
        protected override SslSession CreateSession() => new NewHttpsSession(this);

        public void RequestStart() => Start();
        public void RequestStop() => Stop();
        public void RequestRestart() => Restart();
    }
}
