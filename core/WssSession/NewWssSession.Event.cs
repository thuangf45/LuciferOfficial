using LuciferCore.Attributes;
using LuciferCore.Event.Dispatcher;
using LuciferCore.Manager.Log;
using LuciferCore.NetCoreServer;
using System.Net.Sockets;
using static LuciferCore.Core.Simulation;

namespace Yourspace.Session
{
    [RateLimiter(5, 2)]
    public partial class NewWssSession : WssSession
    {
        // Trong NewWssSession.cs
        public override void OnWsReceived(byte[] buffer, long offset, long size)
            =>  EventDispatcher.Handle(this, buffer, offset, size);

        protected override void OnReceivedRequest(HttpRequest request)
        { 
            if (!IsWebSocket)
                EventDispatcher.Handle(request, this); 
        }

        protected override void OnReceivedRequestInternal(HttpRequest request)
        {
            if (!IsWebSocket)
            {
                // 1️⃣ Nếu là API thì bỏ qua static file, chuyển thẳng sang EventDispatcher
                if (request.Url.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
                {
                    OnReceivedRequest(request); // sẽ gọi EventDispatcher.Handle(request, this)
                    return;
                }
            }

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
        {

        }
        

        public override void OnWsDisconnected() 
            => GetModel<LogManager>().LogSystem(this, $"Chat WebSocket session with Id {Id} disconnected!");

        protected override void OnError(SocketError error) { }
    }
}
