using LuciferCore.Attributes;
using LuciferCore.Database;
using LuciferCore.Extensions;
using LuciferCore.Handler.Wss;
using Test.core.Entities;
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
            var accounts = DB.GetTable<Account>();

            //Console.WriteLine("Số lượng account: " + accounts.ToList().Count);

            session.Multicast(data, session.GetGroup(data.Header.To));
        }

    }
}
