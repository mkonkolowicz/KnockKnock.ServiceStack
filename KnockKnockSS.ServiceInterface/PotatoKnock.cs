using System;
using KnockKnock.ServiceModel.Types;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver.GeoJsonObjectModel;

namespace KnockKnockSS.ServiceInterface
{
    internal class PotatoKnock : IHasLocation
    {
        [BsonId]
        public Guid Id { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
        public Guid FeedId { get; set; }
        public byte[] Content { get; set; }
        public string Message { get; set; }

        public PotatoKnock()
        {

        }

        public PotatoKnock(KnockDto other)
        {
            this.Id = other.Id;
            FeedId = other.FeedId;
            Content = other.Content;
            Message = other.Message;
            Location = other.Location.ToGeoJsonPoint();

        }

        public KnockDto ToDto()
        {
            return new KnockDto
            {
                Content = this.Content,
                FeedId = this.FeedId,
                Id = this.Id,
                Location =
                    new LocationDto
                    {
                        Latitude = this.Location.Coordinates.Latitude,
                        Longitude = this.Location.Coordinates.Longitude
                    },
                Message = this.Message
            };
        }
    }
}