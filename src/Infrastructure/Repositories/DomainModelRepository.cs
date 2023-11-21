using Defender.Common.Configuration.Options;
using Defender.Common.Repositories;
using Defender.WalutomatHelperService.Application.Common.Interfaces.Repositories;
using Defender.WalutomatHelperService.Domain.Entities;
using Microsoft.Extensions.Options;

namespace Defender.WalutomatHelperService.Infrastructure.Repositories.DomainModels;

public class DomainModelRepository : MongoRepository<DomainModel>, IDomainModelRepository
{
    public DomainModelRepository(IOptions<MongoDbOptions> mongoOption) : base(mongoOption.Value)
    {
    }

    public async Task<DomainModel> GetDomainModelByIdAsync(Guid id)
    {
        return await GetItemAsync(id);
    }
}
