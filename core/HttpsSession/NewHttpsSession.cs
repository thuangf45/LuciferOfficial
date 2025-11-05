using LuciferCore.NetCoreServer;

namespace Yourspace.Session
{
    public partial class NewHttpsSession : HttpsSession
    {
        protected virtual Dictionary<string, string> Mapping { get; set; }
        public NewHttpsSession(HttpsServer server) : base(server) 
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

        protected virtual string GetStaticPath(HttpRequest request)
        {
            var url = request.Url ?? "/";
            var index = url.IndexOf('?');
            var path = index < 0 ? url : url.Substring(0, index);

            // 1️⃣ Nếu URL có trong mapping (vd /tool, /forum, /shop)
            if (Mapping.TryGetValue(path, out var mappedFile)) return mappedFile;

            // 2️⃣ Nếu là truy cập trực tiếp file .html → ép về 404
            if (path.EndsWith(".html", StringComparison.OrdinalIgnoreCase)) return Mapping["/404"];

            // 3️⃣ Nếu file cache thực sự tồn tại (css/js/png/ico, v.v.)
            if (Cache.Find(path).Item1) return path;

            // 4️⃣ Còn lại → không có gì hợp lệ → 404
            return Mapping["/404"];
        }
    }
}
