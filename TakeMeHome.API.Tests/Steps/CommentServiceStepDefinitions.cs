using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Mvc.Testing;
using SpecFlow.Internal.Json;
using TakeMeHome.API.TakeMeHome.Resources;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace TakeMeHome.API.Tests.Steps;

[Binding]
public class CommentServiceStepDefinitions : WebApplicationFactory<Program>
{
    private readonly WebApplicationFactory<Program> _factory;

    public CommentServiceStepDefinitions(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private HttpClient Client { get; set; }
    private Uri BaseUri { get; set; }
    
    private Task<HttpResponseMessage> Response { get; set; }

    [Given(@"the endpoint https://localhost:(.*)/api/v(.*)/comments is available")]
    public void GivenTheEndpointHttpsLocalhostApiVCommentsIsAvailable(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/comments");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
    }

    //@add-comment
    [When(@"a POST request is sent to the endpoint with the following data")]
    public void WhenAPostRequestIsSentToTheEndpointWithTheFollowingData(Table saveCommentResource)
    {
        var resource = saveCommentResource.CreateSet<SaveCommentResource>().First();
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

    //get-comments
    [When(@"a Get request is sent to the endpoint")]
    public void WhenAGetRequestIsSentToTheEndpoint()
    {
        Response = Client.GetAsync(BaseUri);
    }
    
    //get-comments-by-user-id
    [When(@"a Get request is sent to the endpoint with user id (.*)")]
    public void WhenAGetRequestIsSentToTheEndpointWithTheFollowingData(int userId)
    {
        var uri = new Uri(BaseUri + $"/{userId}");
        Response = Client.GetAsync(uri);
    }

    //delete-comments
    [When(@"a Delete request is sent to the endpoint with id (.*)")]
    public void WhenADeleteRequestIsSentToTheEndpointWithId(int id)
    {
        Response = Client.DeleteAsync($"{BaseUri}/{id}");
    }
    
    //update-comments
    [When(@"a Put request is sent to the endpoint with id (.*) and the following data")]
    public void WhenAPutRequestIsSentToTheEndpointWithIdAndTheFollowingData(int commentId, Table saveCommentResource)
    {
        var resource = saveCommentResource.CreateSet<SaveCommentResource>().First();
        var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
        Response = Client.PutAsync($"{BaseUri}/{commentId}", content);
    }
}
