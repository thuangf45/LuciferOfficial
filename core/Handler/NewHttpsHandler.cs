using LuciferCore.Attributes;
using LuciferCore.Extensions;
using LuciferCore.Handler.Https;
using LuciferCore.Manager.Session;
using LuciferCore.NetCoreServer;

namespace Yourspace.Handler
{
    internal class NewHttpsHandler : HttpsHandlerBase
    {
        public override string Type => "/yourapi";

        [Safe]
        [Authorize(UserRole.Guest)]
        [RateLimiter(100, 1)]
        [HttpHead("")]
        protected override void HeadHandle([Data] HttpRequest request, [Session] HttpsSession session)
        {
            session.Ok("API is working!");
        }

        [Safe]
        [Authorize(UserRole.Guest)]
        [RateLimiter(100, 1)]
        [HttpGet("")]
        protected override void GetHandle([Data] HttpRequest request, [Session] HttpsSession session)
        {
            session.Ok("API is working!");
        }

        [Safe]
        [Authorize(UserRole.Guest)]
        [RateLimiter(100, 1)]
        [HttpPost("")]
        protected override void PostHandle([Data] HttpRequest request, [Session] HttpsSession session)
        {
            session.Ok("API is working!");
        }

        [Safe]
        [Authorize(UserRole.Guest)]
        [RateLimiter(100, 1)]
        [HttpPut("")]
        protected override void PutHandle([Data] HttpRequest request, [Session] HttpsSession session)
        {
            session.Ok("API is working!");
        }

        [Safe]
        [Authorize(UserRole.Guest)]
        [RateLimiter(100, 1)]
        [HttpDelete("")]
        protected override void DeleteHandle([Data] HttpRequest request, [Session] HttpsSession session)
        {
            session.Ok("API is working!");
        }

        [Safe]
        [Authorize(UserRole.Guest)]
        [RateLimiter(100, 1)]
        [HttpTrace("")]
        protected override void TraceHandle([Data] HttpRequest request, [Session] HttpsSession session)
            => base.TraceHandle(request, session);

        [Safe]
        [Authorize(UserRole.Guest)]
        [RateLimiter(100,1)]
        [HttpOptions("")]
        protected override void OptionsHandle([Data] HttpRequest request, [Session] HttpsSession session)
            => base.OptionsHandle(request, session);
    }
}
