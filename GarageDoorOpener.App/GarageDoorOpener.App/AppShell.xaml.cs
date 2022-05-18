namespace GarageDoorOpener.App;

public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();
        Routing.RegisterRoute(nameof(ConfigurationPage), typeof(ConfigurationPage));
        Routing.RegisterRoute("GarageDoor", typeof(GarageDoorPage));
    }
}
