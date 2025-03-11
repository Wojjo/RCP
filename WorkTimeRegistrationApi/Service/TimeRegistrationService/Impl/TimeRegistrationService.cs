using MediatR;
using WorkTimeRegistrationApi.Service.TimeRegistrationService.Impl.Commands;
using WorkTimeRegistrationApi.Service.TimeRegistrationService.Impl.Queries;
using WorkTimeRegistrationShared.DTOs;

namespace WorkTimeRegistrationApi.Service.TimeRegistrationService.Impl;

public class TimeRegistrationService : ITimeRegistrationService
{
    private readonly IMediator _mediator;

    public TimeRegistrationService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<RegisteredTimeDto> GetRegisteredTime(int userId, DateTime date)
    {
        var result =  await _mediator.Send(new GetRegisteredTimeQuery(userId, date));
        return result?.ResultValue ?? new RegisteredTimeDto();
    }

    public async Task<RegisterTimeResponseDto> RegisterTime(RegisterTimeDto registerTime)
    {
        var result = await _mediator.Send(new RegisterTimeCommand(registerTime));
        return result?.ResultValue ?? new RegisterTimeResponseDto() { IsSuccess = false };
    }
}
