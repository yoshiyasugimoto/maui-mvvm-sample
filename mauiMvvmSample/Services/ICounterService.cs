namespace mauiMvvmSample.Services;

public interface ICounterService
{
    int CurrentCount { get; }
    
    event EventHandler<CountChangedEventArgs> CountChanged;
    
    Task<int> IncrementAsync();
    Task<int> ResetAsync();
    Task<int> SetCountAsync(int value);
}

public class CountChangedEventArgs : EventArgs
{
    public int OldValue { get; init; }
    public int NewValue { get; init; }
    public DateTime Timestamp { get; init; } = DateTime.Now;
}