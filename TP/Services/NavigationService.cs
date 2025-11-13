namespace TP.Services;

public class NavigationService : INavigationService
{
    private readonly IServiceProvider _serviceProvider;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public async Task NavigateToAsync(string route, IDictionary<string, object>? parameters = null)
    {
        var shell = Shell.Current;
        if (shell == null)
        {
            System.Diagnostics.Debug.WriteLine("Shell.Current est null");
            return;
        }

        try
        {
            if (parameters != null)
            {
                await shell.GoToAsync(route, parameters);
            }
            else
            {
                await shell.GoToAsync(route);
            }
            System.Diagnostics.Debug.WriteLine($"Navigation réussie vers: {route}");
        }
        catch (Exception ex)
        {
            // Log l'erreur pour le débogage
            System.Diagnostics.Debug.WriteLine($"Erreur de navigation vers {route}: {ex.Message}");
        }
    }

    public async Task NavigateBackAsync()
    {
        var shell = Shell.Current;
        if (shell == null)
        {
            System.Diagnostics.Debug.WriteLine("Shell.Current est null pour NavigateBackAsync");
            return;
        }

        try
        {
            // Essayer la navigation retour standard
            await shell.GoToAsync("..");
            System.Diagnostics.Debug.WriteLine("Navigation retour réussie");
        }
        catch (Exception ex)
        {
            // Si la navigation retour ne fonctionne pas, essayer d'aller à HomePage
            System.Diagnostics.Debug.WriteLine($"Erreur navigation retour: {ex.Message}, tentative vers HomePage");
            try
            {
                await shell.GoToAsync("//HomePage");
            }
            catch (Exception ex2)
            {
                System.Diagnostics.Debug.WriteLine($"Erreur navigation vers HomePage: {ex2.Message}");
            }
        }
    }

    public async Task NavigateToRootAsync()
    {
        var shell = Shell.Current;
        if (shell == null) return;

        try
        {
            await shell.GoToAsync("///");
        }
        catch (Exception)
        {
            // Handle navigation errors gracefully
        }
    }
}

