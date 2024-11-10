using Android.App;
using Android.Content.PM;
using Android.OS;
using WeatherApp.Platforms.Android;
using WeatherApp.Services.Interfaces;

namespace WeatherApp
{
    [Activity(Theme = "@style/Maui.SplashTheme", MainLauncher = true, LaunchMode = LaunchMode.SingleTop, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize | ConfigChanges.Density)]
    public class MainActivity : MauiAppCompatActivity
    {
        public static MainActivity ActivityCurrent { get; set; }

        public MainActivity()
        {
            ActivityCurrent = this;
            DependencyService.Register<IBackgroundService, StartService>();
            DependencyService.Register<INotificationService, NotificationService>();
        }
    }
}
