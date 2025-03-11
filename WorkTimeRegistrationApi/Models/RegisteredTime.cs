using System.ComponentModel.DataAnnotations;
using WorkTimeRegistrationShared.Enums;

namespace WorkTimeRegistrationApi.Models;

public class RegisteredTime
{
    [Key]
    public int Id { get; set; }
    public int UserId { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime? EndTime { get; set; } = null;
    public RegisterTimeKind RegisterTimeKind { get; set; }
}
