using Microsoft.EntityFrameworkCore;
using WorkTimeRegistrationApi.Entities.DbCtx;
using WorkTimeRegistrationApi.Infrastructure;
using WorkTimeRegistrationShared.DTOs;
using WorkTimeRegistrationShared.Enums;
using WorkTimeRegistrationShared.Infrastructure;

namespace WorkTimeRegistrationApi.Service.TimeRegistrationService.Impl.Queries;

public class GetRegisteredTimeQuery : IResultRequest<RegisteredTimeDto>
{
    public int UserId { get; set; }
    public DateTime Date { get; set; }

    public GetRegisteredTimeQuery(int userId, DateTime date)
    {
        UserId = userId;
        Date = date;
    }
}


public class GetRegisteredTimeQueryHandler(RcpDbContext rcpCtx)
    : IResultRequestHandler<GetRegisteredTimeQuery, RegisteredTimeDto>
{
    public async Task<Result<RegisteredTimeDto>> Handle(GetRegisteredTimeQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var today = DateTime.Now.Date;
            var tomorrow = today.AddDays(1);

            var registeredTimes = await rcpCtx.RegisteredTimes
                .Where(x => x.UserId == request.UserId 
                            && x.StartTime >= today 
                            && x.StartTime < tomorrow) 
                .AsNoTracking()
                .OrderBy(x => x.StartTime)
                .ToListAsync();

            var workTimes = new List<TimeRange>();
            var breakTimes = new List<TimeRange>();
            
            foreach (var time in registeredTimes)
            {
                if (time.RegisterTimeKind is RegisterTimeKind.StartWork or RegisterTimeKind.EndWork)
                {
                    workTimes.Add(new TimeRange { StartTime = time.StartTime, EndTime = time?.EndTime ?? default(DateTime) });
                }
                else if (time.RegisterTimeKind is RegisterTimeKind.StartBreak or RegisterTimeKind.EndBreak)
                {
                    breakTimes.Add(new TimeRange { StartTime = time.StartTime, EndTime = time?.EndTime ?? default(DateTime) });
                }
            }
            
            var result = new RegisteredTimeDto
            {
                RegisteredWorkTimes = workTimes,
                RegisteredBreakTimes = breakTimes
            };
            return new Success<RegisteredTimeDto>(result);
        }
        catch (Exception ex)
        {
            // Handle exception
            return new Failure<RegisteredTimeDto>("");
        }
    }
}