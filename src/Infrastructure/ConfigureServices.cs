using System.Net.Http.Headers;
using System.Reflection;
using Defender.Common.Clients.Identity;
using Defender.Common.Helpers;
using Defender.Common.Interfaces;
using Defender.WalutomatHelperService.Application.Common.Interfaces;
using Defender.WalutomatHelperService.Application.Common.Interfaces.Repositories;
using Defender.WalutomatHelperService.Application.Common.Interfaces.Wrapper;
using Defender.WalutomatHelperService.Application.Configuration.Options;
using Defender.WalutomatHelperService.Infrastructure.Clients.Service;
using Defender.WalutomatHelperService.Infrastructure.Repositories.DomainModels;
using Defender.WalutomatHelperService.Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Defender.WalutomatHelperService.Infrastructure;

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
        services.RegisterIdentityAsServiceClient(
            (serviceProvider, client) =>
            {
                client.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<ServiceOptions>>().Value.Url);
                client.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(
                    "Bearer",
                    InternalJwtHelper.GenerateInternalJWT(configuration["JwtTokenIssuer"]));
            });

        services.RegisterIdentityClient(
            (serviceProvider, client) =>
            {
                client.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<ServiceOptions>>().Value.Url);

                var schemaAndToken = serviceProvider.GetRequiredService<IAccountAccessor>().Token?.Split(' ');

                if (schemaAndToken?.Length == 2)
                {
                    client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(schemaAndToken[0], schemaAndToken[1]);
                }
            });
    }

}
