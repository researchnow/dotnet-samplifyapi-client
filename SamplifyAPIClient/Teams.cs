using System.Runtime.Serialization;
using System;

namespace Dynata.SamplifyAPIClient
{ 
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
        public string Status { get; set; }
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