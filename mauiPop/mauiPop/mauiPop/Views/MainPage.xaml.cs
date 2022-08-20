namespace mauiPop.Views;
using CommunityToolkit.Maui.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void Button_Clicked(object sender, EventArgs e)
    {
        var popup = new BusyView();

        this.ShowPopup(popup);
    }
}

