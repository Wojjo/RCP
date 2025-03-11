using WorkTimeRegistrationShared.DTOs;

namespace WorkTimeRegistrationApp.Repository.ApiRepository;

public interface IApiRepository
{
    public Task<RegisteredTimeDto> GetRegisteredTime(int userId, DateTime date);
    public Task<bool> RegisterTime(RegisterTimeDto workTime);
}
