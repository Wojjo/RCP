using Microsoft.EntityFrameworkCore;
using System.Reflection;
using WorkTimeRegistrationApi.Entities.DbCtx;
using WorkTimeRegistrationApi.Service.TimeRegistrationService;
using WorkTimeRegistrationApi.Service.TimeRegistrationService.Impl;
using ServerVersion = Microsoft.EntityFrameworkCore.ServerVersion;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<RcpDbContext>(options =>
{
    options.UseMySql(
        builder.Configuration.GetConnectionString("RcpDb")!,
        ServerVersion.Parse("8.0.23-mysql")
    );
#if DEBUG
    options.EnableSensitiveDataLogging(true);
#endif
});

builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
builder.Services.AddScoped<ITimeRegistrationService, TimeRegistrationService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
