using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.WalutomatHelperService.Application.Common.Interfaces;
using Defender.WalutomatHelperService.Application.Common.Interfaces.Repositories;
using Defender.WalutomatHelperService.Application.Common.Interfaces.Wrapper;
using Defender.WalutomatHelperService.Domain.Enums;
using Defender.WalutomatHelperService.Domain.Models;


namespace Defender.WalutomatHelperService.Application.Services;

public class RateService : IRateService
{
    private readonly ICurrencyHistoricalRateRepository _currencyRateRepository;
    private readonly IWalutomatAPIWrapper _walutomatAPIWrapper;

    public RateService(
        ICurrencyHistoricalRateRepository currencyRateRepository,
        IWalutomatAPIWrapper walutomatAPIWrapper)
    {
        _currencyRateRepository = currencyRateRepository;
        _walutomatAPIWrapper = walutomatAPIWrapper;
    }

    public async Task CheckAndSaveCurrentRates(Currency currency1, Currency currency2)
    {
        var currencyPair = new CurrencyPair(currency1, currency2);

        var walutomatCurrencyPair = currencyPair.GetWalutomatCurrencyPair();

        if (walutomatCurrencyPair == WalutomatCurrencyPair.UNKNOWN)
        {
            throw new ServiceException(ErrorCode.BR_WHS_NotSupportedCurrencyPair);
        }

        var historicalRate = await _walutomatAPIWrapper.GetLastOfferDetails(walutomatCurrencyPair);

        if (historicalRate != null)
        {
            await _currencyRateRepository.AddHistoricalRateAsync(historicalRate);
        }
    }
}
