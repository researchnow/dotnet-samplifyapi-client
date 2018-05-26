using System;
using System.Runtime.Serialization;

namespace ResearchNow.SamplifyAPIClient
{
    [DataContract]
    internal class TokenRequest
    {
        [DataMember(Name = "clientId")]
        internal readonly string clientID;

        [DataMember(Name = "username")]
        internal readonly string username;

        [DataMember(Name = "password")]
        internal readonly string password;

        internal TokenRequest(string clientID, string username, string password)
        {
            this.clientID = clientID;
            this.username = username;
            this.password = password;
        }
    }

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
}
