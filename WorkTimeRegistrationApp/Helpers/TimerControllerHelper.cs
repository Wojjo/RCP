using System.Timers;
using WorkTimeRegistrationApp.Drawable;

namespace WorkTimeRegistrationApp.Helpers;

public class TimerControllerHelper
{
    private readonly System.Timers.Timer _timer;
    private DateTime _startTime;
    private TimeSpan _elapsed;
    private readonly Action<string> _updateElapsedTime;
    private readonly Action<double> _updateProgress;
    private readonly CircularProgress _circularDrawable;
    private readonly GraphicsView _progressCircle;
    private readonly IDispatcher _dispatcher;

    public bool IsRunning { get; private set; }

    public TimerControllerHelper(GraphicsView progressCircle, CircularProgress circularDrawable, 
        Action<string> updateElapsedTime, Action<double> updateProgress, TimeSpan elapsed)
    {
        _dispatcher = Application.Current!.Dispatcher;
        _progressCircle = progressCircle;
        _circularDrawable = circularDrawable;
        _updateElapsedTime = updateElapsedTime;
        _updateProgress = updateProgress;
        _elapsed = elapsed;
        _timer = new System.Timers.Timer(1000); 
        _timer.Elapsed += UpdateTime!;
    }

    public void ToggleTimer()
    {
        if (IsRunning)
        {
            _timer.Stop();
        }
        else
        {
            _startTime = DateTime.Now;
            if(_elapsed == default)
            {
                _elapsed = DateTime.Now - _startTime;
            }
            _timer.Start();
        }

        IsRunning = !IsRunning;
    }

    private void UpdateTime(object sender, ElapsedEventArgs e)
    {
        _elapsed += TimeSpan.FromSeconds(1);

        _dispatcher.Dispatch(() =>
        {
            _updateElapsedTime(_elapsed.ToString(@"hh\:mm\:ss"));
            double progressValue = (_elapsed.TotalSeconds % 60) / 60;
            _updateProgress(progressValue);
            _circularDrawable.Progress = progressValue;
            _progressCircle.Invalidate();
        });
    }
}