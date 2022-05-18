using GarageDoorOpener.App.Interfaces;

namespace GarageDoorOpener.App.Services;

internal class PreferenceService : IPreferenceService
{
    private const string BackendServer = nameof(BackendServer);
    public PreferenceService()
    {

    }

    public string GetBackendServerUrl()
    {
        return Preferences.Get(BackendServer, "https://localhost:8080");    //fake url
    }

    public void UpdateBackendServerUrl(string value)
    {
        Preferences.Set(BackendServer, value);
    }
}
