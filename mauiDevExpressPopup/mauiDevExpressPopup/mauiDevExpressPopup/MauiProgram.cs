using DevExpress.Maui;

namespace mauiDevExpressPopup;

public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UsePrismApp<App>(PrismStartup.Configure)
            // This method adds handlers for all DevExpress components.
            .UseDevExpress()
            /* You can also use the code below to add the DXPopup handler.
            .ConfigureMauiHandlers(handlers => {
                handlers.AddHandler<DXPopup, DXPopupHandler>();
            })
            */
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            });

        return builder.Build();
    }
}
