namespace GarageDoorOpener.App.Interfaces;

public interface ISecureStorageService
{
    Task<string> GetSharedKey();
    Task UpdateSharedKey(string value);
}