using Android.Content;
using WeatherApp.Platforms.Android;
using WeatherApp.Platforms.Android.Services;
using WeatherApp.Services.Interfaces;

[assembly: Dependency(typeof(StartService))]
namespace WeatherApp.Platforms.Android
{
    public class StartService : IBackgroundService
    {
        /// <summary>
        /// Start the Background Service
        /// </summary>
        public void Start()
        {
            Intent startService = new Intent(MainActivity.ActivityCurrent, typeof(AndroidBackgroundService));
            startService.SetAction("START_SERVICE");
            MainActivity.ActivityCurrent.StartService(startService);
        }

        /// <summary>
        /// Stop the Background Service
        /// </summary>
        public void Stop()
        {
            Intent stopIntent = new Intent(MainActivity.ActivityCurrent, typeof(AndroidBackgroundService));
            stopIntent.SetAction("STOP_SERVICE");
            MainActivity.ActivityCurrent.StartService(stopIntent);
        }
    }
}
