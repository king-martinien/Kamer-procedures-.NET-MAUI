using TP.Views;

namespace TP;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();
		
        // Register routes
        Routing.RegisterRoute(nameof(SplashPage), typeof(SplashPage));
        Routing.RegisterRoute(nameof(OnboardingPage), typeof(OnboardingPage));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
        Routing.RegisterRoute(nameof(ProcedureDetailPage), typeof(ProcedureDetailPage));
        Routing.RegisterRoute(nameof(CategoryPage), typeof(CategoryPage));
        Routing.RegisterRoute(nameof(AboutPage), typeof(AboutPage));
        Routing.RegisterRoute(nameof(OfficesPage), typeof(OfficesPage));
        Routing.RegisterRoute(nameof(ExplorePage), typeof(ExplorePage));
	}
}
