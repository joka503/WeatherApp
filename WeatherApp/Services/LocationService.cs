namespace WeatherApp.Services
{
    internal class LocationService
    {
        /// <summary>
        /// Get Current GPS Location
        /// </summary>
        /// <returns></returns>
        public async Task<Location> GetCurrentLocation()
        {
            try
            {
                GeolocationRequest request = new GeolocationRequest(GeolocationAccuracy.Best, TimeSpan.FromMinutes(1));

                Location location = await Geolocation.Default.GetLocationAsync(request);

                return location;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}
