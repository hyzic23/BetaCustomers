using BetaCustomers.API.Config;
using BetaCustomers.API.Models;
using BetaCustomers.API.Services;
using BetaCustomers.UnitTests.Fixtures;
using BetaCustomers.UnitTests.Helpers;
using FluentAssertions;
using Microsoft.Extensions.Options;
using Moq;
using Moq.Protected;

namespace BetaCustomers.UnitTests.Systems.Services;

public class TestUsersService
{
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesHttpGetRequest()
    {
        //Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        
        //IOptions<MongoDbConfig> mongoDbConfig
        
        var endpoint = "https://example.com/users";
        var config = Options.Create(
            new UsersApiConfig
            {
                Endpoint = endpoint
            });

        var mongoDbConfig = Options.Create(
            new MongoDbConfig
            {
                
            });
        
        var sut = new UsersService(httpClient, config, mongoDbConfig);
        
        //Act
        await sut.GetAllUsers();
        
        //Assert
        handlerMock
            .Protected()
            .Verify(
                "SendAsync", 
                Times.Exactly(1), 
                ItExpr.Is<HttpRequestMessage>(req => req.Method == HttpMethod.Get),
                          ItExpr.IsAny<CancellationToken>());
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsers()
    {
        //Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        
        var endpoint = "https://example.com/users";
        var config = Options.Create(
            new UsersApiConfig
            {
                Endpoint = endpoint
            });
        var mongoDbConfig = Options.Create(
            new MongoDbConfig
            {
                
            });
        
        var sut = new UsersService(httpClient, config, mongoDbConfig);
        
        //Act
        var result = await sut.GetAllUsers();
        
        //Assert
        result.Should().BeOfType<List<User>>();
    }
    
    [Fact]
    public async Task GetAllUsers_WhenHits404_ReturnsEmptyListOfUsers()
    {
        //Arrange
        var handlerMock = MockHttpMessageHandler<User>.SetupReturn404();
        var httpClient = new HttpClient(handlerMock.Object);
        
        var endpoint = "https://example.com/users";
        var config = Options.Create(
            new UsersApiConfig
            {
                Endpoint = endpoint
            });
        var mongoDbConfig = Options.Create(
            new MongoDbConfig
            {
                
            });
        
        var sut = new UsersService(httpClient, config, mongoDbConfig);
        
        //Act
        var result = await sut.GetAllUsers();
        
        //Assert
        result.Count.Should().Be(0);
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_ReturnsListOfUsersOfExpectedSize()
    {
        //Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var handlerMock = MockHttpMessageHandler<User>.SetupBasicGetResourceList(expectedResponse);
        var httpClient = new HttpClient(handlerMock.Object);
        
        var endpoint = "https://example.com/users";
        var config = Options.Create(
            new UsersApiConfig
            {
                Endpoint = endpoint
            });
        var mongoDbConfig = Options.Create(
            new MongoDbConfig
            {
                
            });
        
        var sut = new UsersService(httpClient, config, mongoDbConfig);
        
        //Act
        var result = await sut.GetAllUsers();
        
        //Assert
        result.Count.Should().Be(expectedResponse.Count);
    }
    
    [Fact]
    public async Task GetAllUsers_WhenCalled_InvokesConfiguredExternalUrl()
    {
        //Arrange
        var expectedResponse = UsersFixture.GetTestUsers();
        var endpoint = "https://example.com/users";
        var handlerMock = MockHttpMessageHandler<User>
            .SetupBasicGetResourceList(expectedResponse, endpoint);
        
        var httpClient = new HttpClient(handlerMock.Object);

        var config = Options.Create(
            new UsersApiConfig
            {
                Endpoint = endpoint
            });
        var mongoDbConfig = Options.Create(
            new MongoDbConfig
            {
                
            });
        
        var sut = new UsersService(httpClient, config, mongoDbConfig);
        
        //Act
        var result = await sut.GetAllUsers();

        var uri = new Uri(endpoint);
        
        //Assert
        handlerMock
            .Protected()
            .Verify("SendAsync",
                Times.Exactly(1),
                ItExpr.Is<HttpRequestMessage>(
                    req => req.Method == HttpMethod.Get 
                           && req.RequestUri == uri),
                ItExpr.IsAny<CancellationToken>());
    }

    
}