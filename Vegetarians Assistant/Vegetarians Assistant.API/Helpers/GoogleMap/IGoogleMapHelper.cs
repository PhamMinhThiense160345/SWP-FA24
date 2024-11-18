using Vegetarians_Assistant.Services.ModelView;

namespace Vegetarians_Assistant.API.Helpers.GoogleMap;

public interface IGoogleMapHelper
{
    double CalculateDistance(GoogleMapLocationView source, GoogleMapLocationView destination);
}
