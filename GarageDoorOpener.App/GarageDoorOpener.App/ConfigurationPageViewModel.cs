using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GarageDoorOpener.App.Interfaces;

using Microsoft.Extensions.Logging;

namespace GarageDoorOpener.App;

public class ConfigurationPageViewModel : ObservableObject
{
    private readonly ILogger<ConfigurationPageViewModel> _logger;
    private readonly IPreferenceService _preferenceService;
    private readonly ISecureStorageService _secureStorageService;
    private string sharedKey;
    private string serverUrl;


    public ConfigurationPageViewModel(ILogger<ConfigurationPageViewModel> logger, IPreferenceService preferenceService, ISecureStorageService secureStorageService)
    {
        _logger = logger;
        _preferenceService = preferenceService;
        _secureStorageService = secureStorageService;
        Update = new AsyncRelayCommand(UpdateAsync);
    }

    public string SharedKey 
    { 
        get => sharedKey; 
        set => SetProperty(ref sharedKey, value); 
    }

    public string ServerUrl 
    { 
        get => serverUrl; 
        set => SetProperty(ref serverUrl, value); 
    }

    public IAsyncRelayCommand Update { get; }

    public async Task Initialize()
    {
        var task = _secureStorageService.GetSharedKey();
        ServerUrl = _preferenceService.GetBackendServerUrl();
        SharedKey = await task;
    }

    public async Task UpdateAsync()
    {
        if (string.IsNullOrWhiteSpace(ServerUrl))
        {
            await Toast.Make("The server url cannot be empty", ToastDuration.Long).Show();
            return;
        }
        if (string.IsNullOrWhiteSpace(SharedKey))
        {
            await Toast.Make("The shared key cannot be empty", ToastDuration.Long).Show();
            return;
        }

        try
        {
            var task = _secureStorageService.UpdateSharedKey(SharedKey);
            _preferenceService.UpdateBackendServerUrl(ServerUrl);
            await task;
            await Shell.Current.GoToAsync("GarageDoor");  //not sure I should be doing this from the view model?
        }
        catch(Exception e)
        {
            _logger.LogError(e,"");
        }
    }
}
