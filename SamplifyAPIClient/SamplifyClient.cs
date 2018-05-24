using System;
using System.Net.Http;

namespace ResearchNow.SamplifyAPIClient
{
    public enum SamplifyEnv
    {
        UAT = 0, //Use uat environment
        Prod = 1, //Use prod environment
        CheckProcessEnv = 3, //Check process environment variable 'env' to determine. (default = dev)
    }

    public class SamplifyClient
    {
        public TokenResponse Auth { get; set; }

        private readonly string APIBaseURL;
        private readonly string AuthURL;
        private readonly Request Request;
        private readonly TokenRequest Credentials;

        public SamplifyClient(string clientID, string username, string password, SamplifyEnv env)
        {
            this.APIBaseURL = HostConstants.UATAPIBaseURL;
            this.AuthURL = HostConstants.UATAuthURL;
            if (this.IsProdEnvironment(env))
            {
                this.APIBaseURL = HostConstants.ProdAPIBaseURL;
                this.AuthURL = HostConstants.ProdAuthURL;
            }
            this.Credentials = new TokenRequest(clientID, username, password);
            this.Request = new Request();
            this.Auth = new TokenResponse();
        }

        public ProjectResponse CreateProject(CreateUpdateProjectCriteria project)
        {
            Response res = new ProjectResponse();
            this.RequestAndParseResponse(HttpMethod.Post, "/projects", project, ref res);
            return (ProjectResponse)res;
        }

        public ProjectResponse UpdateProject(CreateUpdateProjectCriteria project)
        {
            Response res = new ProjectResponse();
            string path = string.Format("/projects/{0}", project.ExtProjectID);
            this.RequestAndParseResponse(HttpMethod.Post, path, project, ref res);
            return (ProjectResponse)res;
        }

        public BuyProjectResponse BuyProject(string extProjectID, BuyProjectCriteria[] buy)
        {
            Response res = new BuyProjectResponse();
            string path = string.Format("/projects/{0}/buy", extProjectID);
            this.RequestAndParseResponse(HttpMethod.Post, path, buy, ref res);
            return (BuyProjectResponse)res;
        }

        public CloseProjectResponse CloseProject(string extProjectID)
        {
            Response res = new CloseProjectResponse();
            string path = string.Format("/projects/{0}/buy", extProjectID);
            this.RequestAndParseResponse(HttpMethod.Post, path, null, ref res);
            return (CloseProjectResponse)res;
        }

        public GetAllProjectsResponse GetAllProjects(QueryOptions options)
        {
            Response res = new GetAllProjectsResponse();
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/projects{0}", query);
            this.RequestAndParseResponse(HttpMethod.Get, path, null, ref res);
            return (GetAllProjectsResponse)res;
        }

        public ProjectResponse GetProjectBy(string extProjectID)
        {
            Response res = new ProjectResponse();
            string path = string.Format("/projects/{0}", extProjectID);
            this.RequestAndParseResponse(HttpMethod.Get, path, null, ref res);
            return (ProjectResponse)res;
        }

        public ProjectReportResponse GetProjectReport(string extProjectID)
        {
            Response res = new ProjectReportResponse();
            string path = string.Format("/projects/{0}/report", extProjectID);
            this.RequestAndParseResponse(HttpMethod.Get, path, null, ref res);
            return (ProjectReportResponse)res;
        }

        public LineItemResponse AddLineItem(string extProjectID, LineItemCriteria lineItem)
        {
            Response res = new LineItemResponse();
            string path = string.Format("/projects/{0}/lineItems", extProjectID);
            this.RequestAndParseResponse(HttpMethod.Post, path, lineItem, ref res);
            return (LineItemResponse)res;
        }

        public LineItemResponse UpdateLineItem(string extProjectID, string extLineItemID, LineItemCriteria lineItem)
        {
            Response res = new LineItemResponse();
            string path = string.Format("/projects/{0}/lineItems/{1}", extProjectID, extLineItemID);
            this.RequestAndParseResponse(HttpMethod.Post, path, lineItem, ref res);
            return (LineItemResponse)res;
        }

        public UpdateLineItemStateResponse UpdateLineItemState(string extProjectID, string extLineItemID, string action)
        {
            Response res = new UpdateLineItemStateResponse();
            string path = string.Format("/projects/{0}/lineItems/{1}/{2}", extProjectID, extLineItemID, action);
            this.RequestAndParseResponse(HttpMethod.Post, path, null, ref res);
            return (UpdateLineItemStateResponse)res;
        }

        public GetAllLineItemsResponse GetAllLineItems(string extProjectID, QueryOptions options)
        {
            Response res = new GetAllLineItemsResponse();
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/projects/{0}/lineItems{1}", extProjectID, query);
            this.RequestAndParseResponse(HttpMethod.Get, path, null, ref res);
            return (GetAllLineItemsResponse)res;
        }

        public LineItemResponse GetLineItemBy(string extProjectID, string extLineItemID)
        {
            Response res = new LineItemResponse();
            string path = string.Format("/projects/{0}/lineItems/{1}", extProjectID, extLineItemID);
            this.RequestAndParseResponse(HttpMethod.Get, path, null, ref res);
            return (LineItemResponse)res;
        }

        public GetFeasibilityResponse GetFeasibility(string extProjectID, QueryOptions options)
        {
            Response res = new GetFeasibilityResponse();
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/projects/{0}/feasibility{1}", extProjectID, query);
            this.RequestAndParseResponse(HttpMethod.Get, path, null, ref res);
            return (GetFeasibilityResponse)res;
        }

        public GetCountriesResponse GetCountries(QueryOptions options)
        {
            Response res = new GetCountriesResponse();
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/countries{0}", query);
            this.RequestAndParseResponse(HttpMethod.Get, path, null, ref res);
            return (GetCountriesResponse)res;
        }

        public GetAttributesResponse GetAttributes(string countryCode, string languageCode, QueryOptions options)
        {
            Response res = new GetAttributesResponse();
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/attributes/{0}/{1}{2}", countryCode, languageCode, query);
            this.RequestAndParseResponse(HttpMethod.Get, path, null, ref res);
            return (GetAttributesResponse)res;
        }

        public GetSurveyTopicsResponse GetSurveyTopics(QueryOptions options)
        {
            Response res = new GetSurveyTopicsResponse();
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/categories/surveyTopics{0}", query);
            this.RequestAndParseResponse(HttpMethod.Get, path, null, ref res);
            return (GetSurveyTopicsResponse)res;
        }

        public TokenResponse GetAuth()
        {
            var r = Request.Send(this.AuthURL, HttpMethod.Post, "", "", Credentials).Result;
            return (TokenResponse)Util.Deserialize(r.Body, typeof(TokenResponse));
        }

        private void RequestAndParseResponse(HttpMethod method, string url, object body, ref Response response)
        {
            APIResponse api;
            try
            {
                api = this.Fetch(method, url, body);
            }
            catch (Exception e)
            {
                response.Fail(e);
                return;
            }
            if (api.HasError)
            {
                try
                {
                    response = (Response)Util.Deserialize(api.Body, response.GetType());
                }
                catch (Exception e)
                {
                    response.Fail(e);
                }
                response.SetHTTPErrorResponse(api.Error);
                return;
            }
            try
            {
                response = (Response)Util.Deserialize(api.Body, response.GetType());
            }
            catch (Exception e)
            {
                response.Fail(e);
                return;
            }
        }

        private APIResponse Fetch(HttpMethod method, string url, object body)
        {
            if (this.Auth.AccessTokenExpired)
            {
                this.RequestAndParseToken();
            }
            try
            {
                var ar = Request.Send(this.APIBaseURL, method, url, this.Auth.AccessToken, body).Result;
                if (ar.Unauthorized)
                {
                    this.RequestAndParseToken();
                    return Request.Send(this.APIBaseURL, method, url, this.Auth.AccessToken, body).Result;
                }
                return ar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private void RequestAndParseToken()
        {
            var t = DateTime.Now;
            try
            {
                var ar = Request.Send(this.AuthURL, HttpMethod.Post, "", "", Credentials).Result;
                this.Auth = (TokenResponse)Util.Deserialize(ar.Body, typeof(TokenResponse));
            }
            catch (Exception e)
            {
                this.Auth = new TokenResponse();
                throw e;
            }
            this.Auth.Acquired = t;
        }

        private bool IsProdEnvironment(SamplifyEnv env)
        {
            if (env == SamplifyEnv.Prod)
            {
                return true;
            }
            if (env == SamplifyEnv.CheckProcessEnv)
            {
                try
                {
                    string val = Environment.GetEnvironmentVariable("env");
                    if (val == "prod")
                    {
                        return true;
                    }
                    return false;
                }
                catch { return false; }
            }
            return false;
        }

        internal static class HostConstants
        {
            internal const string ProdAuthURL = "https://api.researchnow.com/auth/v1/token/password";
            internal const string ProdAPIBaseURL = "https://api.researchnow.com/sample/v1";
            internal const string UATAuthURL = "https://api.uat.pe.researchnow.com/auth/v1/token/password";
            internal const string UATAPIBaseURL = "https://api.uat.pe.researchnow.com/sample/v1";
        }
    }
}
