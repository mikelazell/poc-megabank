using System.Security.Claims;
using Megabank.Api.Interfaces;

namespace Megabank.Api.Services
{
    public class CurrentUserService : ICurrentUserService
    {
        private readonly HttpContext _httpContext;

        public CurrentUserService(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext.HttpContext ?? throw new ArgumentNullException(nameof(httpContext));
        }
        public string? Id
        {
            get
            {
                if (_httpContext.User.Identity is ClaimsIdentity identity)
                {
                    return identity.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier")?.Value;
                }
                return null;
            }
        }
    }

}
