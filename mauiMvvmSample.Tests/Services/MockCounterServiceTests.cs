using Microsoft.Extensions.Logging;
using Moq;
using mauiMvvmSample.Services;

namespace mauiMvvmSample.Tests.Services;

public class MockCounterServiceTests
{
    private Mock<ICounterService> _mockCounterService = null!;

    public MockCounterServiceTests()
    {
        _mockCounterService = new Mock<ICounterService>();
    }

    [Fact]
    public void ICounterService_ShouldHaveRequiredProperties()
    {
        // Arrange
        _mockCounterService.Setup(x => x.CurrentCount).Returns(42);

        // Act
        var count = _mockCounterService.Object.CurrentCount;

        // Assert
        Assert.Equal(42, count);
        _mockCounterService.Verify(x => x.CurrentCount, Times.Once);
    }

    [Fact]
    public async Task ICounterService_IncrementAsync_ShouldBeCallable()
    {
        // Arrange
        _mockCounterService.Setup(x => x.IncrementAsync()).ReturnsAsync(1);

        // Act
        var result = await _mockCounterService.Object.IncrementAsync();

        // Assert
        Assert.Equal(1, result);
        _mockCounterService.Verify(x => x.IncrementAsync(), Times.Once);
    }

    [Fact]
    public async Task ICounterService_ResetAsync_ShouldBeCallable()
    {
        // Arrange
        _mockCounterService.Setup(x => x.ResetAsync()).ReturnsAsync(0);

        // Act
        var result = await _mockCounterService.Object.ResetAsync();

        // Assert
        Assert.Equal(0, result);
        _mockCounterService.Verify(x => x.ResetAsync(), Times.Once);
    }

    [Fact]
    public async Task ICounterService_SetCountAsync_ShouldBeCallable()
    {
        // Arrange
        _mockCounterService.Setup(x => x.SetCountAsync(It.IsAny<int>())).ReturnsAsync(100);

        // Act
        var result = await _mockCounterService.Object.SetCountAsync(100);

        // Assert
        Assert.Equal(100, result);
        _mockCounterService.Verify(x => x.SetCountAsync(100), Times.Once);
    }

    [Fact]
    public void ICounterService_CountChangedEvent_ShouldBeSubscribable()
    {
        // Arrange
        bool eventRaised = false;
        CountChangedEventArgs? receivedArgs = null;

        // Act
        _mockCounterService.Object.CountChanged += (sender, e) =>
        {
            eventRaised = true;
            receivedArgs = e;
        };

        var testArgs = new CountChangedEventArgs { OldValue = 0, NewValue = 1 };
        _mockCounterService.Raise(x => x.CountChanged += null, _mockCounterService.Object, testArgs);

        // Assert
        Assert.True(eventRaised);
        Assert.NotNull(receivedArgs);
        Assert.Equal(0, receivedArgs.OldValue);
        Assert.Equal(1, receivedArgs.NewValue);
    }

    [Fact]
    public void CountChangedEventArgs_ShouldSupportInitialization()
    {
        // Act
        var eventArgs = new CountChangedEventArgs
        {
            OldValue = 5,
            NewValue = 10,
            Timestamp = DateTime.Now.AddMinutes(-1)
        };

        // Assert
        Assert.Equal(5, eventArgs.OldValue);
        Assert.Equal(10, eventArgs.NewValue);
        Assert.True(eventArgs.Timestamp < DateTime.Now);
    }
}