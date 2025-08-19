using mauiMvvmSample.Services;

namespace mauiMvvmSample.Tests.Models;

public class CountChangedEventArgsTests
{
    [Fact]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Act
        var eventArgs = new CountChangedEventArgs();
        
        // Assert
        Assert.Equal(0, eventArgs.OldValue);
        Assert.Equal(0, eventArgs.NewValue);
        Assert.True(eventArgs.Timestamp <= DateTime.Now);
        Assert.True(eventArgs.Timestamp >= DateTime.Now.AddSeconds(-1));
    }

    [Fact]
    public void Properties_ShouldBeSetCorrectly()
    {
        // Arrange
        var expectedTimestamp = DateTime.Now.AddMinutes(-5);
        
        // Act
        var eventArgs = new CountChangedEventArgs
        {
            OldValue = 10,
            NewValue = 15,
            Timestamp = expectedTimestamp
        };
        
        // Assert
        Assert.Equal(10, eventArgs.OldValue);
        Assert.Equal(15, eventArgs.NewValue);
        Assert.Equal(expectedTimestamp, eventArgs.Timestamp);
    }

    [Fact]
    public void OldValue_ShouldAcceptNegativeValues()
    {
        // Act
        var eventArgs = new CountChangedEventArgs { OldValue = -5 };
        
        // Assert
        Assert.Equal(-5, eventArgs.OldValue);
    }

    [Fact]
    public void NewValue_ShouldAcceptNegativeValues()
    {
        // Act
        var eventArgs = new CountChangedEventArgs { NewValue = -10 };
        
        // Assert
        Assert.Equal(-10, eventArgs.NewValue);
    }

    [Fact]
    public void Values_ShouldAcceptLargeNumbers()
    {
        // Act
        var eventArgs = new CountChangedEventArgs
        {
            OldValue = int.MaxValue,
            NewValue = int.MinValue
        };
        
        // Assert
        Assert.Equal(int.MaxValue, eventArgs.OldValue);
        Assert.Equal(int.MinValue, eventArgs.NewValue);
    }

    [Fact]
    public void Timestamp_ShouldBeUpdatedAutomatically()
    {
        // Arrange
        var startTime = DateTime.Now;
        
        // Act
        var eventArgs = new CountChangedEventArgs();
        var endTime = DateTime.Now;
        
        // Assert
        Assert.True(eventArgs.Timestamp >= startTime);
        Assert.True(eventArgs.Timestamp <= endTime);
    }

    [Fact]
    public void InheritsFromEventArgs()
    {
        // Act
        var eventArgs = new CountChangedEventArgs();
        
        // Assert
        Assert.IsAssignableFrom<EventArgs>(eventArgs);
    }

    [Fact]
    public void InitProperties_ShouldAllowPartialInitialization()
    {
        // Act
        var eventArgs = new CountChangedEventArgs { OldValue = 5 };
        
        // Assert
        Assert.Equal(5, eventArgs.OldValue);
        Assert.Equal(0, eventArgs.NewValue);
        Assert.True(eventArgs.Timestamp <= DateTime.Now);
    }
}