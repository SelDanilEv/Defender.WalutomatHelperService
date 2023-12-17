using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Defender.Common.Attributes;
using Defender.Common.Models;
using Defender.WalutomatHelperService.Application.Modules.Module.Commands;

namespace Defender.WalutomatHelperService.WebUI.Controllers.V1;

public class WalutomatController : BaseApiController
{
    public WalutomatController(IMediator mediator, IMapper mapper) : base(mediator, mapper)
    {
    }

    [HttpPost("refresh")]
    //[Auth(Roles.User)]
    [ProducesResponseType(typeof(void), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task RefreshRatesAsync([FromQuery] RefreshRatesCommand command)
    {
        await ProcessApiCallAsync(command);
    }

}
