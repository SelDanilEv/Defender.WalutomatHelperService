using Defender.Common.Entities;
using Defender.WalutomatHelperService.Domain.Models;

namespace Defender.WalutomatHelperService.Domain.Entities;

public class CurrencyHistoricalRate : IBaseModel
{
    public Guid Id { get; set; }

    public CurrencyPair? CurrencyPair { get; set; }

    public decimal? Ask { get; set; }
    public decimal? Bid { get; set; }

    public DateTime? Date { get; set; }

    public CurrencyHistoricalRate Revert()
    {
        var tempCurrency = this.CurrencyPair.Currency1;
        this.CurrencyPair.Currency1 = this.CurrencyPair.Currency2;
        this.CurrencyPair.Currency2 = tempCurrency;

        var temp = this.Ask;
        this.Ask = 1 / this.Bid;
        this.Bid = 1 / temp;

        return this;
    }
}
