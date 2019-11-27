using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Dynata.SamplifyAPIClient;

namespace SamplifyAPIClientTest
{
    public class MockSamplifyClient : SamplifyClient
    {
        public MockSamplifyClient(Action<HttpRequestMessage> onRequestReceived)
            : base(new HttpClient(new MockHandler(onRequestReceived)))
        {
            var auth = new TokenResponse();
            auth.AccessToken = "test-token";
            auth.Acquired = DateTime.Now;
            auth.ExpiresIn = 3600;
            base.Auth = auth;
        }

        private class MockHandler : HttpMessageHandler
        {
            private Action<HttpRequestMessage> Callback;

            public MockHandler(Action<HttpRequestMessage> onRequestReceived)
            {
                this.Callback = onRequestReceived;
            }

            protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
            {
                this.Callback(request);
                var res = new HttpResponseMessage(System.Net.HttpStatusCode.NotImplemented);
                res.Content = new StringContent("{}", System.Text.Encoding.UTF8, "application/json");
                return await Task.Run(() => res).ConfigureAwait(false);
            }
        }
    }
}
