using Defender.WalutomatHelperService.Domain.Enums;

namespace Defender.WalutomatHelperService.Application.Common.Interfaces;

public interface IRateService
{
    Task CheckAndSaveCurrentRates(Currency currency1, Currency currency2);
}
