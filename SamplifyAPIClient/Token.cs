using System;
using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    [DataContract]
    public class TokenResponse
    {
        [DataMember(Name = "accessToken")]
        public string AccessToken { get; set; }

        [DataMember(Name = "expiresIn")]
        public uint ExpiresIn { get; set; }

        [DataMember(Name = "refreshToken")]
        public string RefreshToken { get; set; }

        [DataMember(Name = "refreshExpiresIn")]
        public uint RefreshExpiresIn { get; set; }

        [IgnoreDataMember]
        public DateTime Acquired { get; set; }

        public bool AccessTokenExpired
        {
            get
            {
                if (string.IsNullOrEmpty(this.AccessToken) || this.Acquired == DateTime.MinValue
                    || (uint)DateTime.Now.Subtract(this.Acquired).TotalSeconds > this.ExpiresIn)
                {
                    return true;
                }
                return false;
            }
        }
    }

    [DataContract]
    internal class TokenRequest
    {
        [DataMember(Name = "clientId")]
        internal readonly string ClientID;

        [DataMember(Name = "username")]
        internal readonly string Username;

        [DataMember(Name = "password")]
        internal readonly string Password;

        internal TokenRequest(string clientID, string username, string password)
        {
            this.ClientID = clientID;
            this.Username = username;
            this.Password = password;
        }
    }

    [DataContract]
    internal class LogoutRefreshRequest
    {
        [DataMember(Name = "clientId")]
        internal readonly string ClientID;

        [DataMember(Name = "accessToken", EmitDefaultValue = false)]
        internal readonly string AccessToken;

        [DataMember(Name = "refreshToken")]
        internal readonly string RefreshToken;

        internal LogoutRefreshRequest(string clientID, string accessToken, string refreshToken)
        {
            this.ClientID = clientID;
            this.AccessToken = accessToken;
            this.RefreshToken = refreshToken;
        }
    }
}
