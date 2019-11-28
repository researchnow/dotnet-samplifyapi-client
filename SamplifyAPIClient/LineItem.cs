using System;
using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
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

    // DeliveryType values
    public static class DeliveryTypeConstants
    {
        public const string Slow = "SLOW";
        public const string Balanced = "BALANCED";
        public const string Fast = "FAST";
    }

    // OperatorType values
    public static class OperatorTypeConstants
    {
        public const string Include = "include";
        public const string Exclude = "exclude";
    }

    [DataContract]
    public class QuotaPlan : IValidator
    {
        [DataMember(Name = "filters")]
        public QuotaFilters[] Filters { get; set; }
        [DataMember(Name = "quotaGroups")]
        public QuotaGroup[] QuotaGroups { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNotNull(this.Filters, this.QuotaGroups);
        }
    }

    [DataContract]
    public class QuotaFilters : IValidator
    {
        [DataMember(Name = "attributeId")]
        public string AttributeID { get; set; }
        [DataMember(Name = "options")]
        public string[] Options { get; set; }
        [DataMember(Name = "operator")]
        public string Operator { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsOperatorType(this.Operator);
        }
    }

    [DataContract]
    public class QuotaGroup
    {
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "quotaCells")]
        public QuotaCell[] QuotaCells { get; set; }
        [DataMember(Name = "quotaGroupId")]
        public string QuotaGroupID { get; set; }
    }

    [DataContract]
    public class QuotaCell
    {
        [DataMember(Name = "quotaNodes")]
        public QuotaNode[] QuotaNodes { get; set; }
        [DataMember(Name = "count")]
        public int Count { get; set; }
        [DataMember(Name = "perc")]
        public int Perc { get; set; }
        [DataMember(Name = "quotaCellId")]
        public string QuotaCellID { get; set; }
    }

    [DataContract]
    public class QuotaNode : IValidator
    {
        [DataMember(Name = "attributeId")]
        public string AttributeID { get; set; }
        [DataMember(Name = "options")]
        public string[] OptionIDs { get; set; }
        [DataMember(Name = "operator")]
        public string Operator { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsOperatorType(this.Operator);
        }
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
        [DataMember(Name = "securityKey1")]
        public string SecurityKey1 { get; set; }
        [DataMember(Name = "securityLevel")]
        public string SecurityLevel { get; set; }
        [DataMember(Name = "securityKey2")]
        public string SecurityKey2 { get; set; }
    }

    [DataContract]
    public class Source
    {
        [DataMember(Name = "id")]
        public int Id { get; set; }
    }

    [DataContract]
    public class Target
    {
        [DataMember(Name = "count")]
        public int Count { get; set; }
        [DataMember(Name = "dailyLimit")]
        public int DailyLimit { get; set; }
        [DataMember(Name = "softLaunch")]
        public int SoftLaunch { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
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
        [DataMember(Name = "surveyURLParams")]
        public SurveyURLParams[] SurveyURLParams { get; set; }
        [DataMember(Name = "surveyTestURL")]
        public string SurveyTestURL { get; set; }
        [DataMember(Name = "surveyTestURLParams")]
        public SurveyURLParams[] SurveyTestURLParams { get; set; }
        [DataMember(Name = "indicativeIncidence")]
        public decimal IndicativeIncidence { get; set; }
        [DataMember(Name = "daysInField")]
        public int DaysInField { get; set; }
        [DataMember(Name = "lengthOfInterview")]
        public int LengthOfInterview { get; set; }
        [DataMember(Name = "deliveryType")]
        public string DeliveryType { get; set; }
        [DataMember(Name = "dynataLineItemReferenceId")]
        public string DynataLineItemReferenceId { get; set; }
        [DataMember(Name = "requiredCompletes")]
        public int RequiredCompletes { get; set; }
        [DataMember(Name = "quotaPlan")]
        public QuotaPlan QuotaPlan { get; set; }
        [DataMember(Name = "endLinks")]
        public EndLinks EndLinks { get; set; }
        [DataMember(Name = "sources")]
        public Source[] Source { get; set; }
        [DataMember(Name = "targets")]
        public Target[] Target { get; set; }
    }

    [DataContract]
    public class SurveyURLParams
    {
        [DataMember(Name = "key")]
        public string Key { get; set; }
        [DataMember(Name = "values")]
        public string[] Values { get; set; }
    }

    // LineItemCriteria has the fields to create or update a Line Item.
    [DataContract]
    public class LineItemCriteria : IValidator
    {
        [DataMember(Name = "extLineItemId")]
        public string ExtLineItemID { get; set; }
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }
        [DataMember(Name = "countryISOCode", EmitDefaultValue = false)]
        public string CountryISOCode { get; set; }
        [DataMember(Name = "languageISOCode", EmitDefaultValue = false)]
        public string LanguageISOCode { get; set; }
        [DataMember(Name = "surveyURL", EmitDefaultValue = false)]
        public string SurveyURL { get; set; }
        [DataMember(Name = "surveyTestURL", EmitDefaultValue = false)]
        public string SurveyTestURL { get; set; }
        [DataMember(Name = "indicativeIncidence", EmitDefaultValue = false)]
        public decimal IndicativeIncidence { get; set; }
        [DataMember(Name = "daysInField", EmitDefaultValue = false)]
        public int DaysInField { get; set; }
        [DataMember(Name = "lengthOfInterview", EmitDefaultValue = false)]
        public int LengthOfInterview { get; set; }
        [DataMember(Name = "deliveryType", EmitDefaultValue = false)]
        public string DeliveryType { get; set; }
        [DataMember(Name = "requiredCompletes", EmitDefaultValue = false)]
        public int RequiredCompletes { get; set; }
        [DataMember(Name = "quotaPlan", EmitDefaultValue = false)]
        public QuotaPlan QuotaPlan { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.ExtLineItemID, this.Title, this.CountryISOCode, this.LanguageISOCode);
            Validator.IsCountryCodeOrNull(this.CountryISOCode);
            Validator.IsLanguageCodeOrNull(this.LanguageISOCode);
            Validator.IsUrlOrNull(this.SurveyURL);
            Validator.IsUrlOrNull(this.SurveyTestURL);
            Validator.IsNonZero<decimal>(this.IndicativeIncidence);
            Validator.IsNonZero<int>(this.DaysInField);
            Validator.IsNonZero<int>(this.LengthOfInterview);
            Validator.IsDeliveryTypeOrNull(this.DeliveryType);
            Validator.IsNonZero<int>(this.RequiredCompletes);
            Validator.IsNotNull(this.QuotaPlan);
            Validator.Validate(this.QuotaPlan);
        }
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
        [DataMember(Name = "conversion")]
        public decimal Conversion { get; set; }
        [DataMember(Name = "remainingCompletes")]
        public int RemainingCompletes { get; set; }
        [DataMember(Name = "actualMedianLOI")]
        public int ActualMedianLOI { get; set; }
        [DataMember(Name = "incurredCost")]
        public decimal IncurredCost { get; set; }
        [DataMember(Name = "completesRefused")]
        public int CompletesRefused { get; set; }
        [DataMember(Name = "countryISOCode")]
        public string CountryISOCode { get; set; }
        [DataMember(Name = "currency")]
        public string Currency { get; set; }
        [DataMember(Name = "incompletes")]
        public int Incompletes { get; set; }
        [DataMember(Name = "languageISOCode")]
        public string LanguageISOCode { get; set; }
        [DataMember(Name = "lastAcceptedIncidenceRate")]
        public decimal LastAcceptedIncidenceRate { get; set; }
        [DataMember(Name = "lastAcceptedLOI")]
        public int LastAcceptedLOI { get; set; }
        [DataMember(Name = "stateReason")] 
        public string StateReason { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
    }

    [DataContract]
    public class DetailedLineItemReport
    {
        [DataMember(Name = "extLineItemId")]
        public string ExtLineItemID { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "stateReason")]
        public string StateReason { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "countryISOCode")]
        public string CountryISOCode { get; set; }
        [DataMember(Name = "languageISOCode")]
        public string LanguageISOCode { get; set; }
        [DataMember(Name = "sources")]
        public Source[] Source { get; set; }
        [DataMember(Name = "stats")]
        public DetailedStats Stats { get; set; }
        [DataMember(Name = "cost")]
        public Cost Cost { get; set; }
        [DataMember(Name = "quotaGroups")]
        public DetailedQuotaGroupReport[] QuotaGroups { get; set; }
    }

    [DataContract]
    public class DetailedQuotaGroupReport
    {
        [DataMember(Name = "quotaGroupId")]
        public string QuotaGroupId { get; set; }
        [DataMember(Name = "stats")]
        public DetailedStats Stats { get; set; }
        [DataMember(Name = "quotaCells")]
        public DetailedQuotaCellReport[] QuotaCells { get; set; }
    }

    [DataContract]
    public class DetailedQuotaCellReport
    {
        [DataMember(Name = "quotaCellId")]
        public string QuotaCellId { get; set; }
        [DataMember(Name = "quotaNodes")]
        public QuotaNode[] QuotaNodes { get; set; }
        [DataMember(Name = "stats")]
        public DetailedStats Stats { get; set; }
    }

    [DataContract]
    public class Cost
    {
        [DataMember(Name = "costPerUnit")]
        public decimal CostPerUnit { get; set; }
        [DataMember(Name = "currency")]
        public string Currency { get; set; }
        [DataMember(Name = "estimatedCost")]
        public decimal EstimatedCost { get; set; }
        [DataMember(Name = "incurredCost")]
        public decimal IncurredCost { get; set; }
        [DataMember(Name = "detailedCost")]
        public DetailedCost[] DetailedCost { get; set; }
    }

    [DataContract]
    public class DetailedCost
    {
        [DataMember(Name = "costPerUnit")]
        public decimal CostPerUnit { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "estimatedCost")]
        public decimal EstimatedCost { get; set; }
        [DataMember(Name = "incurredCost")]
        public decimal IncurredCost { get; set; }
        [DataMember(Name = "deliveredUnits")]
        public int DeliveredUnits { get; set; }
        [DataMember(Name = "requestedUnits")]
        public int RequestedUnits { get; set; }
    }

    [DataContract]
    public class Feasibility
    {
        [DataMember(Name = "status")]
        public string Status { get; set; }
        [DataMember(Name = "costPerInterview")]
        public decimal CostPerInterview { get; set; }
        [DataMember(Name = "currency")]
        public string Currency { get; set; }
        [DataMember(Name = "feasible")]
        public bool Feasible { get; set; }
        [DataMember(Name = "totalCount")]
        public int TotalCount { get; set; }

        [DataMember(Name = "valueCounts")]
        public ValueCount[] ValueCounts { get; set; }
    }

    [DataContract]
    public class Quote
    {
        [DataMember(Name = "costPerUnit")]
        public decimal CostPerUnit { get; set; }
        [DataMember(Name = "currency")]
        public string Currency { get; set; }
        [DataMember(Name = "detailedQuote")]
        public DetailedQuote[] DetailedQuote { get; set; }
        [DataMember(Name = "estimatedCost")]
        public decimal EstimatedCost { get; set; }
    }

    [DataContract]
    public class DetailedQuote
    {
        [DataMember(Name = "costPerUnit")]
        public decimal CostPerUnit { get; set; }
        [DataMember(Name = "estimatedCost")]
        public decimal EstimatedCost { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "units")]
        public int Units { get; set; }
    }

    [DataContract]
    public class ValueCount
    {
        [DataMember(Name = "quotaCells")]
        public FeasibilityQuotaCell[] QuotaCells { get; set; }
    }

    [DataContract]
    public class FeasibilityQuotaCell
    {
        [DataMember(Name = "feasibilityCount")]
        public int FeasibilityCount { get; set; }
        [DataMember(Name = "quotaNodes")]
        public QuotaNode[] QuotaNodes { get; set; }
    }

    // Supported attribute for a country and language. Required to build up the Quota Plan.
    [DataContract]
    public class Attribute
    {
        [DataMember(Name = "category")]
        public AttributeCategory Category { get; set; }
        [DataMember(Name = "exclusions")]
        public string[] Exclusions { get; set; }
        //[DataMember(Name = "extras")]
        //public string Extras { get; set; }
        [DataMember(Name = "format")]
        public string Format { get; set; }
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "isAllowedInFilters")]
        public bool IsAllowedInFilters { get; set; }
        [DataMember(Name = "isAllowedInQuotas")]
        public bool IsAllowedInQuotas { get; set; }
        [DataMember(Name = "localizedText")]
        public string LocalizedText { get; set; }
        [DataMember(Name = "name")]
        public string Name { get; set; }
        [DataMember(Name = "options")]
        public AttributeOption[] Options { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
        [DataMember(Name = "tier")]
        public string Tier { get; set; }
        [DataMember(Name = "type")]
        public string Type { get; set; }
        
    }

    [DataContract]
    public class AttributeOption
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "localizedText")]
        public string LocalizedText { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }

    [DataContract]
    public class AttributeCategory
    {
        [DataMember(Name = "mainCategory")]
        public AttrCategory ID { get; set; }
        [DataMember(Name = "subCategory")]
        public AttrCategory LocalizedText { get; set; }
    }

    [DataContract]
    public class AttrCategory
    {
        [DataMember(Name = "id")]
        public string ID { get; set; }
        [DataMember(Name = "localizedText")]
        public string LocalizedText { get; set; }
        [DataMember(Name = "text")]
        public string Text { get; set; }
    }
}
