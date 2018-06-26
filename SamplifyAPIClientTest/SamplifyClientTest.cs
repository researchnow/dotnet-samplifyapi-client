using System.Collections.Generic;
using System.Net.Http;
using ResearchNow.SamplifyAPIClient;
using Xunit;

namespace SamplifyAPIClientTest
{
    public class SamplifyClientTest
    {
        [Fact]
        public void TestAuth()
        {
            const string testToken = "test-token";
            const string expectedToken = "Bearer test-token";
            HttpRequestMessage message = new HttpRequestMessage();
            SamplifyClient testClient = new MockSamplifyClient((request) =>
            {
                message = request;
            });
            testClient.Auth.AccessToken = testToken;
            testClient.GetAllProjects(null).Wait();
            Assert.NotNull(message);
            Assert.NotNull(message.Headers);
            Assert.NotNull(message.Headers.Authorization);
            Assert.Equal(message.Headers.Authorization.ToString(), expectedToken);
        }

        [Fact]
        public void TestEndpoints()
        {
            var messages = new List<HttpRequestMessage>();
            string[] tests = new string[]{
                "/projects",
                "/projects/project001",
                "/projects/buy-test/buy",
                "/projects/close-test/close",
                "/projects",
                "/projects/test-prj-id",
                "/projects/test-report-id/report",
                "/projects/test/lineItems",
                "/projects/test-prj-id/lineItems/test-lineitem-id",
                "/projects/test-prj-id/lineItems/test-lineitem-id/pause",
                "/projects/test-prj-id/lineItems",
                "/projects/test-prj-id/lineItems/test-lineitem-id",
                "/projects/test-prj-id/feasibility",
                "/countries",
                "/attributes/GB/en",
                "/categories/surveyTopics"};

            SamplifyClient testClient = new MockSamplifyClient((request) =>
            {
                messages.Add(request);
            });

            testClient.CreateProject(Helper.GetTestProject()).Wait();
            testClient.UpdateProject(new CreateUpdateProjectCriteria { ExtProjectID = "project001" }).Wait();
            testClient.BuyProject("buy-test", Helper.GetTestBuyProjectCriteria()).Wait();
            testClient.CloseProject("close-test").Wait();
            testClient.GetAllProjects(null).Wait();
            testClient.GetProjectBy("test-prj-id").Wait();
            testClient.GetProjectReport("test-report-id").Wait();
            testClient.AddLineItem("test", Helper.GetTestLineItem()).Wait();
            testClient.UpdateLineItem("test-prj-id", "test-lineitem-id", new LineItemCriteria()).Wait();
            testClient.UpdateLineItemState("test-prj-id", "test-lineitem-id", ActionConstants.ActionPaused).Wait();
            testClient.GetAllLineItems("test-prj-id", null).Wait();
            testClient.GetLineItemBy("test-prj-id", "test-lineitem-id").Wait();
            testClient.GetFeasibility("test-prj-id", null).Wait();
            testClient.GetCountries(null).Wait();
            testClient.GetAttributes("GB", "en", null).Wait();
            testClient.GetSurveyTopics(null).Wait();

            Assert.Equal<int>(messages.Count, tests.Length);
            for (int i = 0; i < tests.Length; i++)
            {
                Assert.Equal(tests[i], messages[i].RequestUri.PathAndQuery);
            }
        }

        [Fact]
        public void TestQueryString()
        {
            HttpRequestMessage message = new HttpRequestMessage();
            var optOne = new QueryOptions();
            var optTwo = new QueryOptions();
            var optThree = new QueryOptions();
            optOne.AddFilter("title", "Samplify Client Test");
            optOne.AddFilter("state", StateConstants.Provisioned);
            optTwo.AddSort("createdAt", SortDirection.Asc);
            optTwo.AddSort("extProjectId", SortDirection.Desc);
            optThree.AddFilter("title", "Samplify Client Test");
            optThree.AddFilter("state", StateConstants.Provisioned);
            optThree.AddSort("createdAt", SortDirection.Asc);
            optThree.AddSort("extProjectId", SortDirection.Desc);

            var tests = new Dictionary<QueryOptions, string>();
            tests.Add(optOne, "/projects?title=Samplify+Client+Test&amp;state=PROVISIONED");
            tests.Add(optTwo, "/projects?sort=createdAt:asc,extProjectId:desc");
            tests.Add(optThree, "/projects?title=Samplify+Client+Test&amp;state=PROVISIONED&amp;sort=createdAt:asc,extProjectId:desc");

            SamplifyClient testClient = new MockSamplifyClient((request) =>
            {
                message = request;
            });

            foreach (var t in tests)
            {
                testClient.GetAllProjects(t.Key).Wait();
                Assert.NotNull(message);
                Assert.Equal(message.RequestUri.PathAndQuery, t.Value);
            }
        }
    }
}
