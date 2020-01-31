using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text;

namespace Dynata.SamplifyAPIClient
{
    public enum SamplifyEnv
    {
        UAT = 0, //Use uat environment
        Prod = 1, //Use prod environment
        DEV = 2, // Use dev environment
        CheckProcessEnv = 3, //Check process environment variable 'env' to determine. (default = uat)
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
            this.AuthURL = HostConstants.UATAuthBaseURL;
            if (this.IsProdEnvironment(env))
            {
                this.APIBaseURL = HostConstants.ProdAPIBaseURL;
                this.AuthURL = HostConstants.ProdAuthBaseURL;
            } else if (SamplifyEnv.DEV == env)
            {
                this.APIBaseURL = HostConstants.DEVAPIBaseURL;
                this.AuthURL = HostConstants.DEVAuthBaseURL;
            }
         
            this.Credentials = new TokenRequest(clientID, username, password);
            this.Request = new Request();
            this.Auth = new TokenResponse();
        }

        // Used for unit testing
        public SamplifyClient(HttpClient testClient)
        {
            this.APIBaseURL = HostConstants.UnitTextAPIBaseURL;
            this.AuthURL = HostConstants.UnitTextAuthURL;
            this.Credentials = new TokenRequest("", "", "");
            this.Request = new Request(testClient);
            this.Auth = new TokenResponse();
        }

        // Attributes
        public async Task<GetAttributesResponse> GetAttributes(string countryCode, string languageCode, QueryOptions options)
        {
            Validator.IsNonEmptyString(countryCode, languageCode);
            Validator.IsCountryCodeOrNull(countryCode);
            Validator.IsLanguageCodeOrNull(languageCode);

            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/attributes/{0}/{1}{2}", countryCode, languageCode, query);
            return await this.RequestAndParseResponse<GetAttributesResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        // Categories
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

        // Countries
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

        // Events
        public async Task<GetAllEventsResponse> GetAllEvents(QueryOptions options)
        {
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/events{0}", query);
            return await this.RequestAndParseResponse<GetAllEventsResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<CreateProjectEventResponse> CreateProjectEvent(CreateProjectEventCriteria createProjectEvent)
        {
            Validator.IsNotNull(createProjectEvent);
            Validator.Validate(createProjectEvent);
            return await this.RequestAndParseResponse<CreateProjectEventResponse>(HttpMethod.Post, "/events/lineItems/createproject", createProjectEvent).ConfigureAwait(false);
        }

        public async Task<RepriceEventResponse> RepriceEvent(RepriceEventCriteria repriceEvent)
        {
            Validator.IsNotNull(repriceEvent);
            Validator.Validate(repriceEvent);
            return await this.RequestAndParseResponse<RepriceEventResponse>(HttpMethod.Post, "/events/lineItems/reprice", repriceEvent).ConfigureAwait(false);
        }

        public async Task<StateChangeEventResponse> StateChangeEvent(StateChangeEventCriteria stateChangeEvent)
        {
            Validator.IsNotNull(stateChangeEvent);
            Validator.Validate(stateChangeEvent);
            return await this.RequestAndParseResponse<StateChangeEventResponse>(HttpMethod.Post, "/events/lineItems/stateChange", stateChangeEvent).ConfigureAwait(false);
        }

        public async Task<EventResponse> GetEventBy(int eventID)
        {
            string path = string.Format("/events/{0}", eventID);
            return await this.RequestAndParseResponse<EventResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<EventResponse> AcceptEvent(int eventID)
        {
            string path = string.Format("/events/{0}/accept", eventID);
            return await this.RequestAndParseResponse<EventResponse>(HttpMethod.Post, path, null).ConfigureAwait(false);
        }

        public async Task<EventResponse> RejectEvent(int eventID)
        {
            string path = string.Format("/events/{0}/reject", eventID);
            return await this.RequestAndParseResponse<EventResponse>(HttpMethod.Post, path, null).ConfigureAwait(false);
        }

        // Projects
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

        public async Task<ProjectResponse> CreateProject(ProjectCriteria project)
        {
            Validator.IsNotNull(project);
            Validator.Validate(project);
            return await this.RequestAndParseResponse<ProjectResponse>(HttpMethod.Post, "/projects", project).ConfigureAwait(false);
        }

        public async Task<ProjectResponse> GetProjectBy(string extProjectID)
        {
            Validator.IsNonEmptyString(extProjectID);
            string path = string.Format("/projects/{0}", extProjectID);
            return await this.RequestAndParseResponse<ProjectResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<ProjectResponse> UpdateProject(ProjectCriteria project)
        {
            Validator.IsNotNull(project);
            Validator.IsNonEmptyString(project.ExtProjectID);
            Validator.IsEmail(project.NotificationEmails);
            Validator.IsDeviceType(project.Devices);

            string path = string.Format("/projects/{0}", project.ExtProjectID);
            return await this.RequestAndParseResponse<ProjectResponse>(HttpMethod.Post, path, project).ConfigureAwait(false);
        }

        public async Task<BuyProjectResponse> BuyProject(string extProjectID, BuyProjectCriteria[] buy)
        {
            Validator.IsNonEmptyString(extProjectID);
            Validator.IsNotNull(buy);
            Validator.ValidateAll(buy);

            string path = string.Format("/projects/{0}/buy", extProjectID);
            return await this.RequestAndParseResponse<BuyProjectResponse>(HttpMethod.Post, path, buy).ConfigureAwait(false);
        }

        public async Task<CloseProjectResponse> CloseProject(string extProjectID)
        {
            Validator.IsNonEmptyString(extProjectID);
            string path = string.Format("/projects/{0}/close", extProjectID);
            return await this.RequestAndParseResponse<CloseProjectResponse>(HttpMethod.Post, path, null).ConfigureAwait(false);
        }

        public async Task<GetInvoicesSummaryResponse> GetInvoicesSummary(QueryOptions options)
        {
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/projects/invoices/summary{0}", query);

            APIResponseRaw apiRaw = (APIResponseRaw)await this.Fetch(HttpMethod.Get, path, null).ConfigureAwait(false);

            GetInvoicesSummaryResponse response = new GetInvoicesSummaryResponse
            {
                RequestID = apiRaw.RequestID,
                Data = apiRaw.BodyRaw
            };

            return response;
        }


        // TODO - Reconcile

        public async Task<ProjectReportResponse> GetProjectReport(string extProjectID)
        {
            Validator.IsNonEmptyString(extProjectID);
            string path = string.Format("/projects/{0}/report", extProjectID);
            return await this.RequestAndParseResponse<ProjectReportResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<EndLinksResponse> GetEndLinksBy(string extProjectID, string surveyID)
        {
            Validator.IsNonEmptyString(extProjectID, surveyID);
            string path = string.Format("/projects/{0}/surveys/{1}", extProjectID, surveyID);
            return await this.RequestAndParseResponse<EndLinksResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        // Feasibility
        // TODO - Update this
        public async Task<GetFeasibilityResponse> GetFeasibility(string extProjectID)
        {
            Validator.IsNonEmptyString(extProjectID);
            string path = string.Format("/projects/{0}/feasibility", extProjectID);
            return await this.RequestAndParseResponse<GetFeasibilityResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        // LineItems
        public async Task<GetAllLineItemsResponse> GetAllLineItems(string extProjectID, QueryOptions options)
        {
            Validator.IsNonEmptyString(extProjectID);
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/projects/{0}/lineItems{1}", extProjectID, query);
            return await this.RequestAndParseResponse<GetAllLineItemsResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<LineItemResponse> AddLineItem(string extProjectID, LineItemCriteria lineItem)
        {
            Validator.IsNonEmptyString(extProjectID);
            Validator.IsNotNull(lineItem);
            Validator.Validate(lineItem);

            string path = string.Format("/projects/{0}/lineItems", extProjectID);
            return await this.RequestAndParseResponse<LineItemResponse>(HttpMethod.Post, path, lineItem).ConfigureAwait(false);
        }

        public async Task<LineItemResponse> GetLineItemBy(string extProjectID, string extLineItemID)
        {
            Validator.IsNonEmptyString(extProjectID, extLineItemID);
            string path = string.Format("/projects/{0}/lineItems/{1}", extProjectID, extLineItemID);
            return await this.RequestAndParseResponse<LineItemResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        public async Task<LineItemResponse> UpdateLineItem(string extProjectID, string extLineItemID, LineItemCriteria lineItem)
        {
            Validator.IsNonEmptyString(extProjectID, extLineItemID);
            Validator.IsNotNull(lineItem);
            Validator.IsCountryCodeOrNull(lineItem.CountryISOCode);
            Validator.IsLanguageCodeOrNull(lineItem.LanguageISOCode);
            Validator.IsUrlOrNull(lineItem.SurveyURL);
            Validator.IsUrlOrNull(lineItem.SurveyTestURL);
            Validator.IsDeliveryTypeOrNull(lineItem.DeliveryType);

            string path = string.Format("/projects/{0}/lineItems/{1}", extProjectID, extLineItemID);
            return await this.RequestAndParseResponse<LineItemResponse>(HttpMethod.Post, path, lineItem).ConfigureAwait(false);
        }

        public async Task<UpdateLineItemStateResponse> UpdateLineItemState(string extProjectID, string extLineItemID, string action)
        {
            Validator.IsNonEmptyString(extProjectID, extLineItemID, action);
            Validator.IsActionOrNull(action);

            string path = string.Format("/projects/{0}/lineItems/{1}/{2}", extProjectID, extLineItemID, action);
            return await this.RequestAndParseResponse<UpdateLineItemStateResponse>(HttpMethod.Post, path, null).ConfigureAwait(false);
        }

        // SampleSources
        public async Task<GetSourcesResponse> GetSources(QueryOptions options)
        {
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/sources{0}", query);
            return await this.RequestAndParseResponse<GetSourcesResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        // Users
        public async Task<GetUserInfoResponse> GetUserInfo(QueryOptions options)
        {
            string query = "";
            if (options != null)
            {
                query = options.ToString();
            }
            string path = string.Format("/users/info{0}", query);
            return await this.RequestAndParseResponse<GetUserInfoResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        //GetDetailedProjectReport
        public async Task<DetailedProjectReportResponse> GetDetailedProjectReport(string extProjectID)
        {
            Validator.IsNonEmptyString(extProjectID);
            string path = string.Format("/projects/{0}/detailedReport", extProjectID);
            return await this.RequestAndParseResponse<DetailedProjectReportResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        //GetDetailedLineItemReport
        public async Task<DetailedLineItemReportResponse> GetDetailedLineItemReport(string extProjectID, string extLineItemID)
        {
            Validator.IsNonEmptyString(extProjectID);
            Validator.IsNonEmptyString(extLineItemID);
            string path = string.Format("/projects/{0}/lineItems/{1}/detailedReport", extProjectID, extLineItemID);
            return await this.RequestAndParseResponse<DetailedLineItemReportResponse>(HttpMethod.Get, path, null).ConfigureAwait(false);
        }

        //Auth
        public async Task<bool> RefreshToken()
        {
            if (this.Auth.AccessTokenExpired)
            {
                this.Auth = await this.GetAuth().ConfigureAwait(false);
                return !this.Auth.AccessTokenExpired;
            }
            var req = new LogoutRefreshRequest(this.Credentials.ClientID, "", this.Auth.RefreshToken);
            try
            {
                var t = DateTime.Now;
                var ar = await Request.Send(this.AuthURL, HttpMethod.Post, "/token/refresh", "", req).ConfigureAwait(false);
                this.Auth = (TokenResponse)Util.Deserialize(ar.Body, typeof(TokenResponse));
                this.Auth.Acquired = t;
                return !ar.HasError;
            }
            catch (Exception e)
            {
                this.Auth = new TokenResponse();
                throw e;
            }
        }

        public async Task<bool> Logout()
        {
            if (!this.Auth.AccessTokenExpired)
            {
                var req = new LogoutRefreshRequest(this.Credentials.ClientID, this.Auth.AccessToken, this.Auth.RefreshToken);
                var r = await Request.Send(this.AuthURL, HttpMethod.Post, "/logout", "", req).ConfigureAwait(false);
                return !r.HasError;
            }
            return true;
        }

        public async Task<TokenResponse> GetAuth()
        {
            var t = DateTime.Now;
            var r = await Request.Send(this.AuthURL, HttpMethod.Post, "/token/password", "", Credentials);
            var res = (TokenResponse)Util.Deserialize(r.Body, typeof(TokenResponse));
            res.Acquired = t;
            return res;
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
            var ar = await Request.Send(this.APIBaseURL, method, url, this.Auth.AccessToken, body).ConfigureAwait(false);
            if (ar.Unauthorized)
            {
                await this.RequestAndParseToken().ConfigureAwait(false);
                return await Request.Send(this.APIBaseURL, method, url, this.Auth.AccessToken, body).ConfigureAwait(false);
            }
            return ar;
        }

        private async Task RequestAndParseToken()
        {
            var t = DateTime.Now;
            try
            {
                var ar = await Request.Send(this.AuthURL, HttpMethod.Post, "/token/password", "", Credentials).ConfigureAwait(false);
                if (ar.HasError)
                {
                    throw new SamplifyAuthenticationException();
                }
                this.Auth = (TokenResponse)Util.Deserialize(ar.Body, typeof(TokenResponse));
            }
            catch
            {
                this.Auth = new TokenResponse();
                throw;
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
            // PROD
            internal const string ProdAuthBaseURL = "https://api.Dynata.com/auth/v1";
            internal const string ProdAPIBaseURL = "https://api.Dynata.com/sample/v1";

            // UAT
            internal const string UATAuthBaseURL = "https://api.uat.pe.Dynata.com/auth/v1";
            internal const string UATAPIBaseURL = "https://api.uat.pe.Dynata.com/sample/v1";

            // DEV
            internal const string DEVAPIBaseURL = "https://api.dev.pe.dynata.com/sample/v1";
            internal const string DEVAuthBaseURL = "https://api.dev.pe.dynata.com/auth/v1";

            internal const string UnitTextAPIBaseURL = "http://172.0.0.1";
            internal const string UnitTextAuthURL = "http://172.0.0.1/auth/v1/token/password";
        }
    }
}
