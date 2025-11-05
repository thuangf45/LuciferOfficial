using LuciferCore.Attributes;
using LuciferCore.NetCoreServer;
using LuciferCore.Server;
using System.Net;
using Yourspace.Session;

namespace Yourspace.Server
{
    [Server("WssServer")]
    public partial class NewWssServer : WssServer, IServer
    {
        public NewWssServer(SslContext context, IPAddress address, int port) : base(context, address, port) { }
        public NewWssServer() : base(CreateSslContext(), IPAddress.Any, GetPortFromEnv()) => AddStaticContent();

        protected override SslSession CreateSession() { return new NewWssSession(this); }
        public void RequestStart() => Start();
        public void RequestStop() => Stop();
        public void RequestRestart() => Restart();

    }
}
