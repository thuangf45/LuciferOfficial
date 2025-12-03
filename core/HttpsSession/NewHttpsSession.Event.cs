using LuciferCore.Attributes;
using LuciferCore.Event.Dispatcher;
using LuciferCore.Manager.Log;
using LuciferCore.NetCoreServer;
using System.Net.Sockets;
using System.Net.WebSockets;
using static LuciferCore.Core.Simulation;

namespace Yourspace.Session
{
    [RateLimiter(5, 2)]
    public partial class NewHttpsSession
    {
        protected override void OnReceivedRequestInternal(HttpRequest request)
        {
            // 1️⃣ Nếu là API thì bỏ qua static file, chuyển thẳng sang EventDispatcher
            if (request.Url.StartsWith("/api/", StringComparison.OrdinalIgnoreCase))
            {
                OnReceivedRequest(request); // sẽ gọi EventDispatcher.Handle(request, this)
                return;
            }
            if (request.Method == "GET")
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

        protected override void OnReceivedRequest(HttpRequest request)
            => EventDispatcher.Handle(request, this);

        protected override void OnReceivedRequestError(HttpRequest request, string error)    
            => GetModel<LogManager>().LogSystem(this,$"Request error: {error}", LogLevel.ERROR);

        protected override void OnError(SocketError error){}
    }


}
