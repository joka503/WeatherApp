using System.Globalization;

namespace WeatherApp.ValueConverters
{
    public class StateConverter : IValueConverter
    {
        public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            switch ((int) value)
            {
                default:
                    return "No Status";

                case 0:
                    return "Not Started";

                case 1:
                    return "Started";
            }
        }

        public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        {
            return null;
        }
    }
}
