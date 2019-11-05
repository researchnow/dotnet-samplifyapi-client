using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    [DataContract]
    public class UserInfo
    {
        [DataMember(Name = "applications")]
        public Application[] Applications { get; set; }
        [DataMember(Name = "companyId")]
        public int CompanyID { get; set; }
        [DataMember(Name = "companyName")]
        public string CompanyName { get; set; }
        [DataMember(Name = "email")]
        public string Email { get; set; }
        [DataMember(Name = "fullName")]
        public string FullName { get; set; }
        [DataMember(Name = "userName")]
        public string UserName { get; set; }
    }

    [DataContract]
    public class Application
    {
        [DataMember(Name = "appId")]
        public int AppID { get; set; }
        [DataMember(Name = "current")]
        public bool Current { get; set; }
        [DataMember(Name = "default")]
        public bool Default { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }
}
