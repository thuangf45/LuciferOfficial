using LuciferCore.Attributes;
using LuciferCore.Handler;
using LuciferCore.Manager.Log;
using System.Text;
using Yourspace.Server;
using Yourspace.Session;
using static LuciferCore.Core.Simulation;

namespace Yourspace.Handler
{
    internal class NewWssHandler : WssHandlerBase
    {
        public override string Type => "/yourtype";
        [WsMessage("SendChat")]
        public void Send([Packet] WsPacketModel data, NewWssSession session)
        {
            string msg = Encoding.UTF8.GetString(data.Body);
            GetModel<LogManager>().Log($"Guest: {msg}");
            ((NewWssServer)session.Server).MulticastText(msg);
        }
    }
}
