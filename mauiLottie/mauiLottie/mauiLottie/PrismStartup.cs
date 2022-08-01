using mauiLottie.Views;

namespace mauiLottie;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        #region 這裡是使用 Prism Template 建立產生的程式碼
        //builder.RegisterTypes(RegisterTypes)
        //        .OnAppStart("NavigationPage/MainPage");
        #endregion

        #region 修正這個 App 第一個要顯示的頁面不是 MainPage ，而是 SplashPage
        builder.RegisterTypes(RegisterTypes)
                .OnAppStart("SplashPage");
        #endregion
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage>()
                     .RegisterInstance(SemanticScreenReader.Default);

        #region 在這裡註冊剛剛建立的啟動頁面
        containerRegistry.RegisterForNavigation<SplashPage>();
        #endregion
    }
}
