using Moq;
using mauiMvvmSample.ViewModels;

namespace mauiMvvmSample.Tests.Views;

public class MainPageViewModelBindingTests
{
    [Fact]
    public void ViewModelProperties_ShouldBeAccessible()
    {
        // Arrange
        var mockCounterService = new Mock<mauiMvvmSample.Services.ICounterService>();
        mockCounterService.Setup(x => x.CurrentCount).Returns(0);
        
        // Act
        var viewModel = new MainPageViewModel(mockCounterService.Object);
        
        // Assert
        Assert.NotNull(viewModel);
        Assert.NotNull(viewModel.IncrementCounterCommand);
        Assert.NotNull(viewModel.ResetCounterCommand);
        Assert.Equal("Click me", viewModel.ButtonText);
        Assert.Equal(string.Empty, viewModel.AnnounceText);
    }

    [Fact]
    public void ViewModelNamespace_ShouldBeCorrect()
    {
        // Arrange
        var mockCounterService = new Mock<mauiMvvmSample.Services.ICounterService>();
        
        // Act
        var viewModel = new MainPageViewModel(mockCounterService.Object);
        
        // Assert
        Assert.Equal("mauiMvvmSample.ViewModels.MainPageViewModel", viewModel.GetType().FullName);
    }
}