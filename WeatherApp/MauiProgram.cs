using Microsoft.Extensions.Logging;
using WeatherApp.Enumerables;
using WeatherApp.Services;
using WeatherApp.ViewModels;

namespace WeatherApp
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
    		builder.Logging.AddDebug();
#endif

            #region Singletons

            builder.Services.AddSingleton<MainPage>();
            builder.Services.AddSingleton<MainViewModel>();
            builder.Services.AddSingleton<RestService>();
            builder.Services.AddSingleton<LocationService>();
            builder.Services.AddSingleton<PermissionsService>();

            #endregion

            

            return builder.Build();
        }
    }
}
