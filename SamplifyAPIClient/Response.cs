using System;
using System.Runtime.Serialization;

namespace Dynata.SamplifyAPIClient
{
    // Custom StatusType values in API responses.
    public static class StatusTypeConstants
    {
        public const string StatusTypeSuccess = "success";
        public const string StatusTypeFail = "fail";
        public const string StatusTypeUnknown = "unknown";
    }

    // Base class for all response types
    [DataContract]
    public class Response
    {
        internal Response() { }

        [DataMember(Name = "status")]
        public ResponseStatus ResponseStatus { get; set; }
        [DataMember(Name = "meta")]
        public Meta Meta { get; set; }
        [IgnoreDataMember]
        public bool HasError
        {
            get
            {
                if (this.ResponseStatus != null)
                {
                    return this.ResponseStatus.HTTPErrorResponse != null ||
                               ResponseStatus.Message == StatusTypeConstants.StatusTypeFail;
                }
                return false;
            }
        }

        internal void SetHTTPErrorResponse(ErrorResponse httpErrorResponse)
        {
            if (this.ResponseStatus == null)
            {
                this.ResponseStatus = new ResponseStatus();
            }
            this.ResponseStatus.HTTPErrorResponse = httpErrorResponse;
        }

        internal void Fail(Exception e)
        {
            this.ResponseStatus = new ResponseStatus(e);
        }
    }

    [DataContract]
    public class ProjectResponse : Response
    {
        [DataMember(Name = "data")]
        public Project Data { get; set; }
    }

    [DataContract]
    public class BuyProjectResponse : Response
    {
        [DataMember(Name = "data")]
        public BuyProjectLineItem[] Data { get; set; }
    }

    [DataContract]
    public class GetAllProjectsResponse : Response
    {
        [DataMember(Name = "data")]
        public ProjectHeader[] Data { get; set; }
    }

    [DataContract]
    public class GetAllEventsResponse : Response
    {
        [DataMember(Name = "data")]
        public Event[] Data { get; set; }
    }

    [DataContract]
    public class EventResponse : Response
    {
        [DataMember(Name = "data")]
        public Event Data { get; set; }
    }

    [DataContract]
    public class CreateProjectEventResponse : Response
    {
        [DataMember(Name = "data")]
        public Webhook Data { get; set; }
    }

    [DataContract]
    public class RepriceEventResponse : Response
    {
        [DataMember(Name = "data")]
        public Reprice Data { get; set; }
    }

    [DataContract]
    public class StateChangeEventResponse : Response
    {
        [DataMember(Name = "data")]
        public Webhook Data { get; set; }
    }

    [DataContract]
    public class ProjectReportResponse : Response
    {
        [DataMember(Name = "data")]
        public ProjectReport Data { get; set; }
    }

    [DataContract]
    public class DetailedProjectReportResponse : Response
    {
        [DataMember(Name = "data")]
        public DetailedProjectReport Data { get; set; }
    }

    [DataContract]
    public class DetailedLineItemReportResponse : Response
    {
        [DataMember(Name = "data")]
        public DetailedLineItemReport Data { get; set; }
    }

    [DataContract]
    public class QuotaCellReponse : Response
    {
        [DataMember(Name = "data")]
        public QuotaCell Data { get; set; }
    }

    [DataContract]
    public class EndLinksResponse : Response
    {
        [DataMember(Name = "data")]
        public ProjectSurveyEndLinks Data { get; set; }
    }

    public class CloseProjectResponse : Response
    {
        [DataMember(Name = "data")]
        public Item Data { get; set; }

        [DataContract]
        public class Item : ProjectHeader
        {
            [DataMember(Name = "lineItems")]
            public LineItemHeader[] LineItems { get; set; }
        }
    }

    [DataContract]
    public class LineItemResponse : Response
    {
        [DataMember(Name = "data")]
        public LineItem Data { get; set; }
    }

    [DataContract]
    public class UpdateLineItemStateResponse : Response
    {
        [DataMember(Name = "data")]
        public LineItemHeader Data { get; set; }
    }

    [DataContract]
    public class GetAllLineItemsResponse : Response
    {
        [DataMember(Name = "data")]
        public ListItem[] Data { get; set; }

        [DataContract]
        public class ListItem : LineItemHeader
        {
            [DataMember(Name = "title")]
            public string Title { get; set; }
            [DataMember(Name = "countryISOCode")]
            public string CountryISOCode { get; set; }
            [DataMember(Name = "languageISOCode")]
            public string LanguageISOCode { get; set; }
        }
    }

    [DataContract]
    public class GetFeasibilityResponse : Response
    {
        [DataMember(Name = "data")]
        public ListItem[] Data { get; set; }

        [DataContract]
        public class ListItem
        {
            [DataMember(Name = "extLineItemId")]
            public string ExtLineItemID { get; set; }
            [DataMember(Name = "feasibility")]
            public Feasibility Feasibility { get; set; }
            [DataMember(Name = "quote")]
            public Quote Quote { get; set; }
        }
    }

    [DataContract]
    public class GetCountriesResponse : Response
    {
        [DataMember(Name = "data")]
        public Country[] Data { get; set; }
    }

    [DataContract]
    public class GetSourcesResponse : Response
    {
        [DataMember(Name = "data")]
        public Source[] Data { get; set; }
    }

    [DataContract]
    public class GetAttributesResponse : Response
    {
        [DataMember(Name = "data")]
        public Attribute[] Data { get; set; }
    }

    [DataContract]
    public class GetUserInfoResponse : Response
    {
        [DataMember(Name = "data")]
        public UserInfo Data { get; set; }
    }

    [DataContract]
    public class GetCompanyUsersResponse : Response
    {
        [DataMember(Name = "data")]
        public CompanyUsers[] Data { get; set; }
    }

    [DataContract]
    public class GetCompanyTeamsResponse : Response
    {
        [DataMember(Name = "data")]
        public Team[] Data { get; set; }
    }

    [DataContract]
    public class GetRolesResponse : Response
    {
        [DataMember(Name = "data")]
        public Roles[] Data { get; set; }
    }

    [DataContract]
    public class GetProjectPermissionsResponse : Response
    {
        [DataMember(Name = "data")]
        public ProjectPermissions Data { get; set; }
    }

    [DataContract]
    public class UpsertProjectPermissionsResponse : Response
    {
        [DataMember(Name = "data")]
        public ProjectPermissions Data { get; set; }
    }

    [DataContract]
    public class TemplateResponse : Response
    {
        [DataMember(Name = "data")]
        public TemplateData Data { get; set; }
    }

    [DataContract]
    public class TemplateListResponse : Response
    {
        [DataMember(Name = "data")]
        public TemplateData[] Data { get; set; }
    }

    [DataContract]
    public class TemplateDeleteResponse : Response
    {
        [DataMember(Name = "data")]
        public string Data { get; set; }
    }


    [DataContract]
    public class TemplateData: TemplateCriteria
    {
        [DataMember(Name = "id")]
        public int ID { get; set; }
        [DataMember(Name = "editable")]
        public bool Editable { get; set; }
        [DataMember(Name = "createdAt")]
        public string CreatedAt { get; set; }
        [DataMember(Name = "updatedAt")]
        public string UpdatedAt { get; set; }
        [DataMember(Name = "state")]
        public string State { get; set; }
    }

    [DataContract]
    public class GetSurveyTopicsResponse : Response
    {
        [DataMember(Name = "data")]
        public SurveyTopic[] Data { get; set; }
    }

    [DataContract]
    public class GetStudyMetadataResponse : Response
    {
        [DataMember(Name = "data")]
        public StudyMetadata Data { get; set; }
    }

    [DataContract]
    public class RawResponse: Response
    {
        public byte[] Data { get; set; }
        public string RequestID { get; set; }
    }

    [DataContract]
    public class GetInvoicesSummaryResponse : RawResponse
    {
    
    }

    // ResponseStatus is the custom status part in API response.
    [DataContract]
    public class ResponseStatus
    {
        [IgnoreDataMember]
        private string RawMessage;

        internal ResponseStatus() { }
        internal ResponseStatus(Exception e)
        {
            this.Message = StatusTypeConstants.StatusTypeFail;
            this.Errors = new ErrorInfo[] { new ErrorInfo("-1", e.Message) };
        }

        [DataMember(Name = "errors")]
        public ErrorInfo[] Errors { get; set; }
        [IgnoreDataMember]
        public ErrorResponse HTTPErrorResponse { get; set; }

        // Reads "message" from API's custom success/error response and interprets the status as defined by StatusTypeConstants
        [DataMember(Name = "message")]
        public string Message
        {
            get
            {
                switch (this.RawMessage)
                {
                    case "success":
                        return StatusTypeConstants.StatusTypeSuccess;
                    case "fail":
                        return StatusTypeConstants.StatusTypeFail;
                    default:
                        return StatusTypeConstants.StatusTypeUnknown;
                }
            }
            set
            {
                this.RawMessage = value;
            }
        }

        [DataContract]
        public class ErrorInfo
        {
            [DataMember(Name = "code")]
            public string Code { get; set; }
            [DataMember(Name = "message")]
            public string Message { get; set; }

            internal ErrorInfo() { }
            internal ErrorInfo(string code, string message)
            {
                this.Code = code;
                this.Message = message;
            }
        }
    }

    [DataContract]
    public class Meta
    {
        // Links for page navigation
        [DataMember(Name = "links")]
        public LinkItems Links { get; set; }
        [DataMember(Name = "pageSize")]
        public int PageSize { get; set; }
        [DataMember(Name = "total")]
        public int Total { get; set; }


        [DataContract]
        public class LinkItems
        {
            [DataMember(Name = "first")]
            public string First { get; set; }
            [DataMember(Name = "last")]
            public string Last { get; set; }
            [DataMember(Name = "next")]
            public string Next { get; set; }
            [DataMember(Name = "prev")]
            public string Prev { get; set; }
            [DataMember(Name = "self")]
            public string Self { get; set; }
        }
    }
}
