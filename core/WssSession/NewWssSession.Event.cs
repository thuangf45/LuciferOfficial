using LuciferCore.Manager.Log;
using LuciferCore.NetCoreServer;
using System.Net.Sockets;
using System.Text;
using static LuciferCore.Core.Simulation;

namespace Yourspace.Session
{
    public partial class NewWssSession
    {
        public override void OnWsReceived(byte[] buffer, long offset, long size)
        {
            string message = Encoding.UTF8.GetString(buffer, (int)offset, (int)size);
            GetModel<LogManager>().Log("Incoming: " + message);

            // Multicast message to all connected sessions
            ((WssServer)Server).MulticastText(message);

            // If the buffer starts with '!' the disconnect the current session
            if (message == "!")
                Close(1000);
        }
        protected override void OnReceivedRequestInternal(HttpRequest request)
        {
            if (request.Method == "GET" && !IsWebSocket)
            {
                var staticPath = GetStaticPath(request);
                if (!string.IsNullOrEmpty(staticPath) && Cache.Find(staticPath) is var response && response.Item1)
                {
                    OnReceivedCachedRequest(request, response.Item2);
                    return;
                }
            }

            OnReceivedRequest(request);
        }

        public override void OnWsConnected(HttpRequest request)
            => SendTextAsync("Hello from WebSocket chat! Please send a message or '!' to disconnect the client!");
        

        public override void OnWsDisconnected() 
            => GetModel<LogManager>().Log($"Chat WebSocket session with Id {Id} disconnected!");

        protected override void OnError(SocketError error) { }
    }
}
