# Samplify API client

A .NET (Standard 2.0) client library to connect with researchnow/ssi demand api
<br /><a href="https://developers.researchnow.com/samplifyapi-docs" target="_blank">See complete API reference here</a>

### Prerequisites

* Account credentials to access researchnow/ssi demand api
* Or, a test account to explore the API on UAT server

## Usage examples

The host URLs are configured while creating the client connection, described in the next section.

Prod settings:
* Authentication endpoint: "https://api.researchnow.com/auth/v1/token/password"
* API base url: "https://api.researchnow.com/sample/v1"

UAT (default) settings:
* Authentication endpoint: "https://api.uat.pe.researchnow.com/auth/v1/token/password"
* API base url: "https://api.uat.pe.researchnow.com/sample/v1"

### Creating a client connection

The new client is initialized with production or UAT environment using `SamplifyEnv` parameter value in the constructor as described below:

* SamplifyEnv.UAT
* SamplifyEnv.Prod
* SamplifyEnv.CheckProcessEnv
    * Environment variable `env` could be set to "uat" or "prod". If none found, default "uat" value will be used.

```
SamplifyClient client = new SamplifyClient("client_id", "username", "password", SamplifyEnv.UAT);
```

The session expires after some time but the client will automatically acquire one by making an authentication request before sending out the actual request, again.

### Basic request structure

All the request functions are `async Task<T>` type and return their respective response objects with the Task. All those returned, response object types are derived from `ResearchNow.SamplifyAPIClient.Response` type.
The response returned, consists of:
* Requested data object.
* `HasError` property to determine if the request executed successfully.
* ResponseStatus, which consists of:
    * API custom status related to the request's execution.
    * `ErrorInfo` array, containing un-marshaled error data from API response.
    * Http `ErrorResponse` object, containing http error code, url etc. in case of any error.

Some of the response objects (such as those that return a list) also contain a "Meta" field.
* Meta, contains metadata such as page navigation links etc.

```
private async void btnGetAllProjects_Click(object sender, EventArgs e)
{
    var r = await client.GetAllProjects(null);
    if (!r.HasError)
    {
        foreach (var p in r.Projects)
        {
            // ...
        }
    }
}
```

In the above example, the `await` operator is applied to `GetAllProjects` (async) method. To enable the `await` keyword, `btnGetAllProjects_Click` event handler is defined by using the `async` modifier.

## Filtering & Sorting

All client functions that take `QueryOptions` parameter, support filtering/sorting & pagination. Nested fields are not supported for filtering and sorting operations. Default `limit` value is set to 10 but values up to 50 are permitted.

```
var options = new QueryOptions(10, 5); //offset=10, limit=5

options.AddFilter("title", "Test Survey");
options.AddFilter("state", StateConstants.Provisioned);

options.AddSort("createdAt", SortDirection.Asc);

var r = await client.GetAllProjects(options);
```

If multiple sort objects are provided, the order in which they are added in the list, is followed.
Following is a list of filtering/sorting field values:
* id
* extProjectId
* extLineItemId
* createdAt
* updatedAt
* title
* name
* text
* type
* state
* stateReason
* stateLastUpdatedAt
* isoCode
* countryName
* countryISOCode
* languageISOCode
* launchedAt

## Supported API functions

* `public async Task<ProjectResponse> CreateProject(CreateUpdateProjectCriteria project)`
* `public async Task<ProjectResponse> UpdateProject(CreateUpdateProjectCriteria project)`
* `public async Task<BuyProjectResponse> BuyProject(string extProjectID, BuyProjectCriteria[] buy)`
* `public async Task<CloseProjectResponse> CloseProject(string extProjectID)`
* `public async Task<GetAllProjectsResponse> GetAllProjects(QueryOptions options)`
* `public async Task<ProjectResponse> GetProjectBy(string extProjectID)`
* `public async Task<ProjectReportResponse> GetProjectReport(string extProjectID)`
* `public async Task<LineItemResponse> AddLineItem(string extProjectID, LineItemCriteria lineItem)`
* `public async Task<LineItemResponse> UpdateLineItem(string extProjectID, string extLineItemID, LineItemCriteria lineItem)`
* `public async Task<UpdateLineItemStateResponse> UpdateLineItemState(string extProjectID, string extLineItemID, string action)`
* `public async Task<GetAllLineItemsResponse> GetAllLineItems(string extProjectID, QueryOptions options)`
* `public async Task<LineItemResponse> GetLineItemBy(string extProjectID, string extLineItemID)`
* `public async Task<GetFeasibilityResponse> GetFeasibility(string extProjectID, QueryOptions options)`
* `public async Task<GetCountriesResponse> GetCountries(QueryOptions options)`
* `public async Task<GetAttributesResponse> GetAttributes(string countryCode, string languageCode, QueryOptions options)`
* `public async Task<GetSurveyTopicsResponse> GetSurveyTopics(QueryOptions options)`
* `public async Task<bool> RefreshToken()`
* `public async Task<bool> Logout()`

## Versioning

### 1.0
Supports API functionalities, such as:
* Authentication, including automatic re-authentication on token expire.
* Project and Line Items related requests. Such as, create, update, get-all, get-by-id requests etc.
* Filtering & Sorting
* Pricing & Feasibility
* Data endpoint functions, serving attributes, categories and countries & languages data.

## Authors

* Maaz Nisar
