using LuciferCore.NetCoreServer;
using System.Net.Sockets;
using System.Net.WebSockets;
using System.Text;

namespace Yourspace.Session
{
    public partial class NewWssSession : WssSession
    {
        protected Dictionary<string, string> Mapping { get; }
        public NewWssSession(WssServer server) : base(server)
        {
            // Ẩn URL cho handshake
            Mapping = new Dictionary<string, string>()
            {
                { "/", "/index.html" },      // Map ẩn URL chính
                { "/404", "/404.html" }      // ← FIX: Thêm 404 để tránh KeyNotFound
                // Thêm nếu cần: { "/tool", "/tool.html" }
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
