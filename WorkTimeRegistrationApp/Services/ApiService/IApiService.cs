using WorkTimeRegistrationShared.DTOs;

namespace WorkTimeRegistrationApp.Services.ApiService;

public interface IApiService
{
    public Task<bool> RegisterTime(RegisterTimeDto workTime);
    public Task<RegisteredTimeDto> GetRegisteredTime(int userId, DateTime date);
}
