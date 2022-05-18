using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;

using Microsoft.Extensions.Logging;
using GarageDoorOpener.App.Interfaces;

namespace GarageDoorOpener.App;

public class GarageDoorViewModel : ObservableObject
{
    private readonly ILogger<GarageDoorViewModel> _logger;
    private readonly IGarageDoorService _garageDoorService;
    private Color leftBackgroundColor = new(0, 255, 0);
    private Color rightBackgroundColor = new(0, 255, 0);
    private bool leftDoorIsRunning = false;
    private bool rightDoorIsRunning = false;

    public GarageDoorViewModel(ILogger<GarageDoorViewModel> logger, IGarageDoorService garageDoorService)
    {
        _logger = logger;
        _garageDoorService = garageDoorService;
        SignalLeftDoor = new AsyncRelayCommand<bool>(SendLeftDoorRequest);
        SignalRightDoor = new AsyncRelayCommand<bool>(SendRightDoorRequest);
    }

    public IAsyncRelayCommand SignalLeftDoor { get; }
    public IAsyncRelayCommand SignalRightDoor { get; }

    public bool LeftDoorIsRunning
    {
        get => leftDoorIsRunning;
        set => SetProperty(ref leftDoorIsRunning, value);
    }

    public bool RightDoorIsRunning 
    { 
        get => rightDoorIsRunning; 
        set => SetProperty(ref rightDoorIsRunning, value); 
    }

    public Color LeftBackgroundColor
    {
        get => leftBackgroundColor;
        set => SetProperty(ref leftBackgroundColor, value);
    }

    public Color RightBackgroundColor
    {
        get => rightBackgroundColor;
        set => SetProperty(ref rightBackgroundColor, value);
    }


    //have to pass in a parameter to show if this is running and exit if it is
    //there seems to be a problem that when the button is disabled that the command still fires
    private async Task SendLeftDoorRequest(bool isRunning)
    {
        // doing something simple initially but not fool proof
        if (isRunning || LeftDoorIsRunning)
            return;
        LeftDoorIsRunning = true;
        LeftBackgroundColor = new Color(255, 0, 0);
        try
        {
            await _garageDoorService.SignalLeftDoor();
            //await Task.Delay(10000);

        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message, ToastDuration.Long).Show();
            _logger.LogError(ex, "failed");
        }
        finally
        {
            LeftBackgroundColor = new Color(0, 255, 0);
            LeftDoorIsRunning = false;
        }
    }

    private async Task SendRightDoorRequest(bool isRunning)
    {
        if (isRunning || RightDoorIsRunning)
            return;
        RightDoorIsRunning = true;
        RightBackgroundColor = new Color(255, 0, 0);
        try
        {
            await _garageDoorService.SignalRightDoor();
            //await Task.Delay(10000);

        }
        catch (Exception ex)
        {
            await Toast.Make(ex.Message, ToastDuration.Long).Show();
            _logger.LogError(ex, "failed");
        }
        finally
        {
            RightBackgroundColor = new Color(0, 255, 0);
            RightDoorIsRunning = false;
        }
    }
}

