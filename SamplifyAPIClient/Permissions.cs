using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    [DataContract]
    public class ProjectPermissions
    {
        [DataMember(Name = "extProjectId")]
        public string ExtProjectID { get; set; }
        [DataMember(Name = "currentUser")]
        public CurrentUser CurrentUser { get; set; }
        [DataMember(Name = "users")]
        public UserData[] Users { get; set; }
        [DataMember(Name = "teams")]
        public TeamData[] Teams { get; set; }
    }

    [DataContract]
    public class CurrentUser
    {
        [DataMember(Name = "roles")]
        public string[] Roles { get; set; }
    }

    [DataContract]
    public class UserData
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }
        [DataMember(Name = "username")]
        public string Username { get; set; }
        [DataMember(Name = "role")]
        public string Role { get; set; }
    }

    [DataContract]
    public class TeamData
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
    }

    // UpsertPermissionsCriteria has the fields insert or update the project permissions.
    [DataContract]
    public class UpsertPermissionsCriteria : IValidator
    {
        [DataMember(Name = "extProjectId")]
        public string ExtProjectID { get; set; }
        [DataMember(Name = "users", EmitDefaultValue = false)]
        public UserPermission[] UserPermissions { get; set; }
        [DataMember(Name = "teams", EmitDefaultValue = false)]
        public TeamPermission[] TeamPermissions { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.ExtProjectID);
        }
    }

    [DataContract]
    public class TeamPermission
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }
    }

    [DataContract]
    public class UserPermission
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }
        [DataMember(Name = "role")]
        public string Role { get; set; }
    }
}