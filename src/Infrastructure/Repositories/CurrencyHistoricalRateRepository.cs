using Defender.Common.Configuration.Options;
using Defender.Common.DB.Repositories;
using Defender.WalutomatHelperService.Application.Common.Interfaces.Repositories;
using Defender.WalutomatHelperService.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Defender.WalutomatHelperService.Infrastructure.Repositories.DomainModels;

public class CurrencyHistoricalRateRepository : BaseMongoRepository<CurrencyHistoricalRate>, ICurrencyHistoricalRateRepository
{
    public CurrencyHistoricalRateRepository(IOptions<MongoDbOptions> mongoOption) : base(mongoOption.Value)
    {
    }

    public async Task<CurrencyHistoricalRate> GetCurrencyHistoricalRateByIdAsync(Guid id)
    {
        return await GetItemAsync(id);
    }

    public async Task<CurrencyHistoricalRate> AddHistoricalRateAsync(CurrencyHistoricalRate rate)
    {
        return await AddItemAsync(rate);
    }
}
