using MongoDB.Driver.GeoJsonObjectModel;

namespace KnockKnockSS.ServiceInterface
{
    public interface IHasLocation
    {
        GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
    }
}