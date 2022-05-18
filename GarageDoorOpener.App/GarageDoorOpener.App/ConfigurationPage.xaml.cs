namespace GarageDoorOpener.App;

public partial class ConfigurationPage : ContentPage
{
    public ConfigurationPage(ConfigurationPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if(BindingContext is ConfigurationPageViewModel vm)
        {
            await vm.Initialize();
        }
    }
}