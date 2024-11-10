using Android.App;
using Android.Content;
using WeatherApp.Platforms.Android;
using WeatherApp.Services.Interfaces;

[assembly: Dependency(typeof(NotificationService))]
namespace WeatherApp.Platforms.Android
{
    internal class NotificationService : INotificationService
    {
        public void CreateNotification(string text)
        {
            NotificationManager manager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(Context.NotificationService);

            Notification notification = new Notification.Builder(MainActivity.ActivityCurrent, "ServiceChannel")
                .SetContentText(text)
                .SetSmallIcon(Resource.Mipmap.appicon)
                .Build();
    
            manager.Notify(100, notification);
        }
    }
}
