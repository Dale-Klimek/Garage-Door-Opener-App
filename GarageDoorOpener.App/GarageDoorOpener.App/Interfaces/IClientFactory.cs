using GarageDoorOpener.Shared.Protos;

namespace GarageDoorOpener.App.Interfaces;

internal interface IClientFactory
{
    GarageDoor.GarageDoorClient CreateClient();
}