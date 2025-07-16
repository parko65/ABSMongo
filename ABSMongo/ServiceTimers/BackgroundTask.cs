using System.Diagnostics;

namespace ABSMongo.ServiceTimers;
public class BackgroundTask
{
    private Task? _timerTask;
    private readonly PeriodicTimer _timer;
    private readonly CancellationTokenSource _cts = new();

    public BackgroundTask(TimeSpan interval)
    {
        _timer = new PeriodicTimer(interval);
    }

    public void Start()
    {
        _timerTask = DoWorkAsync();
    }

    private async Task DoWorkAsync()
    {
        try
        {
            while (await _timer.WaitForNextTickAsync(_cts.Token))
            {

            }
        }
        catch (OperationCanceledException)
        {
            Debug.WriteLine("Timer stopped.");
        }
    }

    public async Task StopAsync()
    {
        if (_timerTask is null)
        {
            return;
        }

        _cts.Cancel();

        try
        {
            await _timerTask;
        }
        catch (OperationCanceledException)
        {
            // Expected when cancelling
            Debug.WriteLine("Timer stopped.");
        }

        _cts.Dispose();

        _timer.Dispose();

        Debug.WriteLine("Task was cancelled.");
    }
}