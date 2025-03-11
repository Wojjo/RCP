using WorkTimeRegistrationShared.Enums;

namespace WorkTimeRegistrationShared.DTOs;

public class RegisteredTimeDto
{
    public List<TimeRange> RegisteredWorkTimes { get; set; } = new();
    public RegisterTimeStatus? RegisterWorkTimeStatus { get; set; } = null;
    public List<TimeRange> RegisteredBreakTimes { get; set; } = new();
    public RegisterTimeStatus? RegisterBreakTimeStatus { get; set; } = null;

    public TimeSpan GetTotalWorkedTime()
    {
        RegisterWorkTimeStatus = CalculateTotalTime(RegisteredWorkTimes, out TimeSpan totalWorkedTime);
        return totalWorkedTime;
    }

    public TimeSpan GetTotalBreakTime()
    {
        RegisterBreakTimeStatus = CalculateTotalTime(RegisteredBreakTimes, out TimeSpan totalBreakTime);
        return totalBreakTime;
    }

    private RegisterTimeStatus CalculateTotalTime(List<TimeRange> timeRanges, out TimeSpan totalTime)
    {
        bool hasOngoingSession = false;
        totalTime = TimeSpan.Zero;

        foreach (var time in timeRanges)
        {
            DateTime endTime = time.EndTime == default ? DateTime.Now : time.EndTime;
            totalTime += endTime - time.StartTime;

            if (time.EndTime == default)
            {
                hasOngoingSession = true;
            }
        }

        return hasOngoingSession ? RegisterTimeStatus.TimekeepingStarted : RegisterTimeStatus.TimekeepingFinished;
    }

}

public class TimeRange
{
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }
}
