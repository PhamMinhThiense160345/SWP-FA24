using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.API.Helpers.GoogleMap;

public class GoogleMapHelper : IGoogleMapHelper
{
    private const double EarthRadiusKm = 6378;

    public double CalculateDistance(GoogleMapLocationView source, GoogleMapLocationView destination)
    {
        // Convert degrees to radians
        double lat1Rad = DegreesToRadians(source.Latitude);
        double lon1Rad = DegreesToRadians(source.Longtitude);
        double lat2Rad = DegreesToRadians(destination.Latitude);
        double lon2Rad = DegreesToRadians(destination.Longtitude);

        // Differences in coordinates
        double dLat = lat2Rad - lat1Rad;
        double dLon = lon2Rad - lon1Rad;

        // Haversine formula
        double a = Math.Pow(Math.Sin(dLat / 2), 2) + Math.Cos(lat1Rad) * Math.Cos(lat2Rad) * Math.Pow(Math.Sin(dLon / 2), 2);
        double c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));

        // Calculate the distance
        var distance = EarthRadiusKm * c;
        return Math.Round(distance);
    }

    // Convert degrees to radians
    private static double DegreesToRadians(double degrees)
    {
        return degrees * (Math.PI / 180);
    }
}
