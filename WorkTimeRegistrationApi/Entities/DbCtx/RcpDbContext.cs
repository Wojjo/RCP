using Microsoft.EntityFrameworkCore;
using WorkTimeRegistrationApi.Models;

namespace WorkTimeRegistrationApi.Entities.DbCtx;

public class RcpDbContext(DbContextOptions<RcpDbContext> options) : DbContext(options)
{
    public virtual DbSet<RegisteredTime> RegisteredTimes { get; set; }
}