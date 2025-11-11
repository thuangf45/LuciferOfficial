using LuciferCore.Attributes;
using LuciferCore.Handler;
using LuciferCore.Manager.Log;
using System.Text;
using Yourspace.Server;
using Yourspace.Session;
using static LuciferCore.Core.Simulation;
using LuciferCore.Extensions;

namespace Yourspace.Handler
{
    internal class NewWssHandler : WssHandlerBase
    {
        public override string Type => "/wss";

        [WsMessage("ChatMessage")]
        [Safe]
        [RateLimiter(10,1)]
        public void SendChat([Session] NewWssSession session, [Data] WsPacketModel data)
        {
            string msg = Encoding.UTF8.GetString(data.Body.Span);
            GetModel<LogManager>().LogSystem(this, $"Guest: {msg}");

            session.Multicast(data, session.GetGroup(data.Header.To));
        }

    }
}
