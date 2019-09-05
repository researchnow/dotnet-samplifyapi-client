using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    [DataContract]
    public class Event
    {
        [DataMember(Name = "actions", IsRequired = false, EmitDefaultValue = false)]
        public Actions Actions { get; set; }
        [DataMember(Name = "eventId")]
        public long EventID { get; set; }
        [DataMember(Name = "eventType")]
        public string EventType { get; set; }
        [DataMember(Name = "extLineItemId")]
        public string ExtLineItemID { get; set; }
        [DataMember(Name = "extProjectId")]
        public string ExtProjectID { get; set; }
        [DataMember(Name = "parentEventId")]
        public long? ParentEventID { get; set; }
        [DataMember(Name = "resource", IsRequired = false, EmitDefaultValue = false)]
        public Resource Resource { get; set; }
        [DataMember(Name = "createdAt")]
        public string RawDTStringCreatedAt;

        [IgnoreDataMember]
        public DateTime? CreatedAt => Util.ConvertToDateTimeNullable(RawDTStringCreatedAt);
    }

    [DataContract]
    public class Actions
    {
        [DataMember(Name = "acceptURL")]
        public string AcceptURL { get; set; }
        [DataMember(Name = "rejectURL")]
        public string RejectURL { get; set; }
    }

    [DataContract]
    public class Resource
    {
        [DataMember(Name = "costPerInterview")]
        public EventValues CostPerInterview;
        [DataMember(Name = "estimatedCost")]
        public EventValues EstimatedCost { get; set; }
        [DataMember(Name = "lengthOfInterview")]
        public EventValues LengthOfInterview { get; set; }
        [DataMember(Name = "incidenceRate")]
        public EventValues IndicativeIncidence { get; set; }
        [DataMember(Name = "isAutoAccepted")]
        public bool? IsAutoAccepted { get; set; }
        [DataMember(Name = "currency")]
        public string Currency { get; set; }
        [DataMember(Name = "status")]
        public EventStatusValues Status { get; set; }
        [DataMember(Name = "reason")]
        public string Reason { get; set; }
    }

    [DataContract]
    public class EventValues
    {
        [DataMember(Name = "newValue")]
        public double NewValue { get; set; }
        [DataMember(Name = "previousValue")]
        public double PreviousValue { get; set; }
    }

    [DataContract]
    public class EventStatusValues
    {
        [DataMember(Name = "newValue")]
        public string NewValue { get; set; }
        [DataMember(Name = "previousValue")]
        public string PreviousValue { get; set; }
    }

}
