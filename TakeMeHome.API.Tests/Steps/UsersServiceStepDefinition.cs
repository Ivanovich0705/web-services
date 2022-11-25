using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using SpecFlow.Internal.Json;
using TakeMeHome.API.TakeMeHome.Resources;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace TakeMeHome.API.Tests.Steps;

[Binding]
public sealed class UserServiceStepDefinition: WebApplicationFactory<Program>
{
    // For additional details on SpecFlow step definitions see https://go.specflow.org/doc-stepdef

    private readonly WebApplicationFactory<Program> _factory;

    public UserServiceStepDefinition(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    
    private HttpClient Client { get; set; }
    private Uri BaseUri { get; set; }
    
    private Task<HttpResponseMessage> Response { get; set; }
    
    
    [Given(@"the endpoint https://localhost:(.*)/api/v(.*)/users is available")]
    public void GivenTheEndpointHttpsLocalhostApiVUsersIsAvailable(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/users");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
    }

    //@add-user
    [When(@"a POST request is sent to the endpoint with the following data")]
    public void WhenAPostRequestIsSentToTheEndpointWithTheFollowingData(Table saveUserResource)
    {
        var resource = saveUserResource.CreateSet<SaveUserResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = Client.PostAsync(BaseUri, content);
    }

    [Then(@"the response status code should be (.*)")]
    public void ThenTheResponseStatusCodeShouldBe(int expectedStatus)
    {
        var expectedStatusCode = ((HttpStatusCode)expectedStatus).ToString();
        var actualStatusCode = Response.Result.StatusCode.ToString();
        
        Assert.Equal(expectedStatusCode, actualStatusCode);
    }

    //get-users
    [When(@"a Get request is sent to the endpoint")]
    public void WhenAGetRequestIsSentToTheEndpoint()
    {
        Response = Client.GetAsync(BaseUri);
    }
    
    //get-users-by-user-id
    [When(@"a Get request is sent to the endpoint with user id (.*)")]
    public void WhenAGetRequestIsSentToTheEndpointWithTheFollowingData(int userId)
    {
        var uri = new Uri(BaseUri + $"/{userId}");
        Response = Client.GetAsync(uri);
    }

    //delete-user
    [When(@"a Delete request is sent to the endpoint with id (.*)")]
    public void WhenADeleteRequestIsSentToTheEndpointWithId(int id)
    {
        Response = Client.DeleteAsync($"{BaseUri}/{id}");
    }
    
    //update-user
    [When(@"a Put request is sent to the endpoint with id (.*) and the following data")]
    public void WhenAPutRequestIsSentToTheEndpointWithIdAndTheFollowingData(int userId, Table saveUserResource)
    {
        var resource = saveUserResource.CreateSet<SaveUserResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = Client.PutAsync($"{BaseUri}/{userId}", content);
    }
}