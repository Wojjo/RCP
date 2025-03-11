using WorkTimeRegistrationShared.DTOs;

namespace WorkTimeRegistrationApi.Service.TimeRegistrationService
{
    public interface ITimeRegistrationService
    {
        public Task<RegisterTimeResponseDto> RegisterTime(RegisterTimeDto registerTime);
        public Task<RegisteredTimeDto> GetRegisteredTime(int userId, DateTime date);
    }
}
