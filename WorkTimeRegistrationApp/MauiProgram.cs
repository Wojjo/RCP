using Microsoft.Extensions.Logging;
using WorkTimeRegistrationApp.Infrastructure;
using WorkTimeRegistrationApp.Repository.ApiRepository;
using WorkTimeRegistrationApp.Repository.ApiRepository.Impl;
using WorkTimeRegistrationApp.Services.ApiService;
using WorkTimeRegistrationApp.Services.ApiService.Impl;
using WorkTimeRegistrationApp.ViewModels;

namespace WorkTimeRegistrationApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif
            builder.Services.AddSingleton<RequestProvider>();
            builder.Services.AddSingleton<IApiService, ApiService>();
            builder.Services.AddSingleton<IApiRepository, ApiRepository>();
            builder.Services.AddSingleton<HomePageViewModel>();
            return builder.Build();
        }
    }
}
