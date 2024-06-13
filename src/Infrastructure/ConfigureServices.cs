using System.Net.Http.Headers;
using System.Reflection;
using Defender.Common.Clients.Identity;
using Defender.Common.Clients.UserManagement;
using Defender.Common.Helpers;
using Defender.Common.Interfaces;
using Defender.WalutomatHelperService.Application.Common.Interfaces;
using Defender.WalutomatHelperService.Application.Common.Interfaces.Repositories;
using Defender.WalutomatHelperService.Application.Common.Interfaces.Wrapper;
using Defender.WalutomatHelperService.Application.Configuration.Options;
using Defender.WalutomatHelperService.Application.Services;
using Defender.WalutomatHelperService.Infrastructure.Clients.Walutomat;
using Defender.WalutomatHelperService.Infrastructure.Clients.Walutomat.Generated;
using Defender.WalutomatHelperService.Infrastructure.Repositories.DomainModels;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace Defender.WalutomatHelperService.Infrastructure;

public static class ConfigureServices
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        RegisterRepositories(services);

        RegisterApiClients(services, configuration);

        RegisterClientWrappers(services);

        return services;
    }

    private static void RegisterClientWrappers(IServiceCollection services)
    {
        services.AddTransient<IWalutomatAPIWrapper, WalutomatAPIWrapper>();
    }

    private static void RegisterRepositories(IServiceCollection services)
    {
        services.AddSingleton<ICurrencyHistoricalRateRepository, CurrencyHistoricalRateRepository>();
    }

    private static void RegisterApiClients(IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpClient<IWalutomatClient, WalutomatClient>(nameof(WalutomatClient), (serviceProvider, client) =>
            {
                client.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<WalutomatOptions>>().Value.Url);
            });

        //services.RegisterIdentityAsServiceClient(
        //    (serviceProvider, client) =>
        //    {
        //        client.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<ServiceOptions>>().Value.Url);
        //        client.DefaultRequestHeaders.Authorization =
        //        new AuthenticationHeaderValue(
        //            "Bearer",
        //            InternalJwtHelper.GenerateInternalJWT(configuration["JwtTokenIssuer"]));
        //    });

        //services.RegisterIdentityClient(
        //    (serviceProvider, client) =>
        //    {
        //        client.BaseAddress = new Uri(serviceProvider.GetRequiredService<IOptions<ServiceOptions>>().Value.Url);

        //        var schemaAndToken = serviceProvider.GetRequiredService<IAccountAccessor>().Token?.Split(' ');

        //        if (schemaAndToken?.Length == 2)
        //        {
        //            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(schemaAndToken[0], schemaAndToken[1]);
        //        }
        //    });
    }

}
