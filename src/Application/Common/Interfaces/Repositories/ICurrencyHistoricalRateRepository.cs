using Defender.WalutomatHelperService.Domain.Entities;

namespace Defender.WalutomatHelperService.Application.Common.Interfaces.Repositories;

public interface ICurrencyHistoricalRateRepository
{
    Task<CurrencyHistoricalRate> GetCurrencyHistoricalRateByIdAsync(Guid id);

    Task<CurrencyHistoricalRate> AddHistoricalRateAsync(CurrencyHistoricalRate rate);
}
