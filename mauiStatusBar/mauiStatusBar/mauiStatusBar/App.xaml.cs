using mauiStatusBar.Views;

namespace mauiStatusBar;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();
    }

    //protected override Window CreateWindow(IActivationState activationState)
    //{
    //    // Workaround for: 'Either set MainPage or override CreateWindow.'??
    //    if (this.MainPage == null)
    //    {
    //        this.MainPage = new EmptyPage();
    //    }

    //    return base.CreateWindow(activationState);
    //}
}
