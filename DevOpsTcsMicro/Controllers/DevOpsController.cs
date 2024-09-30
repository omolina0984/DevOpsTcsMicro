using DevOpsTcsMicro.Models;
using DevOpsTcsMicro.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace DevOpsTcsMicro.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class DevOpsController : ControllerBase
    {
        private readonly IApiManagerService _apiManager;

   

        public DevOpsController(IApiManagerService apiManager)
        {
            _apiManager = apiManager;
        }

        [HttpPost("generate-token")]
        public IActionResult GenerateToken([FromBody] TokenRequest request)
        {
            if (string.IsNullOrEmpty(request.Recipient))
            {
                return BadRequest(new { message = "Recipient is required." });
            }

            var token = _apiManager.GenerateJwtToken(request.Recipient);
            return Ok(new { token });
        }

        [HttpPost]
        public IActionResult Post([FromBody] DevOpsModel request)
        {
            // Validate API Key
            if (!Request.Headers.TryGetValue("X-Parse-REST-API-Key", out var apiKey) || !_apiManager.ValidateApiKey(apiKey))
            {
                return Unauthorized(new { message = "Invalid API Key." });
            }

            // Validate JWT Token

            if (!Request.Headers.TryGetValue("X-JWT-KWY", out var jwtToken) || !_apiManager.ValidateJwtToken(jwtToken))
            {
                return Unauthorized(new { message = "Invalid JWT Token." });
            }



            var responseMessage = $"Hello {request.To} your message will be sent.";
            return Ok(new { message = responseMessage });
        }

      
    }
}

