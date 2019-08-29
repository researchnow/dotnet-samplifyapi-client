using System;
using System.Runtime.Serialization;

namespace ResearchNow.SamplifyAPIClient
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
        public Project Project { get; set; }
    }

    [DataContract]
    public class BuyProjectResponse : Response
    {
        [DataMember(Name = "data")]
        public BuyProjectLineItem[] List { get; set; }
    }

    [DataContract]
    public class GetAllProjectsResponse : Response
    {
        [DataMember(Name = "data")]
        public ProjectHeader[] Projects { get; set; }
    }

    [DataContract]
    public class ProjectReportResponse : Response
    {
        [DataMember(Name = "data")]
        public ProjectReport Report { get; set; }
    }

    public class CloseProjectResponse : Response
    {
        [DataMember(Name = "data")]
        public Item Project { get; set; }

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
        public LineItem Item { get; set; }
    }

    [DataContract]
    public class UpdateLineItemStateResponse : Response
    {
        [DataMember(Name = "data")]
        public LineItemHeader LineItem { get; set; }
    }

    [DataContract]
    public class GetAllLineItemsResponse : Response
    {
        [DataMember(Name = "data")]
        public ListItem[] List { get; set; }

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
        public ListItem[] List { get; set; }

        [DataContract]
        public class ListItem
        {
            [DataMember(Name = "extLineItemId")]
            public string ExtLineItemID { get; set; }
            [DataMember(Name = "feasibility")]
            public Feasibility Feasibility { get; set; }
        }
    }

    [DataContract]
    public class GetCountriesResponse : Response
    {
        [DataMember(Name = "data")]
        public Country[] List { get; set; }
    }

    [DataContract]
    public class GetAttributesResponse : Response
    {
        [DataMember(Name = "data")]
        public Attribute[] List { get; set; }
    }

    [DataContract]
    public class GetSurveyTopicsResponse : Response
    {
        [DataMember(Name = "data")]
        public SurveyTopic[] List { get; set; }
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
