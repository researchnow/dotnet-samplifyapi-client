using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Dynata.SamplifyAPIClient
{
    internal class Request
    {
        private HttpClient client;
        internal Request()
        {
            client = new HttpClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        internal Request(HttpClient httpClient)
        {
            this.client = httpClient;
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }
        internal async Task<APIResponse> Send(string host, HttpMethod method, string url, string accessToken, object body)
        {
 
            if (!string.IsNullOrEmpty(accessToken))
            {
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            }
            HttpRequestMessage msg = new HttpRequestMessage(method, string.Format("{0}{1}", host, url));
            string data = string.Empty;
            if (body != null)
            {
                data = Util.Serialize(body); 
                msg.Content = new StringContent(data, System.Text.Encoding.UTF8, "application/json");
            }
            var res = await client.SendAsync(msg).ConfigureAwait(false);

            var reqID = this.GetHeaderValue(res.Headers, "x-request-id");

            string json = await res.Content.ReadAsStringAsync().ConfigureAwait(false);
       
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

            // The REST call was successful.
            if (res.Content.Headers.ContentType.MediaType == "application/pdf")
            {
                byte[] rawMsg = await res.Content.ReadAsByteArrayAsync().ConfigureAwait(false);
                return new APIResponseRaw(reqID, rawMsg, null);
            }


            return new APIResponse(reqID, json, null);
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

    internal class APIResponseRaw: APIResponse
    {
        internal byte[] BodyRaw { get; }

        internal APIResponseRaw(string requestID, byte[] bodyRaw, ErrorResponse err) : base(requestID, null, err)
        {
            this.BodyRaw = bodyRaw;
        }

    }

}
