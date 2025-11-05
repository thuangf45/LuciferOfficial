using LuciferCore.Manager.Log;
using System.Net.Sockets;
using static LuciferCore.Core.Simulation;

namespace Yourspace.Server
{
    public partial class NewWssServer
    {
        protected override void OnError(SocketError error) => GetModel<LogManager>().LogSystem($"Chat WebSocket server caught an error with code {error}", LogLevel.ERROR);
        protected override void OnStarted() => GetModel<LogManager>().LogSystem("Server started!");
        protected override void OnStopped() => GetModel<LogManager>().LogSystem("Server stopped!");
    }
}
