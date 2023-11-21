using Defender.WalutomatHelperService.Application.Common.Interfaces;
using Defender.WalutomatHelperService.Application.Common.Interfaces.Repositories;

namespace Defender.WalutomatHelperService.Infrastructure.Services;

public class Service : IService
{
    private readonly IDomainModelRepository _accountInfoRepository;


    public Service(
        IDomainModelRepository accountInfoRepository)
    {
        _accountInfoRepository = accountInfoRepository;
    }

    public Task DoService()
    {
        throw new NotImplementedException();
    }
}
