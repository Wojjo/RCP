using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkTimeRegistrationApp.Drawable;
using WorkTimeRegistrationApp.Helpers;
using WorkTimeRegistrationApp.Services.ApiService;
using WorkTimeRegistrationShared.DTOs;
using WorkTimeRegistrationShared.Enums;

namespace WorkTimeRegistrationApp.ViewModels;

public partial class HomePageViewModel : ObservableObject
{
    private readonly IApiService _apiService;
    private const int _userId = 1;
    public HomePageViewModel(IApiService apiService)
    {
        _apiService = apiService;
    }
    private TimerControllerHelper _workTimer;
    private TimerControllerHelper _breakTimer;
    public GraphicsView ProgressCircle { get; set; }
    public GraphicsView BreakProgressCircle { get; set; }
    public CircularProgress CircularDrawable { get; } = new() { ProgressColor = Colors.Green };
    public CircularProgress BreakCircularDrawable { get; } = new() { ProgressColor = Colors.SteelBlue };

    [ObservableProperty]
    private bool _isTimeCounting;

    [ObservableProperty]
    private string _elapsedTime = "00:00:00";

    [ObservableProperty]
    private double _progressValue;

    [ObservableProperty]
    private bool _isBreakTimeCounting;

    [ObservableProperty]
    private string _breakElapsedTime = "00:00:00";

    [ObservableProperty]
    private double _breakProgressValue;

    public async Task InitializeTimers()
    {
        var registeredTime = await _apiService.GetRegisteredTime(_userId, DateTime.Now);

        var todayTotalWorkTime = registeredTime.GetTotalWorkedTime();
        ElapsedTime = todayTotalWorkTime.ToString(@"hh\:mm\:ss");

        _workTimer = new TimerControllerHelper(ProgressCircle, CircularDrawable,
            elapsedTime => ElapsedTime = elapsedTime,
            progress => ProgressValue = progress, todayTotalWorkTime);

        if(registeredTime.RegisterWorkTimeStatus == RegisterTimeStatus.TimekeepingStarted)
        {
            _workTimer.ToggleTimer();
            IsTimeCounting = _workTimer.IsRunning;
        }

        var todayTotalBreakTime = registeredTime.GetTotalBreakTime();
        BreakElapsedTime = todayTotalBreakTime.ToString(@"hh\:mm\:ss");

        _breakTimer = new TimerControllerHelper(BreakProgressCircle, BreakCircularDrawable,
            elapsedTime => BreakElapsedTime = elapsedTime,
            progress => BreakProgressValue = progress, todayTotalBreakTime);

        if (registeredTime.RegisterBreakTimeStatus == RegisterTimeStatus.TimekeepingStarted)
        {
            _breakTimer.ToggleTimer();
            IsBreakTimeCounting = _breakTimer.IsRunning;
        }
    }

    [RelayCommand]
    public async Task ChangeTimerStatus()
    {      
        var registerTime = new RegisterTimeDto
        {
            UserId = _userId,
            ActionTime = DateTime.Now,
            ActionKind = !IsTimeCounting ? RegisterTimeKind.StartWork : RegisterTimeKind.EndWork
        };
        var result = await _apiService.RegisterTime(registerTime);
        if(result)
        {
            _workTimer.ToggleTimer();
            IsTimeCounting = _workTimer.IsRunning;
        }       
    }

    [RelayCommand]
    public async Task ChangeBreakTimerStatus()
    {     
        var registerTime = new RegisterTimeDto
        {
            UserId = _userId,
            ActionTime = DateTime.Now,
            ActionKind = !IsBreakTimeCounting ? RegisterTimeKind.StartBreak : RegisterTimeKind.EndBreak
        };
        var result = await _apiService.RegisterTime(registerTime);
        if (result)
        {
            _breakTimer.ToggleTimer();
            IsBreakTimeCounting = _breakTimer.IsRunning;
        }
    }
}