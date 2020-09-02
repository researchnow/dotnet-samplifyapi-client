using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    // TeamStatusConstants values for status of the team.
    public static class TeamStatusConstants
    {
        public const string StatusActive = "ACTIVE";
        public const string StatusInactive = "INACTIVE";
    }

    [DataContract]
    public class Team
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "default")]
        public bool Default { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
        [DataMember(Name = "status")]
        public TeamStatusConstants Status { get; set; }
        [DataMember(Name = "createdAt")]
        public string RawDTStringCreatedAt;
        [DataMember(Name = "updatedAt")]
        public string RawDTStringUpdatedAt;

        [IgnoreDataMember]
        public DateTime? CreatedAt => Util.ConvertToDateTimeNullable(RawDTStringCreatedAt);
        [IgnoreDataMember]
        public DateTime? UpdatedAt => Util.ConvertToDateTimeNullable(RawDTStringUpdatedAt);
    }
}