using PrismMonkey.Helpers;
using PrismMonkey.Services;
using PrismMonkey.ViewModels;
using PrismMonkey.Views;

namespace PrismMonkey;

internal static class PrismStartup
{
    public static void Configure(PrismAppBuilder builder)
    {
        builder.RegisterTypes(RegisterTypes)
                .OnAppStart($"NavigationPage/{ConstantHelper.MonkeyListPage}");
    }

    private static void RegisterTypes(IContainerRegistry containerRegistry)
    {
        containerRegistry.RegisterForNavigation<MainPage>()
                     .RegisterInstance(SemanticScreenReader.Default);

        #region 使用 Prism 來註冊相關服務
        containerRegistry.RegisterSingleton<MonkeyService>();
        containerRegistry.RegisterForNavigation<MonkeyListPage, MonkeyListPageViewModel>();
        containerRegistry.RegisterForNavigation<MonkeyDetailPage, MonkeyDetailPageViewModel>();
        #endregion
    }
}
