# WeatherApp

This application is a .NET MAUI project that provides real-time weather notifications every 5 minutes.

## Setup Instructions

### Prerequisites
- [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui) installed on your development environment.
- An [OpenWeatherMap API key](https://openweathermap.org/api) to access weather data.

### Step-by-Step Setup

1. **Project**
   - Open Visual Studio (2022 or later), which has support for .NET MAUI.
   - Open the WeatherApp project.

2. **Configure the API Key**
   - Open `Services/RestService.cs` in the project.
   - Locate the API request code, and in the parameter `appid` of the request, replace it with your own API key as shown below:
     ```csharp
      var request = new RestRequest("https://api.openweathermap.org/data/2.5/weather", Method.Get)
     .AddParameter(new GetOrPostParameter("lat", deviceLocation.Latitude.ToString()))
     .AddParameter(new GetOrPostParameter("lon", deviceLocation.Longitude.ToString()))
     .AddParameter(new GetOrPostParameter("units", "metric"))
     .AddParameter(new GetOrPostParameter("appid", "{YOUR_API_KEY}")); // Add your API KEY here
     ```
   - Replace `{YOUR_API_KEY}` with the API key from OpenWeatherMap.

3. **Run the Application**
   - Build and deploy the app to an Android device or emulator, as this project currently supports only Android.

## Assumptions Made

- **Notification Behavior**: The app assumes that weather notifications should be shown as a single, ongoing foreground notification. This notification updates every 5 minutes with the latest weather status rather than creating a new notification each time.

## Known Limitations

- **iOS Implementation**: There is currently no iOS support for this app due to time constraints. This project focuses on Android, where the background and foreground services are configured to fetch data and provide real-time updates.

---

For any additional setup or troubleshooting, please refer to the official .NET MAUI [documentation](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui).
