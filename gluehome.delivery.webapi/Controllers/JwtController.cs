using System;
using System.Net;
using System.Net.Http;
using gluehome.delivery.security;
using gluehome.delivery.security.models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace gluehome.delivery.webapi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class JwtController : ControllerBase
    {
        private readonly IJwt _jwtGenerator;
        private readonly IConfiguration _config;
        private readonly ILogger<JwtController> _logger;

        public JwtController(ILogger<JwtController> logger,
            IConfiguration configuration,
            IJwt jwtGenerator)
        {
            _logger = logger;
            _jwtGenerator = jwtGenerator;
            _config = configuration;
        }

        [HttpGet]
        public IActionResult Get(string clientId, string clientSecret)
        {
            var clientSecurity = new ClientSecurityOptions();
            _config.GetSection(ClientSecurityOptions.Security).Bind(clientSecurity);
            if (!string.IsNullOrEmpty(clientId) && !string.IsNullOrEmpty(clientSecret))
            {
                if (clientId.Equals(clientSecurity.ClientId) && clientSecret.Equals(clientSecurity.ClientSecret))
                {
                    return Ok(_jwtGenerator.Generate());
                }
            }
            return Unauthorized("The client Id and secret do not match");
        }
    }
}