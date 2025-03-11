using WorkTimeRegistrationShared.Enums;

namespace WorkTimeRegistrationShared.DTOs;

public class RegisterTimeDto
{
    public int UserId { get; set; }
    public DateTime ActionTime { get; set; }
    public RegisterTimeKind ActionKind { get; set; }
}
