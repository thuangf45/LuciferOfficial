using LuciferCore.NetCoreServer;
using System.Collections.Concurrent;

namespace Yourspace.Session
{
    public partial class NewWssSession
    {
        protected Dictionary<string, string> Mapping { get; }

        private readonly ConcurrentDictionary<string, HashSet<NewWssSession>> Groups = new();

        public NewWssSession(WssServer server) : base(server)
        {
            // Ẩn URL cho handshake
            Mapping = new Dictionary<string, string>()
            {
                { "/","/index.html" },
                { "/404", "/404.html" },
                { "/home", "/home.html" },
                { "/entertainment", "/entertainment.html" },
                { "/forum", "/forum.html" },
                { "/journal", "/journal.html" },
                { "/search", "/search.html" },
                { "/shop", "/shop.html" },
                { "/tool", "/tool.html" },
                { "/tutorial", "/tutorial.html" }
            };
        }
        
    }
}
