using Microsoft.Extensions.Logging;
using mauiMvvmSample.Services;
using mauiMvvmSample.ViewModels;
using mauiMvvmSample.Views;

namespace mauiMvvmSample;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		// Services
		builder.Services.AddSingleton<ICounterService, CounterService>();

		// ViewModels
		builder.Services.AddTransient<MainPageViewModel>();

		// Views
		builder.Services.AddTransient<MainPage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
