using System;
using System.Collections;
using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    // DeviceType values
    public static class RepriceConstants
    {
        public const string RepriceUnknown = "UNKNOWN";
        public const string RepriceLOIChange = "LOI_CHANGE";
        public const string RepriceIRChange = "IR_CHANGE";
    }

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
        public decimal NewValue { get; set; }
        [DataMember(Name = "previousValue")]
        public decimal PreviousValue { get; set; }
    }

    [DataContract]
    public class EventStatusValues
    {
        [DataMember(Name = "newValue")]
        public string NewValue { get; set; }
        [DataMember(Name = "previousValue")]
        public string PreviousValue { get; set; }
    }

    [DataContract]
    public class CreateProjectEventCriteria : IValidator
    {
        [DataMember(Name = "errorMsg", EmitDefaultValue = false)]
        public string ErrorMsg { get; set; }
        [DataMember(Name = "orderNumber", EmitDefaultValue = false)]
        public string OrderNumber { get; set; }
        [DataMember(Name = "projectId", EmitDefaultValue = false)]
        public int ProjectID { get; set; }
        [DataMember(Name = "quotaGroups", EmitDefaultValue = false)]
        public EventQuotaGroup[] QuotaGroups { get; set; }
        [DataMember(Name = "quoteId", EmitDefaultValue = false)]
        public string QuoteID { get; set; }
        [DataMember(Name = "requestId", EmitDefaultValue = false)]
        public string requestID { get; set; }
        [DataMember(Name = "salesOrderId", EmitDefaultValue = false)]
        public string salesOrderID { get; set; }
        [DataMember(Name = "status", EmitDefaultValue = false)]
        public string Status { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.requestID, this.Status);
            Validator.IsNotNull(this.ProjectID);
        }
    }

    [DataContract]
    public class EventQuotaGroup : IValidator
    {
        [DataMember(Name = "lineItemId", EmitDefaultValue = false)]
        public string LineItemID { get; set; }
        [DataMember(Name = "pickerEventId", EmitDefaultValue = false)]
        public int PickerEventID { get; set; }
        [DataMember(Name = "quotaGroupId", EmitDefaultValue = false)]
        public int QuotaGroupID { get; set; }
        [DataMember(Name = "salesOrderDetailId", EmitDefaultValue = false)]
        public string SalesOrderDetailID { get; set; }
        [DataMember(Name = "surveyId", EmitDefaultValue = false)]
        public int SurveyID { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.LineItemID, this.SalesOrderDetailID);
            Validator.IsNotNull(this.PickerEventID, this.QuotaGroupID, this.SurveyID);
        }
    }

    [DataContract]
    public class StateChangeEventCriteria : IValidator
    {
        [DataMember(Name = "action", EmitDefaultValue = false)]
        public string Action { get; set; }
        [DataMember(Name = "extLineItemId", EmitDefaultValue = false)]
        public string ExtLineItemID { get; set; }
        [DataMember(Name = "extProjectId", EmitDefaultValue = false)]
        public string ExtProjectID { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.Action, this.ExtLineItemID, this.ExtProjectID);
        }
    }

    public class Webhook
    {
        [DataMember(Name = "webhookId")]
        public string WebhookID { get; set; }
    }

    public class Reprice
    {
        [DataMember(Name = "IR")]
        public decimal IR { get; set; }
        [DataMember(Name = "LOI")]
        public decimal LOI { get; set; }
        [DataMember(Name = "approvalId")]
        public string ApprovalID { get; set; }
        [DataMember(Name = "calculatedMaxPrice")]
        public decimal CalculatedMaxPrice { get; set; }
        [DataMember(Name = "completes")]
        public int Completes { get; set; }
        [DataMember(Name = "eventId")]
        public int EventID { get; set; }
        [DataMember(Name = "id")]
        public int ID { get; set; }
        [DataMember(Name = "newEstimatedCost")]
        public decimal NewEstimatedCost { get; set; }
        [DataMember(Name = "originalCPI")]
        public decimal OriginalCPI { get; set; }
        [DataMember(Name = "originalEstimatedCost")]
        public decimal OriginalEstimatedCost { get; set; }
        [DataMember(Name = "projectId")]
        public int ProjectID { get; set; }
        [DataMember(Name = "quotaGroupId")]
        public int QuotaGroupID { get; set; }
        [DataMember(Name = "reason")]
        public string Reason { get; set; }
        [DataMember(Name = "requestNewPrice")]
        public decimal RequestNewPrice { get; set; }
        [DataMember(Name = "requiredCompletes")]
        public int RequiredCompletes { get; set; }
    }

    [DataContract]
    public class RepriceEventCriteria : IValidator
    {
        [DataMember(Name = "IR", EmitDefaultValue = false)]
        public decimal IR { get; set; }
        [DataMember(Name = "LOI", EmitDefaultValue = false)]
        public int LOI { get; set; }
        [DataMember(Name = "approvalId", EmitDefaultValue = false)]
        public string ApprovalId { get; set; }
        [DataMember(Name = "calculatedMaxPrice", EmitDefaultValue = false)]
        public decimal CalculatedMaxPrice { get; set; }
        [DataMember(Name = "completes", EmitDefaultValue = false)]
        public int Completes { get; set; }
        [DataMember(Name = "newEstimatedCost", EmitDefaultValue = false)]
        public decimal NewEstimatedCost { get; set; }
        [DataMember(Name = "originalCPI", EmitDefaultValue = false)]
        public decimal OriginalCPI { get; set; }
        [DataMember(Name = "originalEstimatedCost", EmitDefaultValue = false)]
        public decimal OriginalEstimatedCost { get; set; }
        [DataMember(Name = "projectId", EmitDefaultValue = false)]
        public int ProjectId { get; set; }
        [DataMember(Name = "quotaGroupId", EmitDefaultValue = false)]
        public int QuotaGroupId { get; set; }
        [DataMember(Name = "reason", EmitDefaultValue = false)]
        public string Reason { get; set; }
        [DataMember(Name = "requestNewPrice", EmitDefaultValue = false)]
        public decimal RequestNewPrice { get; set; }
        [DataMember(Name = "requiredCompletes", EmitDefaultValue = false)]
        public int RequiredCompletes { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.ApprovalId);
            Validator.IsNotNull(this.IR, this.LOI, this.CalculatedMaxPrice);
            Validator.IsNotNull(this.Completes, this.NewEstimatedCost, this.OriginalCPI);
            Validator.IsNotNull(this.OriginalEstimatedCost, this.ProjectId, this.QuotaGroupId);
            Validator.IsNotNull(this.RequestNewPrice, this.RequiredCompletes);
            Validator.IsRepriceReason(this.Reason);
        }
    }

}
