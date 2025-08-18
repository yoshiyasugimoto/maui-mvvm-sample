using Microsoft.Extensions.Logging;

namespace mauiMvvmSample.Services;

public class CounterService : ICounterService
{
    private readonly ILogger<CounterService> _logger;
    private int _currentCount;

    public int CurrentCount => _currentCount;

    public event EventHandler<CountChangedEventArgs>? CountChanged;

    public CounterService(ILogger<CounterService> logger)
    {
        _logger = logger;
        _currentCount = Preferences.Get("counter_value", 0);
        
        _logger.LogInformation("CounterService initialized with count: {Count}", _currentCount);
    }

    public async Task<int> IncrementAsync()
    {
        try
        {
            _logger.LogDebug("Incrementing counter from {CurrentCount}", _currentCount);

            var oldValue = _currentCount;

            if (_currentCount >= 999999)
            {
                _logger.LogWarning("Counter reached maximum value: {MaxValue}", 999999);
                throw new InvalidOperationException("カウンターが最大値に達しました");
            }

            _currentCount++;

            await SaveCountAsync();

            CountChanged?.Invoke(this, new CountChangedEventArgs 
            { 
                OldValue = oldValue, 
                NewValue = _currentCount 
            });

            _logger.LogInformation("Counter incremented to {NewCount}", _currentCount);

            return _currentCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error incrementing counter");
            throw;
        }
    }

    public async Task<int> ResetAsync()
    {
        try
        {
            _logger.LogDebug("Resetting counter from {CurrentCount}", _currentCount);

            var oldValue = _currentCount;
            _currentCount = 0;

            await SaveCountAsync();

            CountChanged?.Invoke(this, new CountChangedEventArgs 
            { 
                OldValue = oldValue, 
                NewValue = _currentCount 
            });

            _logger.LogInformation("Counter reset to 0");

            return _currentCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error resetting counter");
            throw;
        }
    }

    public async Task<int> SetCountAsync(int value)
    {
        try
        {
            if (value < 0)
            {
                throw new ArgumentException("カウント値は0以上である必要があります", nameof(value));
            }

            if (value > 999999)
            {
                throw new ArgumentException("カウント値は999999以下である必要があります", nameof(value));
            }

            var oldValue = _currentCount;
            _currentCount = value;

            await SaveCountAsync();

            CountChanged?.Invoke(this, new CountChangedEventArgs 
            { 
                OldValue = oldValue, 
                NewValue = _currentCount 
            });

            _logger.LogInformation("Counter set to {NewCount}", _currentCount);

            return _currentCount;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error setting counter to {Value}", value);
            throw;
        }
    }

    private async Task SaveCountAsync()
    {
        Preferences.Set("counter_value", _currentCount);
        _logger.LogDebug("Saved counter value: {Count}", _currentCount);
        await Task.CompletedTask;
    }
}