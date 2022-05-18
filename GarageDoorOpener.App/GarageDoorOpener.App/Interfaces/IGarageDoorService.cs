namespace GarageDoorOpener.App.Interfaces;

public interface IGarageDoorService
{
    Task SignalLeftDoor();
    Task SignalRightDoor();
}