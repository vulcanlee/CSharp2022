using mauiLottie.Views;

namespace mauiLottie;

public partial class App : Application
{
    public App()
    {
        InitializeComponent();

        MainPage = new SplashPage();
    }
}
