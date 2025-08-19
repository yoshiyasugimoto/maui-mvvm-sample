using Moq;
using mauiMvvmSample.Services;
using mauiMvvmSample.ViewModels;

namespace mauiMvvmSample.Tests.ViewModels;

public class MainPageViewModelTests
{
    private Mock<ICounterService> _mockCounterService = null!;
    private MainPageViewModel _viewModel = null!;

    public MainPageViewModelTests()
    {
        _mockCounterService = new Mock<ICounterService>();
        _mockCounterService.Setup(x => x.CurrentCount).Returns(0);
        _viewModel = new MainPageViewModel(_mockCounterService.Object);
    }

    [Fact]
    public void Constructor_ShouldSubscribeToCountChangedEvent()
    {
        // Assert
        _mockCounterService.VerifyAdd(x => x.CountChanged += It.IsAny<EventHandler<CountChangedEventArgs>>(), Times.Once);
    }

    [Fact]
    public void Constructor_ShouldInitializeWithDefaultButtonText()
    {
        // Assert
        Assert.Equal("Click me", _viewModel.ButtonText);
    }

    [Fact]
    public void Constructor_ShouldInitializeWithEmptyAnnounceText()
    {
        // Assert
        Assert.Equal(string.Empty, _viewModel.AnnounceText);
    }

    [Fact]
    public void Count_ShouldReturnCurrentCountFromService()
    {
        // Arrange
        _mockCounterService.Setup(x => x.CurrentCount).Returns(42);
        
        // Act
        var count = _viewModel.Count;
        
        // Assert
        Assert.Equal(42, count);
    }

    [Fact]
    public async Task IncrementCounterCommand_ShouldCallServiceIncrementAsync()
    {
        // Act
        await _viewModel.IncrementCounterCommand.ExecuteAsync(null);
        
        // Assert
        _mockCounterService.Verify(x => x.IncrementAsync(), Times.Once);
    }

    [Fact]
    public async Task IncrementCounterCommand_ShouldSetAnnounceTextOnException()
    {
        // Arrange
        var expectedException = new InvalidOperationException("Test error");
        _mockCounterService.Setup(x => x.IncrementAsync()).ThrowsAsync(expectedException);
        
        // Act
        await _viewModel.IncrementCounterCommand.ExecuteAsync(null);
        
        // Assert
        Assert.Equal("Error: Test error", _viewModel.AnnounceText);
    }

    [Fact]
    public async Task ResetCounterCommand_ShouldCallServiceResetAsync()
    {
        // Act
        await _viewModel.ResetCounterCommand.ExecuteAsync(null);
        
        // Assert
        _mockCounterService.Verify(x => x.ResetAsync(), Times.Once);
    }

    [Fact]
    public async Task ResetCounterCommand_ShouldSetAnnounceTextOnException()
    {
        // Arrange
        var expectedException = new InvalidOperationException("Reset error");
        _mockCounterService.Setup(x => x.ResetAsync()).ThrowsAsync(expectedException);
        
        // Act
        await _viewModel.ResetCounterCommand.ExecuteAsync(null);
        
        // Assert
        Assert.Equal("Error: Reset error", _viewModel.AnnounceText);
    }

    [Fact]
    public void OnCountChanged_WithZeroCount_ShouldUpdateButtonTextToClickMe()
    {
        // Arrange
        _mockCounterService.Setup(x => x.CurrentCount).Returns(0);
        var eventArgs = new CountChangedEventArgs { OldValue = 1, NewValue = 0 };
        
        // Act
        _mockCounterService.Raise(x => x.CountChanged += null, _mockCounterService.Object, eventArgs);
        
        // Assert
        Assert.Equal("Click me", _viewModel.ButtonText);
        Assert.Equal("Click me", _viewModel.AnnounceText);
    }

    [Fact]
    public void OnCountChanged_WithOneCount_ShouldUpdateButtonTextToSingular()
    {
        // Arrange
        _mockCounterService.Setup(x => x.CurrentCount).Returns(1);
        var eventArgs = new CountChangedEventArgs { OldValue = 0, NewValue = 1 };
        
        // Act
        _mockCounterService.Raise(x => x.CountChanged += null, _mockCounterService.Object, eventArgs);
        
        // Assert
        Assert.Equal("Clicked 1 time", _viewModel.ButtonText);
        Assert.Equal("Clicked 1 time", _viewModel.AnnounceText);
    }

    [Fact]
    public void OnCountChanged_WithMultipleCount_ShouldUpdateButtonTextToPlural()
    {
        // Arrange
        _mockCounterService.Setup(x => x.CurrentCount).Returns(5);
        var eventArgs = new CountChangedEventArgs { OldValue = 4, NewValue = 5 };
        
        // Act
        _mockCounterService.Raise(x => x.CountChanged += null, _mockCounterService.Object, eventArgs);
        
        // Assert
        Assert.Equal("Clicked 5 times", _viewModel.ButtonText);
        Assert.Equal("Clicked 5 times", _viewModel.AnnounceText);
    }

    [Fact]
    public void OnCountChanged_ShouldTriggerPropertyChangedForCount()
    {
        // Arrange
        bool propertyChangedRaised = false;
        string? changedPropertyName = null;
        
        _viewModel.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(_viewModel.Count))
            {
                propertyChangedRaised = true;
                changedPropertyName = e.PropertyName;
            }
        };
        
        var eventArgs = new CountChangedEventArgs { OldValue = 0, NewValue = 1 };
        
        // Act
        _mockCounterService.Raise(x => x.CountChanged += null, _mockCounterService.Object, eventArgs);
        
        // Assert
        Assert.True(propertyChangedRaised);
        Assert.Equal(nameof(_viewModel.Count), changedPropertyName);
    }

    [Fact]
    public void ButtonText_PropertyChanged_ShouldNotifyObservers()
    {
        // Arrange
        bool propertyChangedRaised = false;
        string? changedPropertyName = null;
        
        _viewModel.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(_viewModel.ButtonText))
            {
                propertyChangedRaised = true;
                changedPropertyName = e.PropertyName;
            }
        };
        
        // Act
        _viewModel.ButtonText = "New Text";
        
        // Assert
        Assert.True(propertyChangedRaised);
        Assert.Equal(nameof(_viewModel.ButtonText), changedPropertyName);
        Assert.Equal("New Text", _viewModel.ButtonText);
    }

    [Fact]
    public void AnnounceText_PropertyChanged_ShouldNotifyObservers()
    {
        // Arrange
        bool propertyChangedRaised = false;
        string? changedPropertyName = null;
        
        _viewModel.PropertyChanged += (sender, e) =>
        {
            if (e.PropertyName == nameof(_viewModel.AnnounceText))
            {
                propertyChangedRaised = true;
                changedPropertyName = e.PropertyName;
            }
        };
        
        // Act
        _viewModel.AnnounceText = "Test Announcement";
        
        // Assert
        Assert.True(propertyChangedRaised);
        Assert.Equal(nameof(_viewModel.AnnounceText), changedPropertyName);
        Assert.Equal("Test Announcement", _viewModel.AnnounceText);
    }

    [Fact]
    public void Commands_ShouldBeNotNull()
    {
        // Assert
        Assert.NotNull(_viewModel.IncrementCounterCommand);
        Assert.NotNull(_viewModel.ResetCounterCommand);
    }

    [Fact]
    public void Commands_ShouldBeExecutable()
    {
        // Assert
        Assert.True(_viewModel.IncrementCounterCommand.CanExecute(null));
        Assert.True(_viewModel.ResetCounterCommand.CanExecute(null));
    }
}