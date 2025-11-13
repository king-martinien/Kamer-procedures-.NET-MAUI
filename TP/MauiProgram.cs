using Microsoft.Extensions.Logging;
using TP.Services;
using TP.ViewModels;
using TP.Views;

namespace TP;

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

		// Register Services
		builder.Services.AddSingleton<INavigationService, NavigationService>();
		builder.Services.AddSingleton<IStorageService, StorageService>();
		builder.Services.AddSingleton<IProcedureService, ProcedureService>();

		// Register ViewModels
		builder.Services.AddTransient<SplashViewModel>();
		builder.Services.AddTransient<OnboardingViewModel>();
		builder.Services.AddTransient<HomeViewModel>();
		builder.Services.AddTransient<ProcedureDetailViewModel>();
		builder.Services.AddTransient<CategoryViewModel>();
		builder.Services.AddTransient<AboutViewModel>();
		builder.Services.AddTransient<OfficesViewModel>();
		builder.Services.AddTransient<ExploreViewModel>();

		// Register Views
		builder.Services.AddTransient<SplashPage>();
		builder.Services.AddTransient<OnboardingPage>();
		builder.Services.AddTransient<HomePage>();
		builder.Services.AddTransient<ProcedureDetailPage>();
		builder.Services.AddTransient<CategoryPage>();
		builder.Services.AddTransient<AboutPage>();
		builder.Services.AddTransient<OfficesPage>();
		builder.Services.AddTransient<ExplorePage>();

#if DEBUG
		builder.Logging.AddDebug();
#endif

		return builder.Build();
	}
}
