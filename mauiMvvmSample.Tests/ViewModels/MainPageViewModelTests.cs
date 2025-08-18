using Moq;
using mauiMvvmSample.Services;
using mauiMvvmSample.ViewModels;

namespace mauiMvvmSample.Tests.ViewModels;

[TestClass]
public class MainPageViewModelTests
{
    private Mock<ICounterService> _mockCounterService = null!;
    private MainPageViewModel _viewModel = null!;

    [TestInitialize]
    public void Setup()
    {
        _mockCounterService = new Mock<ICounterService>();
        _mockCounterService.Setup(x => x.CurrentCount).Returns(0);
        _viewModel = new MainPageViewModel(_mockCounterService.Object);
    }

    [TestMethod]
    public void Constructor_ShouldSubscribeToCountChangedEvent()
    {
        // Assert
        _mockCounterService.VerifyAdd(x => x.CountChanged += It.IsAny<EventHandler<CountChangedEventArgs>>(), Times.Once);
    }

    [TestMethod]
    public void Constructor_ShouldInitializeWithDefaultButtonText()
    {
        // Assert
        Assert.AreEqual("Click me", _viewModel.ButtonText);
    }

    [TestMethod]
    public void Constructor_ShouldInitializeWithEmptyAnnounceText()
    {
        // Assert
        Assert.AreEqual(string.Empty, _viewModel.AnnounceText);
    }

    [TestMethod]
    public void Count_ShouldReturnCurrentCountFromService()
    {
        // Arrange
        _mockCounterService.Setup(x => x.CurrentCount).Returns(42);
        
        // Act
        var count = _viewModel.Count;
        
        // Assert
        Assert.AreEqual(42, count);
    }

    [TestMethod]
    public async Task IncrementCounterCommand_ShouldCallServiceIncrementAsync()
    {
        // Act
        await _viewModel.IncrementCounterCommand.ExecuteAsync(null);
        
        // Assert
        _mockCounterService.Verify(x => x.IncrementAsync(), Times.Once);
    }

    [TestMethod]
    public async Task IncrementCounterCommand_ShouldSetAnnounceTextOnException()
    {
        // Arrange
        var expectedException = new InvalidOperationException("Test error");
        _mockCounterService.Setup(x => x.IncrementAsync()).ThrowsAsync(expectedException);
        
        // Act
        await _viewModel.IncrementCounterCommand.ExecuteAsync(null);
        
        // Assert
        Assert.AreEqual("Error: Test error", _viewModel.AnnounceText);
    }

    [TestMethod]
    public async Task ResetCounterCommand_ShouldCallServiceResetAsync()
    {
        // Act
        await _viewModel.ResetCounterCommand.ExecuteAsync(null);
        
        // Assert
        _mockCounterService.Verify(x => x.ResetAsync(), Times.Once);
    }

    [TestMethod]
    public async Task ResetCounterCommand_ShouldSetAnnounceTextOnException()
    {
        // Arrange
        var expectedException = new InvalidOperationException("Reset error");
        _mockCounterService.Setup(x => x.ResetAsync()).ThrowsAsync(expectedException);
        
        // Act
        await _viewModel.ResetCounterCommand.ExecuteAsync(null);
        
        // Assert
        Assert.AreEqual("Error: Reset error", _viewModel.AnnounceText);
    }

    [TestMethod]
    public void OnCountChanged_WithZeroCount_ShouldUpdateButtonTextToClickMe()
    {
        // Arrange
        _mockCounterService.Setup(x => x.CurrentCount).Returns(0);
        var eventArgs = new CountChangedEventArgs { OldValue = 1, NewValue = 0 };
        
        // Act
        _mockCounterService.Raise(x => x.CountChanged += null, _mockCounterService.Object, eventArgs);
        
        // Assert
        Assert.AreEqual("Click me", _viewModel.ButtonText);
        Assert.AreEqual("Click me", _viewModel.AnnounceText);
    }

    [TestMethod]
    public void OnCountChanged_WithOneCount_ShouldUpdateButtonTextToSingular()
    {
        // Arrange
        _mockCounterService.Setup(x => x.CurrentCount).Returns(1);
        var eventArgs = new CountChangedEventArgs { OldValue = 0, NewValue = 1 };
        
        // Act
        _mockCounterService.Raise(x => x.CountChanged += null, _mockCounterService.Object, eventArgs);
        
        // Assert
        Assert.AreEqual("Clicked 1 time", _viewModel.ButtonText);
        Assert.AreEqual("Clicked 1 time", _viewModel.AnnounceText);
    }

    [TestMethod]
    public void OnCountChanged_WithMultipleCount_ShouldUpdateButtonTextToPlural()
    {
        // Arrange
        _mockCounterService.Setup(x => x.CurrentCount).Returns(5);
        var eventArgs = new CountChangedEventArgs { OldValue = 4, NewValue = 5 };
        
        // Act
        _mockCounterService.Raise(x => x.CountChanged += null, _mockCounterService.Object, eventArgs);
        
        // Assert
        Assert.AreEqual("Clicked 5 times", _viewModel.ButtonText);
        Assert.AreEqual("Clicked 5 times", _viewModel.AnnounceText);
    }

    [TestMethod]
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
        Assert.IsTrue(propertyChangedRaised);
        Assert.AreEqual(nameof(_viewModel.Count), changedPropertyName);
    }

    [TestMethod]
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
        Assert.IsTrue(propertyChangedRaised);
        Assert.AreEqual(nameof(_viewModel.ButtonText), changedPropertyName);
        Assert.AreEqual("New Text", _viewModel.ButtonText);
    }

    [TestMethod]
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
        Assert.IsTrue(propertyChangedRaised);
        Assert.AreEqual(nameof(_viewModel.AnnounceText), changedPropertyName);
        Assert.AreEqual("Test Announcement", _viewModel.AnnounceText);
    }

    [TestMethod]
    public void Commands_ShouldBeNotNull()
    {
        // Assert
        Assert.IsNotNull(_viewModel.IncrementCounterCommand);
        Assert.IsNotNull(_viewModel.ResetCounterCommand);
    }

    [TestMethod]
    public void Commands_ShouldBeExecutable()
    {
        // Assert
        Assert.IsTrue(_viewModel.IncrementCounterCommand.CanExecute(null));
        Assert.IsTrue(_viewModel.ResetCounterCommand.CanExecute(null));
    }
}