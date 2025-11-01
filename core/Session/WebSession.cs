using LuciferCore.NetCoreServer;

namespace Yourspace.Session
{
    public partial class WebSession : HttpsSession
    {
        public WebSession(HttpsServer server) : base(server) 
        {
            Mapping= new Dictionary<string, string>()
            {
                { "/","/index.html" },
                { "/404", "/404.html" },
                { "/entertainment", "/entertainment.html" },
                { "/forum", "/forum.html" },
                { "/journal", "/journal.html" },
                { "/search", "/search.html" },
                { "/shop", "/shop.html" },
                { "/tool", "/tool.html" },
                { "/tutorial", "/tutorial.html" }
            };
        }
        protected override string GetStaticPath(HttpRequest request) => base.GetStaticPath(request);
    }
}
