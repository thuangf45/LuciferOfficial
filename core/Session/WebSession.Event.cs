using LuciferCore.Event;
using LuciferCore.Manager.Log;
using LuciferCore.NetCoreServer;
using System.Net.Sockets;
using Yourspace.Server;
using static LuciferCore.Core.Simulation;

namespace Yourspace.Session
{
    public partial class WebSession
    {
        protected override void OnReceivedRequest(HttpRequest request)
        {
            GetModel<WebServer>().UpdateNumberRequest();
            EventDispatcher.Handle(request, this);
        }

        protected override void OnReceivedRequestError(HttpRequest request, string error)    
            => GetModel<LogManager>().Log($"Request error: {error}", LogLevel.ERROR, LogSource.SYSTEM);

        protected override void OnError(SocketError error){}
    }
}
