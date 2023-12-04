using Defender.Common.Exstension;
using Defender.WalutomatHelperService.Application.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defender.WalutomatHelperService.Application.Configuration.Exstension;

public static class ServiceOptionsExtensions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WalutomatOptions>(configuration.GetSection(nameof(WalutomatOptions)));

        return services;
    }
}