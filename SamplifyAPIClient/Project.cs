using System.Runtime.Serialization;

namespace ResearchNow.SamplifyAPIClient
{
    // State values for Projects and Line Items.
    public static class StateConstants
    {
        public const string Provisioned = "PROVISIONED";
        public const string Launched = "LAUNCHED";
        public const string Paused = "PAUSED";
        public const string Closed = "CLOSED";
        public const string AwaitingApproval = "AWAITING_APPROVAL";
        public const string Invoiced = "INVOICED";
    }

    // DeviceType values
    public static class DeviceTypeConstants
    {
        public const string DeviceTypeMobile = "mobile";
        public const string DeviceTypeDesktop = "desktop";
        public const string DeviceTypeTablet = "tablet";
    }

    // ExclusionType values
    public static class ExclusionTypeConstants
    {
        public const string ExclusionTypeProject = "PROJECT";
        public const string ExclusionTypeTag = "TAG";
    }

    // Project's category
    [DataContract]
    public class Category
    {
        [DataMember(Name = "surveyTopic")]
        public string[] SurveyTopic { get; set; }
    }

    // Project's exclusions
    [DataContract]
    public class Exclusions
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "list")]
        public string[] List { get; set; }
    }

    [DataContract]
    public class ProjectHeader : Model
    {
        [DataMember(Name = "extProjectId")]
        public string ExtProjectID { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
    }

    [DataContract]
    public class Project : ProjectHeader
    {
        [DataMember(Name = "notificationEmails")]
        public string[] NotificationEmails { get; set; }
        [DataMember(Name = "devices")]
        public string[] Devices { get; set; }
        [DataMember(Name = "category")]
        public Category Category { get; set; }
        [DataMember(Name = "lineItems")]
        public LineItem[] LineItems { get; set; }
        [DataMember(Name = "exclusions")]
        public Exclusions Exclusions { get; set; }
    }

    // CreateUpdateProjectCriteria has the fields to create or update a project.
    [DataContract]
    public class CreateUpdateProjectCriteria
    {
        [DataMember(Name = "extProjectId")]
        public string ExtProjectID { get; set; }
        [DataMember(Name = "title")]
        public string Title { get; set; }
        [DataMember(Name = "notificationEmails")]
        public string[] NotificationEmails { get; set; }
        [DataMember(Name = "devices")]
        public string[] Devices { get; set; }
        [DataMember(Name = "category")]
        public Category Category { get; set; }
        [DataMember(Name = "lineItems")]
        public LineItemCriteria[] LineItems { get; set; }
        [DataMember(Name = "exclusions")]
        public Exclusions Exclusions { get; set; }
    }

    [DataContract]
    public class BuyProjectCriteria
    {
        [DataMember(Name = "extLineItemId")]
        public string ExtLineItemID { get; set; }
        [DataMember(Name = "surveyURL")]
        public string SurveyURL { get; set; }
        [DataMember(Name = "surveyTestURL")]
        public string SurveyTestURL { get; set; }
    }

    [DataContract]
    public class ProjectReport
    {
        [DataMember(Name = "extProjectId")]
        public string ExtProjectID { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
        [DataMember(Name = "attempts")]
        public int Attempts { get; set; }
        [DataMember(Name = "completes")]
        public int Completes { get; set; }
        [DataMember(Name = "screenouts")]
        public int Screenouts { get; set; }
        [DataMember(Name = "overquotas")]
        public int Overquotas { get; set; }
        [DataMember(Name = "starts")]
        public int Starts { get; set; }
        [DataMember(Name = "conversion")]
        public int Conversion { get; set; }
        [DataMember(Name = "remainingCompletes")]
        public int RemainingCompletes { get; set; }
        [DataMember(Name = "actualMedianLOI")]
        public int ActualMedianLOI { get; set; }
        [DataMember(Name = "incurredCost")]
        public decimal IncurredCost { get; set; }
        [DataMember(Name = "estimatedCost")]
        public decimal EstimatedCost { get; set; }
        [DataMember(Name = "lineItems")]
        public LineItemReport[] LineItems { get; set; }
    }

    // Represents Survey Topic for a project. Required to setup a project.
    [DataContract]
    public class SurveyTopic
    {
        [DataMember(Name = "topic")]
        public string Topic { get; set; }
        [DataMember(Name = "description")]
        public string Description { get; set; }
    }
}
