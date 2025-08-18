using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using mauiMvvmSample.Services;

namespace mauiMvvmSample.ViewModels;

public partial class MainPageViewModel : ObservableObject
{
    private readonly ICounterService _counterService;

    [ObservableProperty]
    private string buttonText = "Click me";

    [ObservableProperty]
    private string announceText = string.Empty;

    public int Count => _counterService.CurrentCount;

    public MainPageViewModel(ICounterService counterService)
    {
        _counterService = counterService;
        _counterService.CountChanged += OnCountChanged;
        
        UpdateButtonText();
    }

    [RelayCommand]
    private async Task IncrementCounter()
    {
        try
        {
            await _counterService.IncrementAsync();
        }
        catch (Exception ex)
        {
            AnnounceText = $"Error: {ex.Message}";
        }
    }

    [RelayCommand]
    private async Task ResetCounter()
    {
        try
        {
            await _counterService.ResetAsync();
        }
        catch (Exception ex)
        {
            AnnounceText = $"Error: {ex.Message}";
        }
    }

    private void OnCountChanged(object? sender, CountChangedEventArgs e)
    {
        OnPropertyChanged(nameof(Count));
        UpdateButtonText();
        AnnounceText = ButtonText;
    }

    private void UpdateButtonText()
    {
        var count = Count;
        if (count == 0)
        {
            ButtonText = "Click me";
        }
        else if (count == 1)
        {
            ButtonText = $"Clicked {count} time";
        }
        else
        {
            ButtonText = $"Clicked {count} times";
        }
    }
}