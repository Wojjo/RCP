using WorkTimeRegistrationApp.ViewModels;

namespace WorkTimeRegistrationApp.Views;

public partial class HomePage : ContentPage
{
    private readonly HomePageViewModel _homePageViewModel;
    public HomePage(HomePageViewModel homePageViewModel)
    {
        InitializeComponent();
        BindingContext = homePageViewModel;
        _homePageViewModel = homePageViewModel;
        homePageViewModel.ProgressCircle = ProgressCircle;
        ProgressCircle.Drawable = homePageViewModel.CircularDrawable; 

        homePageViewModel.BreakProgressCircle = BreakProgressCircle;
        BreakProgressCircle.Drawable = homePageViewModel.BreakCircularDrawable;

        
    }

    protected override async void OnAppearing()
    {
        await _homePageViewModel.InitializeTimers();
    }
}