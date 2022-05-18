namespace GarageDoorOpener.App.Interfaces;

public interface IPreferenceService
{
    string GetBackendServerUrl();
    void UpdateBackendServerUrl(string value);
}