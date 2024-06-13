using Defender.Common.Errors;
using Defender.Common.Interfaces;
using Defender.WalutomatHelperService.Application.Common.Interfaces;
using Defender.WalutomatHelperService.Domain.Enums;
using FluentValidation;
using Defender.Common.Extension;
using MediatR;

namespace Defender.WalutomatHelperService.Application.Modules.Module.Commands;

public record RefreshRatesCommand : IRequest<Unit>
{
    public Currency Currency1 { get; set; }
    public Currency Currency2 { get; set; }
};

public sealed class RefreshRatesValidator : AbstractValidator<RefreshRatesCommand>
{
    public RefreshRatesValidator()
    {
        //RuleFor(s => s.DoModule)
        //          .NotEmpty().WithMessage(ErrorCode.VL_InvalidRequest);
    }
}

public sealed class RefreshRatesHandler : IRequestHandler<RefreshRatesCommand, Unit>
{
    private readonly IAccountAccessor _accountAccessor;
    private readonly IRateService _rateService;

    public RefreshRatesHandler(
        IAccountAccessor accountAccessor,
        IRateService rateService
        )
    {
        _accountAccessor = accountAccessor;
        _rateService = rateService;
    }

    public async Task<Unit> Handle(RefreshRatesCommand request, CancellationToken cancellationToken)
    {
        await _rateService.CheckAndSaveCurrentRates(request.Currency1, request.Currency2);

        return Unit.Value;
    }
}
