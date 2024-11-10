using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WeatherApp.Enumerables;
using WeatherApp.Services;
using WeatherApp.Services.Interfaces;

namespace WeatherApp.ViewModels
{
    public partial class MainViewModel : ObservableObject
    {

        #region Variables

        [ObservableProperty]
        string temperature;

        [ObservableProperty]
        string condition;

        [ObservableProperty]
        int status;

        [ObservableProperty]
        string update;

        private PermissionsService _permissionsService;

        #endregion

        public MainViewModel(PermissionsService permissionsService)
        {
            Status = (int)AppState.NotStarted;
            _permissionsService = permissionsService;
        }

        [RelayCommand]
        async void Start()
        {
            try
            {
                if (Status != (int) AppState.Started)
                {
                    if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                    {
                        PermissionStatus permissionLoc = await _permissionsService.CheckAndRequestLocationPermission();
                        if (permissionLoc == PermissionStatus.Granted)
                        {
                            PermissionStatus permissionNotification =
                                await _permissionsService.CheckAndRequestLNotificationPermission();
                            if (permissionNotification == PermissionStatus.Granted)
                            {
                                Status = (int) AppState.Started;
                                DependencyService.Get<IBackgroundService>().Start();
                            }
                            else
                            {
                                await Application.Current.MainPage.DisplayAlert("Alert", "Not notifications permission!\nPlease go to the app settings and set the notifications permission on!", "Ok");
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "Not location permission!\nPlease go to the app settings and set the location permission to always on!", "Ok");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "Not connected to the internet!", "Ok");
                    }
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert("Alert", "Service already running!", "Ok");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                Status = (int) AppState.NotStarted;
                await Application.Current.MainPage.DisplayAlert("Alert", "Could not start the service!", "Ok");
            }
        }

        [RelayCommand]
        void Stop()
        {
            if (Status == (int) AppState.Started)
            {
                Status = (int) AppState.NotStarted;
                DependencyService.Get<IBackgroundService>().Stop();
            }
        }
    }
}
