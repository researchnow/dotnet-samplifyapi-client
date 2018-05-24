using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace ResearchNow.SamplifyAPIClient
{
    internal class Request
    {
        internal async Task<APIResponse> Send(string host, HttpMethod method, string url, string accessToken, object body)
        {
            var client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
            HttpRequestMessage msg = new HttpRequestMessage(method, string.Format("{0}{1}", host, url));
            string data = string.Empty;
            if (body != null)
            {
                data = Util.Serialize(body);
            }
            msg.Content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            try
            {
                var res = await client.SendAsync(msg);
                string json = await res.Content.ReadAsStringAsync();
                var reqID = this.GetHeaderValue(res.Headers, "x-request-id");
                if (!res.IsSuccessStatusCode)
                {
                    string errPath = string.Format("{0}{1}", host, url);
                    ErrorResponse err = new ErrorResponse();
                    err.Timestamp = DateTime.Now;
                    err.RequestID = reqID;
                    err.HTTPCode = (int)res.StatusCode;
                    err.HTTPPhrase = res.ReasonPhrase;
                    err.Path = errPath;

                    return new APIResponse(reqID, json, err);
                }
                return new APIResponse(reqID, json, null);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        private string GetHeaderValue(HttpResponseHeaders headers, string field)
        {
            IEnumerable<string> values;
            if (headers.TryGetValues(field, out values))
            {
                return values.GetEnumerator().Current;
            }
            return string.Empty;
        }
    }

    internal class APIResponse
    {
        internal string Body { get; }
        internal string RequestID { get; }
        internal ErrorResponse Error { get; }
        internal APIResponse(string requestID, string body, ErrorResponse err)
        {
            this.RequestID = requestID;
            this.Body = body;
            this.Error = err;
        }
        internal bool HasError => Error != null;
        internal bool Unauthorized => this.Error != null
                                        && this.Error.HTTPCode == (int)System.Net.HttpStatusCode.Unauthorized;
    }

}
