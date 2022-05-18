using GarageDoorOpener.App.Interfaces;
using GarageDoorOpener.Client.Interfaces;
using GarageDoorOpener.Shared.Protos;

namespace GarageDoorOpener.App.Services;

internal class GarageDoorService : IGarageDoorService
{
    private readonly GarageDoor.GarageDoorClient _client;
    private readonly ISecureStorageService _secureStorageService;
    private readonly IAuthenticatorService _authenticatorService;

    public GarageDoorService(IClientFactory factory, ISecureStorageService secureStorageService, IAuthenticatorService authenticatorService)
    {
        _client = factory.CreateClient();
        _secureStorageService = secureStorageService;
        _authenticatorService = authenticatorService;
    }

    public async Task SignalLeftDoor()
    {
        var request = new SignalDoorRequest() { OpenLeftDoor = new SignalLeftDoor(), AuthenticationCode = await GetCode() };
        await _client.SignalDoorAsync(request).ConfigureAwait(false);
    }

    public async Task SignalRightDoor()
    {
        var request = new SignalDoorRequest { OpenRightDoor = new SignalRightDoor(), AuthenticationCode = await GetCode() };
        await _client.SignalDoorAsync(request).ConfigureAwait(false);
    }

    private async Task<string> GetCode()
    {
        var key = await _secureStorageService.GetSharedKey();
        if (string.IsNullOrWhiteSpace(key))
            throw new ArgumentException("The shared key cannot be empty.");
        return _authenticatorService.GetKey(key);
    }
}
