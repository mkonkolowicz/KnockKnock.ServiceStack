using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using KnockKnock.ServiceModel;
using KnockKnock.ServiceModel.Types;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using ServiceStack.ServiceInterface;

namespace KnockKnockSS.ServiceInterface
{
    public class KnockKnockMongo : Service
    {
        public IMongoCollection<T> Database<T>(string db = "test", string collection = "KnockKnock")
        {
            var conn = ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString;
            var mongo = string.IsNullOrEmpty(conn)
                ? new MongoClient()
                : new MongoClient(conn);
            return mongo.GetDatabase(db).GetCollection<T>(collection);
        }

        private IEnumerable<T> ByLocation<T>(double longitude, double latitude, double radius) where T : IHasLocation
        {
            return Database<T>()
                .Find(Builders<T>.Filter.Near(k => k.Location,
                    new GeoJsonPoint<GeoJson2DGeographicCoordinates>(new GeoJson2DGeographicCoordinates(longitude,
                        latitude)), radius)).ToEnumerable();
        }

        public KnockDto Get(KnockGetV1 request)
        {
            return Database<PotatoKnock>().Find(Builders<PotatoKnock>.Filter.Eq(k => k._id, request.Id)).FirstOrDefault()?.ToDto();
        }

        public List<KnockDto> Get(KnocksByLocation request)
        {
            return ByLocation<PotatoKnock>(request.Longitude, request.Latitude, request.Radius)
                .Select(p => p.ToDto())
                .ToList();
        }


        public List<KnockDto> Get(KnocksByFeedId request)
        {
            return
                Database<PotatoKnock>()
                    .Find(Builders<PotatoKnock>.Filter.Eq(k => k.FeedId, request.FeedId))
                    .ToEnumerable()
                    .Select(k => k.ToDto())
                    .ToList();
        }

        public void Any(KnockPost request)
        {
            Database<PotatoKnock>()
                .FindOneAndReplace(Builders<PotatoKnock>.Filter.Eq(k => k._id, request.Knock.Id), new PotatoKnock(request.Knock),
                    new FindOneAndReplaceOptions<PotatoKnock, PotatoKnock> {IsUpsert = true});
        }

        public string Any(FeedsPersist request)
        {
            throw new NotImplementedException();
         //   Database<PotatoFeed>().FindOneAndUpdate(Builders<PotatoFeed>.Filter.Eq(f => f.FeedId, request.Feed.FeedId))

        }

        public List<FeedDto> Get(FeedsById request)
        {
            return
                Database<PotatoFeed>()
                    .Find(Builders<PotatoFeed>.Filter.Eq(f => f.FeedId, request.FeedId))
                    .ToEnumerable()
                    .Select(f => f.ToDto())
                    .ToList();
        }

        public List<FeedDto> Get(FeedsByName request)
        {
            return
                Database<PotatoFeed>()
                    .Find(Builders<PotatoFeed>.Filter.Eq(f => f.Name, request.FeedName))
                    .ToEnumerable()
                    .Select(f => f.ToDto())
                    .ToList();
        }
        public List<FeedDto> Get(FeedsByLocation request)
        {
            return
                ByLocation<PotatoFeed>(request.Longitude, request.Latitude, request.Radius)
                    .Select(f => f.ToDto())
                    .ToList();
        }
    }

    public interface IHasLocation
    {
        GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
    }

    
    public class PotatoFeed : IHasLocation
    {
        [BsonId]
        public long _id { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
        public string FeedId { get; set; }
        public string Name { get; set; }

        public PotatoFeed(FeedDto other)
        {
            Name = other.Name;
            FeedId = other.FeedId;

            Location =
                new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                    new GeoJson2DGeographicCoordinates(other.Location.Longitude, other.Location.Latitude));
        }

        public FeedDto ToDto()
        {
            return new FeedDto()
            {
                FeedId = FeedId,
                Location = new LocationDto
                {
                    Latitude = this.Location.Coordinates.Latitude,
                    Longitude = this.Location.Coordinates.Longitude
                },
                Name = Name
            };
        }
    }

    public class PotatoKnock : IHasLocation
    {
        [BsonId]
        public long _id { get; set; }

        public GeoJsonPoint<GeoJson2DGeographicCoordinates> Location { get; set; }
        public string FeedId { get; set; }
        public byte[] Content { get; set; }
        public string Message { get; set; }

        public PotatoKnock()
        {

        }

        public PotatoKnock(KnockDto other)
        {
            this._id = other.Id;
            FeedId = other.FeedId;
            Content = other.Content;
            Message = other.Message;
            Location =
                new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                    new GeoJson2DGeographicCoordinates(other.Location?.Longitude ?? 0.0, other.Location?.Latitude ?? 0.0));
        }

        public KnockDto ToDto()
        {
            return new KnockDto
            {
                Content = this.Content,
                FeedId = this.FeedId,
                Id = this._id,
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