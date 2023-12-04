using Defender.Common.Entities;

namespace Defender.WalutomatHelperService.Domain.Entities;

public class UserWalutomatCredentials : IBaseModel
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string? PrivateKey { get; set; }
    public string? ApiKey { get; set; }

}
