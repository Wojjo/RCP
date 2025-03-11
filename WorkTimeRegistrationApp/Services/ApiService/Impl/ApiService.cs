using WorkTimeRegistrationApp.Repository.ApiRepository;
using WorkTimeRegistrationShared.DTOs;

namespace WorkTimeRegistrationApp.Services.ApiService.Impl;

public class ApiService(IApiRepository apiRepository) : IApiService
{
    public async Task<RegisteredTimeDto> GetRegisteredTime(int userId, DateTime date)
    {
        return await apiRepository.GetRegisteredTime(userId, date);
    }

    public async Task<bool> RegisterTime(RegisterTimeDto workTime)
    {
        return await apiRepository.RegisterTime(workTime);
    }
}
