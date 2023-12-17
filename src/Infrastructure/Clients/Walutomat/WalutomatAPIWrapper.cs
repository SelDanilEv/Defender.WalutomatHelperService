using System.Security.Cryptography;
using System.Text;
using AutoMapper;
using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.Common.Wrapper;
using Defender.WalutomatHelperService.Application.Common.Interfaces.Wrapper;
using Defender.WalutomatHelperService.Domain.Entities;
using Defender.WalutomatHelperService.Domain.Enums;
using Defender.WalutomatHelperService.Infrastructure.Clients.Walutomat.Generated;
using Defender.WalutomatHelperService.Infrastructure.Helpers;
using Defender.WalutomatHelperService.Infrastructure.Helpers.LocalSecretHelper;
using Defender.WalutomatHelperService.Infrastructure.Mappings;

namespace Defender.WalutomatHelperService.Infrastructure.Clients.Walutomat;

public class WalutomatAPIWrapper : BaseSwaggerWrapper, IWalutomatAPIWrapper
{
    private readonly IMapper _mapper;
    private readonly IWalutomatClient _walutomatClient;

    public WalutomatAPIWrapper(
        IWalutomatClient walutomatClient,
        IMapper mapper)
    {
        _walutomatClient = walutomatClient;
        _mapper = mapper;
    }

    public async Task<CurrencyHistoricalRate> GetLastOfferDetails(WalutomatCurrencyPair currencyPair)
    {
        if (!EnumMapper<CurrencyPair2>.TryMapEnum(currencyPair, out var mappedCurrencyPair))
        {
            throw new ServiceException(ErrorCode.CM_MappingIssue);
        }

        var result = new CurrencyHistoricalRate();

        using (new CultureInfoScopeHelper("pl-PL"))
        {
            result.Date = DateTime.UtcNow;
        }

        var body = string.Empty;

        var signature = await GenerateServiceSignature(
            result.Date.Value,
            $"/api/v2.0.0/market_fx/best_offers/detailed?currencyPair={currencyPair}&itemLimit=1",
            body);

        _walutomatClient.SetApiKey(await LocalSecretsHelper.GetSecretAsync(LocalSecret.WalutomatApiKey));

        var lastBestOffes = await _walutomatClient.GetBestOffersDetailedAsync(
            signature,
            result.Date.Value,
            mappedCurrencyPair,
            1);

        if (lastBestOffes.Errors != null && lastBestOffes.Errors.Any())
        {
            throw new ServiceException(ErrorCode.ES_WalutomatIssue);
        }

        result.Ask = decimal.Parse(lastBestOffes?.Result?.Asks?.FirstOrDefault()?.Price!);
        result.Bid = decimal.Parse(lastBestOffes?.Result?.Bids?.FirstOrDefault()?.Price!);
        result.CurrencyPair = Domain.Models.CurrencyPair.FromWalutomatCurrencyPair(currencyPair);

        return result;
    }

    private async Task<string> GenerateServiceSignature(DateTime timestamp, string endpoint, string body)
    {
        var data = timestamp.ToString("yyyy-MM-ddTHH:mm:ssZ") + endpoint + body;

        var privateKey = await LocalSecretsHelper.GetSecretAsync(LocalSecret.WalutomatPrivateKey);

        var keyBytes = Convert.FromBase64String(privateKey);

        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportRSAPrivateKey(keyBytes, out _);

            using (var sha256 = SHA256.Create())
            {
                var signatureBytes = rsa.SignData(Encoding.UTF8.GetBytes(data), sha256);
                return Convert.ToBase64String(signatureBytes);
            }
        }
    }

    private string GenerateUserSignature(DateTime timestamp, string endpoint, string body, string privateKey)
    {
        var data = timestamp.ToString("yyyy-MM-ddTHH:mm:ssZ") + endpoint + body;
        var keyBytes = Convert.FromBase64String(privateKey);

        using (var rsa = new RSACryptoServiceProvider())
        {
            rsa.ImportRSAPrivateKey(keyBytes, out _);

            using (var sha256 = SHA256.Create())
            {
                var signatureBytes = rsa.SignData(Encoding.UTF8.GetBytes(data), sha256);
                return Convert.ToBase64String(signatureBytes);
            }
        }
    }
}
