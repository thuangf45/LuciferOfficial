using LuciferCore.Helper;
using LuciferCore.NetCoreServer;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;

namespace Yourspace.Server
{
    public partial class WebServer
    {

        private int numberRequest = 0;
        public int NumberRequest { get => numberRequest; set => numberRequest = value; }

        private int numberUser = 0;
        public int NumberUser { get => numberUser; set => numberUser = value; }

        public void UpdateNumberRequest() => Interlocked.Increment(ref numberRequest);
        public object GetServerStatus() => new { NumberRequest, NumberUser };

        private static SslContext CreateSslContext()
        {
            string certPath = EnvHelper.Get("CERTIFICATE", "assets/tools/certificates/server.pfx");
            string password = EnvHelper.Get("CERT_PASSWORD", "qwerty");

            return new SslContext(SslProtocols.Tls13, new X509Certificate2(certPath, password));
        }

        private static int GetPortFromEnv() => int.Parse(EnvHelper.Get("PORT", "2000"));

        private void AddStaticContent() => AddStaticContent(EnvHelper.Get("WWW", "assets/client"));

    }

}
