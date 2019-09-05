using System;
using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    [DataContract]
    public class Model
    {
        [DataMember(Name = "stateLastUpdatedAt")]
        public string RawDTStringLastUpdated;
        [DataMember(Name = "createdAt")]
        public string RawDTStringCreatedAt;
        [DataMember(Name = "updatedAt")]
        public string RawDTStringUpdatedAt;

        [IgnoreDataMember]
        public DateTime? StateLastUpdatedAt => Util.ConvertToDateTimeNullable(RawDTStringLastUpdated);
        [IgnoreDataMember]
        public DateTime? CreatedAt => Util.ConvertToDateTimeNullable(RawDTStringCreatedAt);
        [IgnoreDataMember]
        public DateTime? UpdatedAt => Util.ConvertToDateTimeNullable(RawDTStringUpdatedAt);
    }
}
