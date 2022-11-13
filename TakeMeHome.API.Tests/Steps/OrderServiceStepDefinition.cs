using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using Microsoft.AspNetCore.JsonPatch;
using System.Net.Mime;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using SpecFlow.Internal.Json;
using TakeMeHome.API.TakeMeHome.Resources;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using TakeMeHome.API.TakeMeHome.Domain.Models;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;
using Xunit;

namespace TakeMeHome.API.Tests.Steps;

[Binding]
public class OrderServiceStepDefinition
{
    
    private readonly WebApplicationFactory<Program> _factory;

    public OrderServiceStepDefinition(WebApplicationFactory<Program> factory)
    {
        _factory = factory;
    }

    private HttpClient Client { get; set; }
    private Uri BaseUri { get; set; }
    
    private Task<HttpResponseMessage> Response { get; set; }

    [Given(@"the endpoint https://localhost:(.*)/api/v(.*)/orders is available")]
    public void GivenTheEndpointHttpsLocalhostApiVOrdersIsAvailable(int port, int version)
    {
        BaseUri = new Uri($"https://localhost:{port}/api/v{version}/orders");
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions { BaseAddress = BaseUri });
    }

    //@add-order
     [When(@"a POST request is sent to the endpoint with the following data")]
     public void WhenAPostRequestIsSentToTheEndpointWithTheFollowingData(Table saveOrderResource)
     {
         var resource = saveOrderResource.CreateSet<SaveOrderResource>().First();
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

    //get-orders
    [When(@"a GET request for orders is sent to the endpoint")]
    public void WhenAGetRequestForOrdersIsSentToTheEndpoint()
    {
        Response = Client.GetAsync(BaseUri);
    }
    
    //get-comments-by-status-id
     [When(@"a Get request is sent to the endpoint with status id (.*)")]
     public void WhenAGetRequestIsSentToTheEndpointWithTheFollowingData(int statusId)
     {
         var uri = new Uri(BaseUri + $"/status/{statusId}");
         Response = Client.GetAsync(uri);
    
     }
    
    //update-order
     [When(@"a Put request is sent to the endpoint with id (.*) and the following data")]
    public void WhenAPutRequestIsSentToTheEndpointWithIdAndTheFollowingData(int orderId, [FromBody] JsonPatchDocument<Order> givenResource)
     {
         var resource = saveCommentResource.CreateSet<SaveCommentResource>().First();
         var content = new StringContent(resource.ToJson(), Encoding.UTF8, MediaTypeNames.Application.Json);
         Response = Client.PutAsync($"{BaseUri}/{commentId}", content);
     }
    
}