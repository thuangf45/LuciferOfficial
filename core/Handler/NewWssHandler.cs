using LuciferCore.Attributes;
using LuciferCore.Extensions;
using LuciferCore.Handler.Wss;
using Yourspace.Session;

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
            //string msg = Encoding.UTF8.GetString(data.Body.Span);
            //GetModel<LogManager>().LogSystem(this, $"Guest: {msg}");
            session.Multicast(data, session.GetGroup(data.Header.To));
        }

    }
}
