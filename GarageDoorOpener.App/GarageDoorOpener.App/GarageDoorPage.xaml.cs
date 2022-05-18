using Microsoft.Extensions.Logging;

namespace GarageDoorOpener.App;

public partial class GarageDoorPage : ContentPage
{
    private readonly ILogger<GarageDoorPage> _logger;

    public GarageDoorPage(ILogger<GarageDoorPage> logger, GarageDoorViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _logger = logger;

    }

    protected override void OnSizeAllocated(double width, double height)
    {
        base.OnSizeAllocated(width, height);

        if (width > height)
        {
            _logger.LogInformation("Need to put pictures in column format");
            HorizontalGrid.IsVisible = true;
            VerticalGrid.IsVisible = false;
        }
        if (height > width)
        {
            _logger.LogInformation("Need to put pictures in row format");
            HorizontalGrid.IsVisible = false;
            VerticalGrid.IsVisible = true;
        }
    }

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync("ConfigurationPage");
    }
}
