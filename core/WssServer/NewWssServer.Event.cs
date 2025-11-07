using LuciferCore.Manager.Log;
using System.Net.Sockets;
using static LuciferCore.Core.Simulation;

namespace Yourspace.Server
{
    public partial class NewWssServer
    {
        protected override void OnError(SocketError error) => GetModel<LogManager>().LogSystem(this, $"Chat WebSocket server caught an error with code {error}", LogLevel.ERROR);
        protected override void OnStarted() => GetModel<LogManager>().LogSystem(this, "Started!");
        protected override void OnStopped() => GetModel<LogManager>().LogSystem(this, "Stopped!");
    }
}
