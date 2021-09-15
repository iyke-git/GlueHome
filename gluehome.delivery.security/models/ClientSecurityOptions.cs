
namespace gluehome.delivery.security.models
{
    public class ClientSecurityOptions
    {
        public const string Security = "ClientSecurity";

        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string ClientKey { get; set; }
    }
}