using System;
using System.Runtime.Serialization;

namespace ResearchNow.SamplifyAPIClient
{
    // Action values for changing Line Item state.
    public static class ActionConstants
    {
        public const string ActionLaunched = "launch";
        public const string ActionPaused = "pause";
        public const string ActionClosed = "close";
    }

    // FeasibilityStatus values
    public static class FeasibilityStatusConstants
    {
        public const string FeasibilityStatusReady = "READY";
        public const string FeasibilityStatusProcessing = "PROCESSING";
    }

    [DataContract]
    public class QuotaPlan
    {
        [DataMember(Name = "filters")]
        public QuotaFilters[] Filters { get; set; }
        [DataMember(Name = "quotaGroups")]
        public QuotaGroup[] QuotaGroups { get; set; }
    }

    [DataContract]
    public class QuotaFilters
    {
        [DataMember(Name = "attributeId")]
        public string AttributeID { get; set; }
        [DataMember(Name = "options")]
        public string[] Options { get; set; }
    }

    [DataContract]
    public class QuotaGroup
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "quotas")]
        public Quota[] Quotas { get; set; }
    }

    [DataContract]
    public class Quota
    {
        [DataMember(Name = "attributeId")]
        public string AttributeID { get; set; }
        [DataMember(Name = "options")]
        public QuotaOption[] Options { get; set; }
    }

    [DataContract]
    public class QuotaOption
    {
        [DataMember(Name = "option")]
        public string[] Option { get; set; }
        [DataMember(Name = "perc")]
        public decimal Perc { get; set; }
    }

    [DataContract]
    public class EndLinks
    {
        [DataMember(Name = "complete")]
        public string Complete { get; set; }
        [DataMember(Name = "screenout")]
        public string Screenout { get; set; }
        [DataMember(Name = "overquota")]
        public string OverQuota { get; set; }
    }

    [DataContract]
    public class LineItemHeader : Model
    {
        [DataMember(Name = "extLineItemId")]
        public string ExtLineItemID { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "stateReason")]
        public string StateReason { get; set; }
        [DataMember(Name = "launchedAt")]
        public string RawDTStringLaunchedAt { get; set; }

        [IgnoreDataMember]
        public DateTime? LaunchedAt => Util.ConvertToDateTimeNullable(RawDTStringLaunchedAt);
    }

    [DataContract]
    public class LineItem : LineItemHeader
    {
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "countryISOCode")]
        public string CountryISOCode { get; set; }
        [DataMember(Name = "languageISOCode")]
        public string LanguageISOCode { get; set; }
        [DataMember(Name = "surveyURL")]
        public string SurveyURL { get; set; }
        [DataMember(Name = "surveyTestURL")]
        public string SurveyTestURL { get; set; }
        [DataMember(Name = "indicativeIncidence")]
        public decimal IndicativeIncidence { get; set; }
        [DataMember(Name = "daysInField")]
        public int DaysInField { get; set; }
        [DataMember(Name = "lengthOfInterview")]
        public int LengthOfInterview { get; set; }
        [DataMember(Name = "requiredCompletes")]
        public int RequiredCompletes { get; set; }
        [DataMember(Name = "quotaPlan")]
        public QuotaPlan QuotaPlan { get; set; }
        [DataMember(Name = "endLinks")]
        public EndLinks EndLinks { get; set; }
    }

    // LineItemCriteria has the fields to create or update a Line Item.
    [DataContract]
    public class LineItemCriteria
    {
        [DataMember(Name = "extLineItemId")]
        public string ExtLineItemID { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "countryISOCode")]
        public string CountryISOCode { get; set; }
        [DataMember(Name = "languageISOCode")]
        public string LanguageISOCode { get; set; }
        [DataMember(Name = "surveyURL")]
        public string SurveyURL { get; set; }
        [DataMember(Name = "surveyTestURL")]
        public string SurveyTestURL { get; set; }
        [DataMember(Name = "indicativeIncidence")]
        public decimal IndicativeIncidence { get; set; }
        [DataMember(Name = "daysInField")]
        public int DaysInField { get; set; }
        [DataMember(Name = "lengthOfInterview")]
        public int LengthOfInterview { get; set; }
        [DataMember(Name = "requiredCompletes")]
        public int RequiredCompletes { get; set; }
        [DataMember(Name = "quotaPlan")]
        public QuotaPlan QuotaPlan { get; set; }
    }

    [DataContract]
    public class BuyProjectLineItem
    {
        [DataMember(Name = "extLineItemId")]
        public string ExtLineItemID { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
    }

    [DataContract]
    public class LineItemReport
    {
        [DataMember(Name = "extLineItemId")]
        public string ExtLineItemID { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "attempts")]
        public int Attempts { get; set; }
        [DataMember(Name = "completes")]
        public int Completes { get; set; }
        [DataMember(Name = "overquotas")]
        public int Overquotas { get; set; }
        [DataMember(Name = "screenouts")]
        public int Screenouts { get; set; }
        [DataMember(Name = "starts")]
        public int Starts { get; set; }
        [DataMember(Name = "conversion")]
        public decimal Conversion { get; set; }
        [DataMember(Name = "remainingCompletes")]
        public int RemainingCompletes { get; set; }
        [DataMember(Name = "actualMedianLOI")]
        public int ActualMedianLOI { get; set; }
        [DataMember(Name = "incurredCost")]
        public decimal IncurredCost { get; set; }
        [DataMember(Name = "estimatedCost")]
        public decimal EstimatedCost { get; set; }
    }

    [DataContract]
    public class Feasibility
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "costPerInterview")]
        public decimal CostPerInterview { get; set; }
        [DataMember(Name = "expiry")]
        public string RawDTStringExpiry { get; set; }
        [DataMember(Name = "currency")]
        public string Currency { get; set; }
        [DataMember(Name = "feasible")]
        public bool Feasible { get; set; }

        [IgnoreDataMember]
        public DateTime? Expiry => Util.ConvertToDateTimeNullable(RawDTStringExpiry);
    }

    // Supported attribute for a country and language. Required to build up the Quota Plan.
    [DataContract]
    public class Attribute
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "options")]
        public AttributeOption[] Options { get; set; }
    }

    [DataContract]
    public class AttributeOption
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}
