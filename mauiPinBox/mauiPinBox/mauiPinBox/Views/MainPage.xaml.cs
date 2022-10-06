namespace mauiPinBox.Views;

public partial class MainPage : ContentPage
{
    public MainPage()
    {
        InitializeComponent();
    }

    private void PinBox_Completed(object sender, TemplateUI.Controls.PinCompletedEventArgs e)
    {
        DisplayAlert("PinBox", $"Pin completed: {e.Password}", "Ok");
    }
}

