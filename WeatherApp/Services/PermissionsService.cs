namespace WeatherApp.Services
{
    public class PermissionsService
    {
        /// <summary>
        /// Validate Location Permissions
        /// </summary>
        /// <returns></returns>
        public async Task<PermissionStatus> CheckAndRequestLocationPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.LocationWhenInUse>())
            {
                // Prompt the user with additional information as to why the permission is needed
                await Application.Current.MainPage.DisplayAlert("Alert",
                    "You need to have the give the location permission to the app so that the app can obtain your location!"
                    , "Ok");
            }

            status = await Permissions.RequestAsync<Permissions.LocationAlways>();

            return status;
        }

        /// <summary>
        /// Validate Notification Permissions
        /// </summary>
        /// <returns></returns>
        public async Task<PermissionStatus> CheckAndRequestLNotificationPermission()
        {
            PermissionStatus status = await Permissions.CheckStatusAsync<Permissions.PostNotifications>();

            if (status == PermissionStatus.Granted)
                return status;

            if (status == PermissionStatus.Denied && DeviceInfo.Platform == DevicePlatform.iOS)
            {
                // Prompt the user to turn on in settings
                // On iOS once a permission has been denied it may not be requested again from the application
                return status;
            }

            if (Permissions.ShouldShowRationale<Permissions.PostNotifications>())
            {
                // Prompt the user with additional information as to why the permission is needed
                await Application.Current.MainPage.DisplayAlert("Alert",
                    "You need to have the give the notifications permission to the app so that the app can show you notifications!"
                    , "Ok");
            }

            status = await Permissions.RequestAsync<Permissions.PostNotifications>();

            return status;
        }
    }
}
