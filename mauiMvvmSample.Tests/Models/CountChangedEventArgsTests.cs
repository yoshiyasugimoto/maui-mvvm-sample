using mauiMvvmSample.Services;

namespace mauiMvvmSample.Tests.Models;

[TestClass]
public class CountChangedEventArgsTests
{
    [TestMethod]
    public void Constructor_ShouldInitializeWithDefaultValues()
    {
        // Act
        var eventArgs = new CountChangedEventArgs();
        
        // Assert
        Assert.AreEqual(0, eventArgs.OldValue);
        Assert.AreEqual(0, eventArgs.NewValue);
        Assert.IsTrue(eventArgs.Timestamp <= DateTime.Now);
        Assert.IsTrue(eventArgs.Timestamp >= DateTime.Now.AddSeconds(-1));
    }

    [TestMethod]
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
        Assert.AreEqual(10, eventArgs.OldValue);
        Assert.AreEqual(15, eventArgs.NewValue);
        Assert.AreEqual(expectedTimestamp, eventArgs.Timestamp);
    }

    [TestMethod]
    public void OldValue_ShouldAcceptNegativeValues()
    {
        // Act
        var eventArgs = new CountChangedEventArgs { OldValue = -5 };
        
        // Assert
        Assert.AreEqual(-5, eventArgs.OldValue);
    }

    [TestMethod]
    public void NewValue_ShouldAcceptNegativeValues()
    {
        // Act
        var eventArgs = new CountChangedEventArgs { NewValue = -10 };
        
        // Assert
        Assert.AreEqual(-10, eventArgs.NewValue);
    }

    [TestMethod]
    public void Values_ShouldAcceptLargeNumbers()
    {
        // Act
        var eventArgs = new CountChangedEventArgs
        {
            OldValue = int.MaxValue,
            NewValue = int.MinValue
        };
        
        // Assert
        Assert.AreEqual(int.MaxValue, eventArgs.OldValue);
        Assert.AreEqual(int.MinValue, eventArgs.NewValue);
    }

    [TestMethod]
    public void Timestamp_ShouldBeUpdatedAutomatically()
    {
        // Arrange
        var startTime = DateTime.Now;
        
        // Act
        var eventArgs = new CountChangedEventArgs();
        var endTime = DateTime.Now;
        
        // Assert
        Assert.IsTrue(eventArgs.Timestamp >= startTime);
        Assert.IsTrue(eventArgs.Timestamp <= endTime);
    }

    [TestMethod]
    public void InheritsFromEventArgs()
    {
        // Act
        var eventArgs = new CountChangedEventArgs();
        
        // Assert
        Assert.IsInstanceOfType(eventArgs, typeof(EventArgs));
    }

    [TestMethod]
    public void InitProperties_ShouldAllowPartialInitialization()
    {
        // Act
        var eventArgs = new CountChangedEventArgs { OldValue = 5 };
        
        // Assert
        Assert.AreEqual(5, eventArgs.OldValue);
        Assert.AreEqual(0, eventArgs.NewValue);
        Assert.IsTrue(eventArgs.Timestamp <= DateTime.Now);
    }
}