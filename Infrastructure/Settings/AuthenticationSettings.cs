using System.ComponentModel;

namespace Infrastructure.Settings
{
    public class AuthenticationSettings
    {
        public string Issuer { get; set; }
        public string PrivateKey { get; set; }
        [Description("Token expiry time defined in minutes.")]
        public int ExpiryTime { get; set; }
        
    }
}
