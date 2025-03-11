using WorkTimeRegistrationApi.Entities.DbCtx;
using WorkTimeRegistrationApi.Infrastructure;
using WorkTimeRegistrationApi.Models;
using WorkTimeRegistrationShared.DTOs;
using WorkTimeRegistrationShared.Enums;
using WorkTimeRegistrationShared.Infrastructure;

namespace WorkTimeRegistrationApi.Service.TimeRegistrationService.Impl.Commands;

public class RegisterTimeCommand : IResultRequest<RegisterTimeResponseDto>
{
    public RegisterTimeDto RegisterTime { get; set; }

    public RegisterTimeCommand(RegisterTimeDto registerTime)
    {
        RegisterTime = registerTime;
    }
}

public class RegisterTimeCommandHandler(RcpDbContext rcpCtx)
    : IResultRequestHandler<RegisterTimeCommand, RegisterTimeResponseDto>
{
    public async Task<Result<RegisterTimeResponseDto>> Handle(RegisterTimeCommand request,
        CancellationToken cancellationToken)
    {
        try
        {
            var registerTime = new RegisteredTime
            {
                UserId = request.RegisterTime.UserId,
                RegisterTimeKind = request.RegisterTime.ActionKind
            };

            if (registerTime.RegisterTimeKind is RegisterTimeKind.StartWork or RegisterTimeKind.StartBreak)
            {
                registerTime.StartTime = request.RegisterTime.ActionTime;
                rcpCtx.RegisteredTimes.Add(registerTime);
            }
            else if (registerTime.RegisterTimeKind is RegisterTimeKind.EndWork)
            {
                var time = rcpCtx.RegisteredTimes
                    .Where(x => x.UserId == request.RegisterTime.UserId && (int)x.RegisterTimeKind == (int)RegisterTimeKind.StartWork)
                    .OrderByDescending(x => x.StartTime)
                    .First();
                time.EndTime = request.RegisterTime.ActionTime;
                time.RegisterTimeKind = registerTime.RegisterTimeKind;
            }
            else if (registerTime.RegisterTimeKind is RegisterTimeKind.EndBreak)
            {
                var time = rcpCtx.RegisteredTimes
                    .Where(x => x.UserId == request.RegisterTime.UserId && (int)x.RegisterTimeKind == (int)RegisterTimeKind.StartBreak)
                    .OrderByDescending(x => x.StartTime)
                    .First();
                time.EndTime = request.RegisterTime.ActionTime;
                time.RegisterTimeKind = registerTime.RegisterTimeKind;
            }

            await rcpCtx.SaveChangesAsync(cancellationToken);
            return new Success<RegisterTimeResponseDto>(new RegisterTimeResponseDto { IsSuccess = true });
        }
        catch (Exception ex)
        {
            //handle error
            return new Failure<RegisterTimeResponseDto>("");
        }
    }
}