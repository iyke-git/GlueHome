using System;
using System.IdentityModel.Tokens.Jwt;
using gluehome.delivery.security.models;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace gluehome.delivery.security
{
    public class JwtTokenGenerator : IJwt
    {
        /*private const string SECRET_KEY = "5A7134743777217A25432A462D4A404E635266556A586E3272357538782F413F";
        public static readonly SymmetricSecurityKey KEY = new
            SymmetricSecurityKey(Convert.FromHexString(SECRET_KEY));*/
        private readonly IConfiguration Configuration;

        public JwtTokenGenerator(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string Generate()
        {
            var clientOptions = new ClientSecurityOptions();
            Configuration.GetSection(ClientSecurityOptions.Security)
                .Bind(clientOptions);
            var signingKey = new SymmetricSecurityKey(
                Convert.FromHexString(clientOptions.ClientKey));
            var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);
            var header = new JwtHeader(credentials);

            var expiry = DateTime.UtcNow.AddMinutes(60);
            var ts = (int)(expiry - new DateTime(1970, 1, 1)).TotalSeconds;

            var payload = new JwtPayload
            {
                {"sub","testSubject"},
                {"Name","Scott"},
                {"email","smithtest@test.com"},
                {"exp",ts},
                {"iss","http://localhost:5000"},
                {"aud","http://localhost:5000"}
            };

            var secToken = new JwtSecurityToken(header, payload);
            var handler = new JwtSecurityTokenHandler();
            var tokenString = handler.WriteToken(secToken);
            return tokenString;
        }
    }
}
