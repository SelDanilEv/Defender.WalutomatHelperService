using AutoMapper;
using Defender.Common.Clients.UserManagement;
using Defender.Common.Wrapper;
using Defender.ServiceTemplate.Application.Common.Interfaces.Wrapper;

namespace Defender.ServiceTemplate.Infrastructure.Clients.Service;

public class ServiceWrapper : BaseSwaggerWrapper, IServiceWrapper
{
    private readonly IMapper _mapper;
    private readonly IUserManagementAsServiceClient _userManagementClient;

    public ServiceWrapper(
        IUserManagementAsServiceClient userManagementClient,
        IMapper mapper)
    {
        _userManagementClient = userManagementClient;
        _mapper = mapper;
    }

    public Task DoWrap()
    {
        //var createCommand = new CreateUserCommand()
        //{
        //    Email = user.Email,
        //    PhoneNumber = user.PhoneNumber,
        //    Nickname = user.Nickname
        //};

        //return await ExecuteSafelyAsync(async () =>
        //{
        //    var response = await _userManagementClient.CreateAsync(createCommand);

        //    return _mapper.Map<Common.DTOs.UserDto>(response);
        //});
        throw new NotImplementedException();
    }
}
