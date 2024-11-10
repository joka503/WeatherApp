using System.Diagnostics;
using Newtonsoft.Json.Linq;
using RestSharp;
using WeatherApp.Services.Interfaces;
using WeatherApp.ViewModels;

namespace WeatherApp.Services
{
    internal class RestService
    {
        #region Variables

        private RestClient _restClient;
        private MainViewModel _mainViewModel;
        private LocationService _locationService;
        private PermissionsService _permissionsService;

        #endregion

        public RestService(MainViewModel vm, LocationService locationService, PermissionsService permissionsService)
        {
            _restClient = new RestClient();
            _mainViewModel = vm;
            _locationService = locationService;
            _permissionsService = permissionsService;
        }

        public async void GetWeatherInfo()
        {
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    PermissionStatus permissionLoc = await _permissionsService.CheckAndRequestLocationPermission();
                    if (permissionLoc == PermissionStatus.Granted)
                    {
                        PermissionStatus permissionNotification = await _permissionsService.CheckAndRequestLNotificationPermission();
                        if (permissionNotification == PermissionStatus.Granted)
                        {
                            Location deviceLocation = await _locationService.GetCurrentLocation();
                            if (deviceLocation != null)
                            {
                                var request = new RestRequest("https://api.openweathermap.org/data/2.5/weather", Method.Get)
                                    .AddParameter(new GetOrPostParameter("lat", deviceLocation.Latitude.ToString()))
                                    .AddParameter(new GetOrPostParameter("lon", deviceLocation.Longitude.ToString()))
                                    .AddParameter(new GetOrPostParameter("units", "metric"))
                                    .AddParameter(new GetOrPostParameter("appid", "")); // Add your API KEY here

                                var cancelTokenSource = new CancellationTokenSource(TimeSpan.FromMinutes(1));

                                var response = await _restClient.ExecuteAsync(request, cancelTokenSource.Token);

                                if (response.IsSuccessful)
                                {
                                    var jObject = JObject.Parse(response.Content);
                                    string temp = jObject["main"]["temp"].Value<string>();
                                    string weatherMain = jObject["weather"][0]["main"].Value<string>();

                                    if (!string.IsNullOrEmpty(temp) && !string.IsNullOrEmpty(weatherMain))
                                    {
                                        _mainViewModel.Condition = weatherMain;
                                        _mainViewModel.Temperature = temp + "ºC";
                                        _mainViewModel.Update = DateTime.Now.ToString("dd/MM/yy HH:mm:ss");
                                        DependencyService.Get<INotificationService>().CreateNotification("Temperature: " + temp + "ºC\nConditions: " + weatherMain);
                                    }
                                }
                            }
                        }
                        else
                        {
                            await Application.Current.MainPage.DisplayAlert("Alert", "No notifications permission!", "Ok");
                        }
                    }
                    else
                    {
                        await Application.Current.MainPage.DisplayAlert("Alert", "No location permission!", "Ok");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
            }
        }
    }
}
