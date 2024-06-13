using Defender.Common.Extension;
using Defender.WalutomatHelperService.Application.Configuration.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Defender.WalutomatHelperService.Application.Configuration.Extension;

public static class ServiceOptionsExtensions
{
    public static IServiceCollection AddApplicationOptions(this IServiceCollection services, IConfiguration configuration)
    {
        services.Configure<WalutomatOptions>(configuration.GetSection(nameof(WalutomatOptions)));

        return services;
    }
}