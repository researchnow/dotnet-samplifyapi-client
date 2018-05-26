using System;
using System.Net.Http;
using System.Threading.Tasks;

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

        public async Task<ProjectResponse> CreateProject(CreateUpdateProjectCriteria project)
        {
            return await this.RequestAndParseResponse<ProjectResponse>(HttpMethod.Post, "/projects", project).ConfigureAwait(false);
        }

        public async Task<ProjectResponse> UpdateProject(CreateUpdateProjectCriteria project)
        {
            string path = string.Format("/projects/{0}", project.ExtProjectID);
            return await this.RequestAndParseResponse<ProjectResponse>(HttpMethod.Post, path, project).ConfigureAwait(false);
        }

        public async Task<BuyProjectResponse> BuyProject(string extProjectID, BuyProjectCriteria[] buy)
        {
            string path = string.Format("/projects/{0}/buy", extProjectID);
            return await this.RequestAndParseResponse<BuyProjectResponse>(HttpMethod.Post, path, buy).ConfigureAwait(false);
        }

        public async Task<CloseProjectResponse> CloseProject(string extProjectID)
        {
            string path = string.Format("/projects/{0}/buy", extProjectID);
            return await this.RequestAndParseResponse<CloseProjectResponse>(HttpMethod.Post, path, null).ConfigureAwait(false);
        }

        public async Task<GetAllProjectsResponse> GetAllProjects(QueryOptions options)
        {
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/projects{0}", query);
            return await this.RequestAndParseResponse<GetAllProjectsResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<ProjectResponse> GetProjectBy(string extProjectID)
        {
            string path = string.Format("/projects/{0}", extProjectID);
            return await this.RequestAndParseResponse<ProjectResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<ProjectReportResponse> GetProjectReport(string extProjectID)
        {
            string path = string.Format("/projects/{0}/report", extProjectID);
            return await this.RequestAndParseResponse<ProjectReportResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<LineItemResponse> AddLineItem(string extProjectID, LineItemCriteria lineItem)
        {
            string path = string.Format("/projects/{0}/lineItems", extProjectID);
            return await this.RequestAndParseResponse<LineItemResponse>(HttpMethod.Post, path, lineItem).ConfigureAwait(false);
        }

        public async Task<LineItemResponse> UpdateLineItem(string extProjectID, string extLineItemID, LineItemCriteria lineItem)
        {
            string path = string.Format("/projects/{0}/lineItems/{1}", extProjectID, extLineItemID);
            return await this.RequestAndParseResponse<LineItemResponse>(HttpMethod.Post, path, lineItem).ConfigureAwait(false);
        }

        public async Task<UpdateLineItemStateResponse> UpdateLineItemState(string extProjectID, string extLineItemID, string action)
        {
            string path = string.Format("/projects/{0}/lineItems/{1}/{2}", extProjectID, extLineItemID, action);
            return await this.RequestAndParseResponse<UpdateLineItemStateResponse>(HttpMethod.Post, path, null).ConfigureAwait(false);
        }

        public async Task<GetAllLineItemsResponse> GetAllLineItems(string extProjectID, QueryOptions options)
        {
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/projects/{0}/lineItems{1}", extProjectID, query);
            return await this.RequestAndParseResponse<GetAllLineItemsResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<LineItemResponse> GetLineItemBy(string extProjectID, string extLineItemID)
        {
            string path = string.Format("/projects/{0}/lineItems/{1}", extProjectID, extLineItemID);
            return await this.RequestAndParseResponse<LineItemResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<GetFeasibilityResponse> GetFeasibility(string extProjectID, QueryOptions options)
        {
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/projects/{0}/feasibility{1}", extProjectID, query);
            return await this.RequestAndParseResponse<GetFeasibilityResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<GetCountriesResponse> GetCountries(QueryOptions options)
        {
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/countries{0}", query);
            return await this.RequestAndParseResponse<GetCountriesResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<GetAttributesResponse> GetAttributes(string countryCode, string languageCode, QueryOptions options)
        {
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/attributes/{0}/{1}{2}", countryCode, languageCode, query);
            return await this.RequestAndParseResponse<GetAttributesResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<GetSurveyTopicsResponse> GetSurveyTopics(QueryOptions options)
        {
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/categories/surveyTopics{0}", query);
            return await this.RequestAndParseResponse<GetSurveyTopicsResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<TokenResponse> GetAuth()
        {
            var r = await Request.Send(this.AuthURL, HttpMethod.Post, "", "", Credentials);
            return (TokenResponse)Util.Deserialize(r.Body, typeof(TokenResponse));
        }

        private async Task<T> RequestAndParseResponse<T>(HttpMethod method, string url, object body) where T : Response, new()
        {
            T response = new T();
            APIResponse api;
            try
            {
                api = await this.Fetch(method, url, body).ConfigureAwait(false);
            }
            catch (Exception e)
            {
                response.Fail(e);
                return response;
            }
            if (api.HasError)
            {
                try
                {
                    response = (T)Util.Deserialize(api.Body, response.GetType());
                }
                catch (Exception e)
                {
                    response.Fail(e);
                }
                response.SetHTTPErrorResponse(api.Error);
                return response;
            }
            try
            {
                response = (T)Util.Deserialize(api.Body, response.GetType());
                return response;
            }
            catch (Exception e)
            {
                response.Fail(e);
                return response;
            }
        }

        private async Task<APIResponse> Fetch(HttpMethod method, string url, object body)
        {
            if (this.Auth.AccessTokenExpired)
            {
                await this.RequestAndParseToken().ConfigureAwait(false);
            }
            try
            {
                var ar = await Request.Send(this.APIBaseURL, method, url, this.Auth.AccessToken, body).ConfigureAwait(false);
                if (ar.Unauthorized)
                {
                    await this.RequestAndParseToken().ConfigureAwait(false);
                    return await Request.Send(this.APIBaseURL, method, url, this.Auth.AccessToken, body).ConfigureAwait(false);
                }
                return ar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private async Task RequestAndParseToken()
        {
            var t = DateTime.Now;
            try
            {
                var ar = await Request.Send(this.AuthURL, HttpMethod.Post, "", "", Credentials).ConfigureAwait(false);
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
