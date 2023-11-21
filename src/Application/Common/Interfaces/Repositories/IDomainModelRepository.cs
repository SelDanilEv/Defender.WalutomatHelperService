using Defender.WalutomatHelperService.Domain.Entities;

namespace Defender.WalutomatHelperService.Application.Common.Interfaces.Repositories;

public interface IDomainModelRepository
{
    Task<DomainModel> GetDomainModelByIdAsync(Guid id);
}
