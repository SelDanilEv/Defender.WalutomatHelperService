using System.Net.Http.Headers;
using System.Reflection;
using Defender.Common.Helpers;
using Defender.ServiceTemplate.Application.Common.Interfaces;
using Defender.ServiceTemplate.Application.Common.Interfaces.Repositories;
using Defender.ServiceTemplate.Application.Common.Interfaces.Wrapper;
using Defender.ServiceTemplate.Application.Configuration.Options;
using Defender.ServiceTemplate.Infrastructure.Clients.Service;
using Defender.ServiceTemplate.Infrastructure.Clients.Service.Generated;
using Defender.ServiceTemplate.Infrastructure.Repositories.DomainModels;
using Defender.ServiceTemplate.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Defender.ServiceTemplate.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        RegisterServices(services);

        RegisterRepositories(services);

        RegisterApiClients(services, configuration);

        RegisterClientWrappers(services);

        return services;
    }

    private static void RegisterClientWrappers(IServiceCollection services)
    {
        services.AddTransient<IServiceWrapper, ServiceWrapper>();
    }

    private static void RegisterServices(IServiceCollection services)
    {
        services.AddTransient<IService, Service>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddSingleton<IDomainModelRepository, DomainModelRepository>();
    }

    private static void RegisterApiClients(IServiceCollection services, IConfiguration configuration)
    {

        services.AddHttpClient<IServiceClient, ServiceClient>(nameof(ServiceClient), (serviceProvider, client) =>
        {
            client.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<ServiceOptions>>().Value.Url);
            client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                InternalJwtHelper.GenerateInternalJWT(configuration["JwtTokenIssuer"]));
        });
    }

}
