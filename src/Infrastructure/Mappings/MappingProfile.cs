using AutoMapper;
using Defender.Common.Clients.Notification;
using Defender.Common.Errors;
using Defender.Common.Exceptions;
using Defender.Common.Mapping;
using Defender.WalutomatHelperService.Domain.Entities;
using Defender.WalutomatHelperService.Domain.Enums;
using Defender.WalutomatHelperService.Domain.Models;
using Defender.WalutomatHelperService.Infrastructure.Clients.Walutomat.Generated;
using ZstdSharp;

namespace Defender.WalutomatHelperService.Infrastructure.Mappings;

public class MappingProfile : BaseMappingProfile
{
    public MappingProfile()
    {

    }
}
