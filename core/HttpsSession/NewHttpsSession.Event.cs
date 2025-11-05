using LuciferCore.Event;
using LuciferCore.Manager.Log;
using LuciferCore.NetCoreServer;
using System.Net.Sockets;
using static LuciferCore.Core.Simulation;

namespace Yourspace.Session
{
    public partial class NewHttpsSession
    {
        protected override void OnReceivedRequestInternal(HttpRequest request)
        {
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
        {
            EventDispatcher.Handle(request, this);
        }

        protected override void OnReceivedRequestError(HttpRequest request, string error)    
            => GetModel<LogManager>().Log($"Request error: {error}", LogLevel.ERROR, LogSource.SYSTEM);

        protected override void OnError(SocketError error){}
    }


}
