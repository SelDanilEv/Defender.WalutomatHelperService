using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.WalutomatHelperService.Domain.Enums;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Defender.WalutomatHelperService.Domain.Models;

public class CurrencyPair
{
    [BsonRepresentation(BsonType.String)]
    public Currency Currency1 { get; set; }
    [BsonRepresentation(BsonType.String)]
    public Currency Currency2 { get; set; }

    public CurrencyPair()
    {
    }

    public CurrencyPair(Currency currency1, Currency currency2)
    {
        Currency1 = currency1;
        Currency2 = currency2;
    }

    public static CurrencyPair FromWalutomatCurrencyPair(
        WalutomatCurrencyPair walutomatCurrencyPair)
    {
        switch (walutomatCurrencyPair)
        {
            case WalutomatCurrencyPair.EURUSD:
                return new CurrencyPair(Currency.EUR, Currency.USD);
            case WalutomatCurrencyPair.EURPLN:
                return new CurrencyPair(Currency.EUR, Currency.PLN);
            case WalutomatCurrencyPair.USDPLN:
                return new CurrencyPair(Currency.USD, Currency.PLN);
            default: throw new ServiceException(ErrorCode.UnhandledError);
        }
    }

    public string? ToStringPair()
    {
        return $"{Currency1}{Currency2}";
    }
    public string? ToRevertStringPair()
    {
        return $"{Currency2}{Currency1}";
    }

    public WalutomatCurrencyPair GetWalutomatCurrencyPair()
    {
        if (TryParseCurrencyPair(this.ToStringPair(), out var result) ||
            TryParseCurrencyPair(this.ToRevertStringPair(), out result))
        {
            return result;
        }

        return WalutomatCurrencyPair.UNKNOWN;
    }

    private bool TryParseCurrencyPair(string input, out WalutomatCurrencyPair result)
    {
        return Enum.TryParse(input, true, out result);
    }
}
