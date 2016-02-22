using System;
using KnockKnock.ServiceModel.Types;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace KnockKnockSS.ServiceInterface
{
    internal class PotatoFeed : IHasLocation
    {
        [BsonId]
        public Guid Id { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
        public string Name { get; set; }

        public PotatoFeed(FeedDto other)
        {
            Name = other.Name;
            Id = other.Id;

            Location =
                new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                    new GeoJson2DGeographicCoordinates(other.Location.Longitude, other.Location.Latitude));
        }

        public FeedDto ToDto()
        {
            return new FeedDto
            {
                Id = Id,
                Location =
                    new LocationDto
                    {
                        Latitude = this.Location.Coordinates.Latitude,
                        Longitude = this.Location.Coordinates.Longitude
                    },
                Name = Name
            };
        }
    }
}