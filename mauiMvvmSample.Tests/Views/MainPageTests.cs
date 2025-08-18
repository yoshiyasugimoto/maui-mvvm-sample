using Moq;
using mauiMvvmSample.ViewModels;

namespace mauiMvvmSample.Tests.Views;

[TestClass]
public class MainPageViewModelBindingTests
{
    [TestMethod]
    public void ViewModelProperties_ShouldBeAccessible()
    {
        // Arrange
        var mockCounterService = new Mock<mauiMvvmSample.Services.ICounterService>();
        mockCounterService.Setup(x => x.CurrentCount).Returns(0);
        
        // Act
        var viewModel = new MainPageViewModel(mockCounterService.Object);
        
        // Assert
        Assert.IsNotNull(viewModel);
        Assert.IsNotNull(viewModel.IncrementCounterCommand);
        Assert.IsNotNull(viewModel.ResetCounterCommand);
        Assert.AreEqual("Click me", viewModel.ButtonText);
        Assert.AreEqual(string.Empty, viewModel.AnnounceText);
    }

    [TestMethod]
    public void ViewModelNamespace_ShouldBeCorrect()
    {
        // Arrange
        var mockCounterService = new Mock<mauiMvvmSample.Services.ICounterService>();
        
        // Act
        var viewModel = new MainPageViewModel(mockCounterService.Object);
        
        // Assert
        Assert.AreEqual("mauiMvvmSample.ViewModels.MainPageViewModel", viewModel.GetType().FullName);
    }
}