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
        public List<byte[]> Messages = new ();

        [WsMessage("JoinRoom")]
        [Safe]
        [RateLimiter(10,1)]
        public void JoinRoom([Session] NewWssSession session, [Data] WsPacketModel data)
        {
            var groupId = data.Header.To;
            if (groupId != null)
            {
                var group = session.AddGroup(groupId);
                lock (group)
                {
                    group.Add(session);
                }
            }
        }

        [WsMessage("GetMessage")]
        [Safe]
        [RateLimiter(10, 1)]
        public void GetMessage([Session] NewWssSession session, [Data] WsPacketModel data)
        {
            foreach (var message in Messages)
            {
                session.SendBinaryAsync(message);
            }
        }

        [WsMessage("ChatMessage")]
        [Safe]
        [RateLimiter(10,1)]
        public void SendChat([Session] NewWssSession session, [Data] WsPacketModel data)
        {
            if (data.Header.ContentType != null && data.Header.ContentType == "application/json")
            {
                var message = data.BodyJson;
                Console.WriteLine(message);
                //Messages.Add(data.Buffer);
            }

            session.Multicast(data, session.GetGroup(data.Header.To));
        }

    }
}
