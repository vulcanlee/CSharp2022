using CommunityToolkit.Maui.Core.Platform;

namespace mauiStatusBar.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    protected override void OnAppearing()
    {
        var foo = App.Current.MainPage;
        StatusBar.SetColor(Colors.Red);
    }
}

