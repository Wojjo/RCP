using WorkTimeRegistrationApp.Infrastructure;
using WorkTimeRegistrationShared.DTOs;

namespace WorkTimeRegistrationApp.Repository.ApiRepository.Impl;

public class ApiRepository(RequestProvider requestProvider) : IApiRepository
{
    public async Task<RegisteredTimeDto> GetRegisteredTime(int userId, DateTime date)
    {
        try
        {
            UriBuilder builder = new("http://localhost:5207")
            {
                Path = $"api/TimeRegistration/GetRegisteredTime/{userId}/{date}"
            };
            var result = await requestProvider.GetAsync<RegisteredTimeDto>(builder.ToString());
            return result.ResultValue ?? new RegisteredTimeDto();
        }
        catch (Exception ex)
        {
            // handle exception 
            return new RegisteredTimeDto();
        }
    }

    public async Task<bool> RegisterTime(RegisterTimeDto workTime)
    {
        try
        {
            UriBuilder builder = new("http://localhost:5207")
            {
                Path = "api/TimeRegistration/RegisterTime"
            };
            var result = await requestProvider.PostAsync<RegisterTimeDto>(builder.ToString(), workTime);
            return result.IsSuccessStatusCode;
        }
        catch (Exception ex)
        {
            // handle exception 
            return false;
        }
    }
}
