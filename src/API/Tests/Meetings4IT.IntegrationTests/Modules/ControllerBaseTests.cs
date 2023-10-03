using AutoFixture;
using Meetings4IT.IntegrationTests.Setup;
using Meetings4IT.Shared.Implementations.Tools;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Moq.AutoMock;
using Newtonsoft.Json;
using System.Net.Http.Headers;

namespace Meetings4IT.IntegrationTests.Modules;

public abstract class ControllerBaseTests : IClassFixture<CustomWebApplicationFactory<Program>>
{
    protected readonly CustomWebApplicationFactory<Program> _factory;
    protected HttpClient Client;
    protected Fixture Fixture;
    protected AutoMocker Mocker;

    protected ControllerBaseTests(CustomWebApplicationFactory<Program> factory)
    {
        _factory = factory;
        Mocker = new AutoMocker();
        Fixture = new Fixture();
        Client = _factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    protected JsonSerializerSettings? SerializerSettings() => new JsonSerializerSettings
    {
        ContractResolver = new PrivateResolver(),
        ConstructorHandling = ConstructorHandling.AllowNonPublicDefaultConstructor,
        TypeNameHandling = TypeNameHandling.Auto,
        NullValueHandling = NullValueHandling.Ignore
    };

    protected async Task<HttpResponseMessage> ClientCall<TRequest>(TRequest? obj, HttpMethod methodType,
        string requestUri)
    {
        var request = new HttpRequestMessage(methodType, requestUri);
        if (obj != null)
        {
            var serializeObject = JsonConvert.SerializeObject(obj);
            request.Content = new StringContent(serializeObject);
            request.Content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
        }

        return await Client.SendAsync(request);
    }

    protected async Task<TResponse?> ReadFromResponse<TResponse>(HttpResponseMessage response)
    {
        var contentString = await response.Content.ReadAsStringAsync();
        return JsonConvert.DeserializeObject<TResponse>(contentString, SerializerSettings());
    }

    protected void InitData<TContext, TData>(TData data)
    where TContext : DbContext
    where TData : class
    {
        var serviceScopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        var dbSet = context.Set<TData>();
        try
        {
            dbSet?.Add(data);

            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex?.Message);
        }
    }

    protected void InitData<TContext, TData>(List<TData> data)
    where TContext : DbContext
        where TData : class
    {
        var serviceScopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = serviceScopeFactory.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<TContext>();
        try
        {
            var dbSet = context.Set<TData>();
            foreach (var dataItem in data)
            {
                dbSet?.Add(dataItem);
            }

            context.SaveChanges();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex?.Message);
        }
    }
}