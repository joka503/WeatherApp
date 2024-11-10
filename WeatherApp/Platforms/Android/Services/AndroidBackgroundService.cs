using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.OS;
using Java.Lang;
using WeatherApp.Services;

namespace WeatherApp.Platforms.Android.Services
{
    [Service(ForegroundServiceType = ForegroundService.TypeDataSync)]
    internal class AndroidBackgroundService : Service
    {
        #region Variables

        private Handler _handler;
        private Runnable _runnable;

        #endregion

        public override IBinder? OnBind(Intent? intent)
        {
            throw new NotImplementedException();
        }

        public override StartCommandResult OnStartCommand(Intent? intent, StartCommandFlags flags, int startId)
        {
            switch (intent.Action)
            {
                case "START_SERVICE":
                    RegisterNotification();
                    StartTimer();
                    break;

                case "STOP_SERVICE":
                    _handler.RemoveCallbacks(_runnable);
                    StopForeground(true);//Stop the service
                    StopSelfResult(startId);
                    break;
            }

            return StartCommandResult.Sticky;
        }

        /// <summary>
        /// Create Foreground Notification
        /// </summary>
        private void RegisterNotification()
        {
            NotificationChannel channel = new NotificationChannel("ServiceChannel", "WeatherApp", NotificationImportance.Max);
            NotificationManager manager = (NotificationManager)MainActivity.ActivityCurrent.GetSystemService(Context.NotificationService);
            manager.CreateNotificationChannel(channel);
            Notification notification = new Notification.Builder(this, "ServiceChannel")
                .SetContentTitle("WeatherApp")
                .SetContentText("Obtaining Weather data...")
                .SetSmallIcon(Resource.Mipmap.appicon)
                .SetOngoing(true)
                .Build();
    
            StartForeground(100, notification);
        }

        /// <summary>
        /// Start timer for request every 5 mins
        /// </summary>
        private void StartTimer()
        {
            _handler = new Handler(Looper.MainLooper);
            _runnable = new Runnable(() =>
            {
                // Perform the API request
                CreateRequest();

                // Post the runnable again after the defined interval
                _handler.PostDelayed(_runnable, 300000); // 5 minutes in milliseconds
            });

            // Start the periodic task immediately
            _handler.Post(_runnable);
        }

        private void CreateRequest()
        {
            RestService service = IPlatformApplication.Current.Services.GetService(typeof(RestService)) as RestService;
            if (service != null)
            {
                service.GetWeatherInfo();
            }
        }
    }
}
