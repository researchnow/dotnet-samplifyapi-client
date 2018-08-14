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
    public class Category : IValidator
    {
        [DataMember(Name = "surveyTopic")]
        public string[] SurveyTopic { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.SurveyTopic);
        }
    }

    // Project's exclusions
    [DataContract]
    public class Exclusions : IValidator
    {
        [DataMember(Name = "type")]
        public string Type { get; set; }
        [DataMember(Name = "list")]
        public string[] List { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.Type);
            Validator.IsNonEmptyString(this.List);
            Validator.IsExclusionTypeOrNull(this.Type);
        }
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

    // ProjectCriteria has the fields to create or update a project.
    [DataContract]
    public class ProjectCriteria : IValidator
    {
        [DataMember(Name = "extProjectId")]
        public string ExtProjectID { get; set; }
        [DataMember(Name = "title", EmitDefaultValue = false)]
        public string Title { get; set; }
        [DataMember(Name = "notificationEmails", EmitDefaultValue = false)]
        public string[] NotificationEmails { get; set; }
        [DataMember(Name = "devices", EmitDefaultValue = false)]
        public string[] Devices { get; set; }
        [DataMember(Name = "category", EmitDefaultValue = false)]
        public Category Category { get; set; }
        [DataMember(Name = "lineItems", EmitDefaultValue = false)]
        public LineItemCriteria[] LineItems { get; set; }
        [DataMember(Name = "exclusions", EmitDefaultValue = false)]
        public Exclusions Exclusions { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.ExtProjectID, this.Title);
            Validator.IsNonEmptyString(this.NotificationEmails);
            Validator.IsEmail(this.NotificationEmails);
            Validator.IsNonEmptyString(this.Devices);
            Validator.IsDeviceType(this.Devices);
            Validator.IsNotNull(this.Category);
            Validator.Validate(this.Category);
            Validator.IsNotNull(this.LineItems);
            Validator.ValidateAll(this.LineItems);
            Validator.Validate(this.Exclusions);
        }
    }

    [DataContract]
    public class BuyProjectCriteria : IValidator
    {
        [DataMember(Name = "extLineItemId")]
        public string ExtLineItemID { get; set; }
        [DataMember(Name = "surveyURL")]
        public string SurveyURL { get; set; }
        [DataMember(Name = "surveyTestURL")]
        public string SurveyTestURL { get; set; }

        void IValidator.IsValid()
        {
            Validator.IsNonEmptyString(this.ExtLineItemID);
            Validator.IsNonEmptyString(this.SurveyURL);
            Validator.IsUrlOrNull(this.SurveyURL);
            Validator.IsNonEmptyString(this.SurveyTestURL);
            Validator.IsUrlOrNull(this.SurveyTestURL);
        }
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
        public decimal Conversion { get; set; }
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
