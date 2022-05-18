using GarageDoorOpener.App.Interfaces;

namespace GarageDoorOpener.App.Services;

internal class SecureStorageService : ISecureStorageService
{
    private const string SharedKey = nameof(SharedKey);

    public SecureStorageService()
    {
    }

    public async Task<string> GetSharedKey()
    {
        return await SecureStorage.GetAsync(SharedKey);
    }

    public async Task UpdateSharedKey(string value)
    {
        await SecureStorage.SetAsync(SharedKey, value);
    }
}
