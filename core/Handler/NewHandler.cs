using LuciferCore.Attributes;
using LuciferCore.Handler;
using LuciferCore.NetCoreServer;

namespace Yourspace.Handler
{
    internal class NewHandler : HttpsHandlerBase
    {
        public override string Type => "/yourapi";

        [HttpHead("")]
        protected override void HeadHandle(HttpRequest request, HttpsSession session)
        {
            OkHandle(session, "API is working!");
        }

        [HttpGet("")]
        protected override void GetHandle(HttpRequest request, HttpsSession session)
        {
            OkHandle(session, "API is working!");
        }

        [HttpPost("")]
        protected override void PostHandle(HttpRequest request, HttpsSession session)
        {
            OkHandle(session, "API is working!");
        }
        [HttpPut("")]
        protected override void PutHandle(HttpRequest request, HttpsSession session)
        {
            OkHandle(session, "API is working!");
        }

        [HttpDelete("")]
        protected override void DeleteHandle(HttpRequest request, HttpsSession session)
        {
            OkHandle(session, "API is working!");
        }

        [HttpTrace("")]
        protected override void TraceHandle(HttpRequest request, HttpsSession session)
        {
            OkHandle(session, "API is working!");
        }

        [HttpOptions("")]
        protected override void OptionsHandle(HttpRequest request, HttpsSession session)
        {
            OkHandle(session, "API is working!");
        }
    }
}
