using Defender.WalutomatHelperService.Domain.Entities;
using Defender.WalutomatHelperService.Domain.Enums;

namespace Defender.WalutomatHelperService.Application.Common.Interfaces.Wrapper;

public interface IWalutomatAPIWrapper
{
    Task<CurrencyHistoricalRate> GetLastOfferDetails(WalutomatCurrencyPair currencyPair);
}
