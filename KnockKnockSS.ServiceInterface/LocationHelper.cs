using KnockKnock.ServiceModel.Types;
using MongoDB.Driver.GeoJsonObjectModel;

namespace KnockKnockSS.ServiceInterface
{
    internal static class LocationHelper
    {
        public static GeoJsonPoint<GeoJson2DGeographicCoordinates> ToGeoJsonPoint(double longitude, double latitude)
        {
            return new LocationDto {Latitude = latitude, Longitude = longitude}.ToGeoJsonPoint();
        }

        public static GeoJsonPoint<GeoJson2DGeographicCoordinates> ToGeoJsonPoint(this LocationDto loc)
        {
            return
                new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                    new GeoJson2DGeographicCoordinates(loc?.Longitude ?? 0.0, loc?.Latitude ?? 0.0));
        }
    }
}