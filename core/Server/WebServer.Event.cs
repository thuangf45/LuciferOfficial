using LuciferCore.Manager.Log;
using System.Net.Sockets;
using static LuciferCore.Core.Simulation;

namespace Yourspace.Server
{
    public partial class WebServer
    {
        protected override void OnError(SocketError error) => GetModel<LogManager>().LogSystem($"HTTPS server caught an error: {error}", LogLevel.ERROR);
        protected override void OnStarted() => GetModel<LogManager>().LogSystem("Server started!");
        protected override void OnStopped() => GetModel<LogManager>().LogSystem("Server stopped!");
    }
}
