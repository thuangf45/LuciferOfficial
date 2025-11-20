using LuciferCore.Helper;
using LuciferCore.NetCoreServer;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Yourspace.Server
{
    public partial class NewWssServer
    {
        private static SslContext CreateSslContext()
        {
            string certPath = EnvHelper.Get("CERTIFICATE", "assets/tools/certificates/server.pfx");
            string password = EnvHelper.Get("CERT_PASSWORD", "RootCA!SecureKey@Example2025Strong");

            return new SslContext(SslProtocols.Tls12, new X509Certificate2(certPath, password));
        }

        private static int GetPortFromEnv() => int.Parse(EnvHelper.Get("PORT_WSS", "8443"));

        private void AddStaticContent() => AddStaticContent(EnvHelper.Get("WWW", "assets/client"));
    }
}
