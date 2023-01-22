using Megabank.Api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Megabank.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessagesController : ControllerBase
    {
        private const string PublicMessage = "The API doesn't require an access token to share this message.";
        private const string ProtectedMessage = "The API successfully validated your access token.";
        private const string AdminMessage = "The API successfully recognized you as an admin.";

        [HttpGet("public")]
        public ApiResponse GetPublicMessage()
        {
            return new ApiResponse(PublicMessage);
        }

        [HttpGet("protected")]
        [Authorize]
        public ApiResponse GetProtectedMessage()
        {
            return new ApiResponse(ProtectedMessage);
        }

        [HttpGet("admin")]
        [Authorize(Policy = "Admin")]
        public ApiResponse GetAdminMessage()
        {
            return new ApiResponse(AdminMessage);
        }
    }
}