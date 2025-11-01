using LuciferCore.NetCoreServer;
using Yourspace.Session;
using LuciferCore.Server;
using System.Net;
using LuciferCore.Attributes;

namespace Yourspace.Server
{
    [Server("WebServer")]
    public partial class WebServer : HttpsServer, IServer
    {
        public WebServer(SslContext context, IPAddress address, int port) : base(context, address, port) { }
        public WebServer() : base(CreateSslContext(), IPAddress.Any, GetPortFromEnv()) => AddStaticContent();
        protected override SslSession CreateSession() => new WebSession(this);

        public void RequestStart() => Start();
        public void RequestStop() => Stop();
        public void RequestRestart() => Restart();
    }
}
