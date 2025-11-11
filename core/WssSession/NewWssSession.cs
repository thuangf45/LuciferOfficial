using LuciferCore.NetCoreServer;
using System.Collections.Concurrent;

namespace Yourspace.Session
{
    public partial class NewWssSession : WssSession
    {
        public HashSet<NewWssSession> GetGroup(string groupId)
        {
            if (groupId != null && Groups.TryGetValue(groupId, out var group))
                return group;

            return null; // group chưa tồn tại
        }

        public HashSet<NewWssSession> AddGroup(string groupId)
        {
            // Sử dụng GetOrAdd để đảm bảo thread-safe
            return Groups.GetOrAdd(groupId, _ => new HashSet<NewWssSession>());
        }

        public void RemoveFromGroup(string groupId, NewWssSession session)
        {
            if (Groups.TryGetValue(groupId, out var group))
            {
                lock (group)
                {
                    group.Remove(session);
                    if (group.Count == 0)
                        Groups.TryRemove(groupId, out _);
                }
            }
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
