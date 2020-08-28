using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    [DataContract]
    public class Roles
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "assignableRoles")]
        public string[] AssignableRoles { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "allowedActions")]
        public AllowedAction[] AllowedActions { get; set; }
    }

    [DataContract]
    public class AllowedAction
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "action")]
        public string Action { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}