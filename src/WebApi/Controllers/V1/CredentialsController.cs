using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Defender.Common.Attributes;
using Defender.Common.Consts;
using Defender.WalutomatHelperService.Application.Modules.Module.Commands;

namespace Defender.WalutomatHelperService.WebUI.Controllers.V1;

public class CredentialsController : BaseApiController
{
    public CredentialsController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost("add")]
    [Auth(Roles.User)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task UploadCredentialDataAsync([FromBody] RefreshRatesCommand command)
    {
        await ProcessApiCallAsync(command);
    }

}
