using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using KnockKnock.ServiceModel;
using KnockKnock.ServiceModel.Types;
using MongoDB.Driver;
using ServiceStack.ServiceInterface;

namespace KnockKnockSS.ServiceInterface
{
    public class KnockKnockMongo : Service
    {
        public IMongoCollection<T> Database<T>(string collection, string db = "test")
        {
            var conn = ConfigurationManager.ConnectionStrings["Mongo"].ConnectionString;
            var mongo = string.IsNullOrEmpty(conn)
                ? new MongoClient()
                : new MongoClient(conn);
            return mongo.GetDatabase(db).GetCollection<T>(collection);
        }

        private IEnumerable<T> ByLocation<T>(string collection, double longitude, double latitude, double radius)
            where T : IHasLocation
        {
            return
                Database<T>(collection)
                    .Find(Builders<T>.Filter.Near(k => k.Location, LocationHelper.ToGeoJsonPoint(longitude, latitude),
                        radius))
                    .ToEnumerable();
        }

        public KnockDto Get(KnockGetV1 request)
        {
            return
                Database<PotatoKnock>("Knocks")
                    .Find(Builders<PotatoKnock>.Filter.Eq(k => k.Id, request.Id))
                    .FirstOrDefault()?
                    .ToDto();
        }

        public List<KnockDto> Get(KnocksByLocation request)
        {
            return
                ByLocation<PotatoKnock>("Knocks", request.Longitude, request.Latitude, request.Radius)
                    .Select(p => p.ToDto())
                    .ToList();
        }

        public List<KnockDto> Get(KnocksByFeedId request)
        {
            return
                Database<PotatoKnock>("Knocks")
                    .Find(Builders<PotatoKnock>.Filter.Eq(k => k.FeedId, request.Id))
                    .ToEnumerable()
                    .Select(k => k.ToDto())
                    .ToList();
        }

        public void Any(KnockPost request)
        {
            Database<PotatoKnock>("Knocks")
                .FindOneAndReplace(Builders<PotatoKnock>.Filter.Eq(k => k.Id, request.Knock.Id),
                    new PotatoKnock(request.Knock),
                    new FindOneAndReplaceOptions<PotatoKnock, PotatoKnock> {IsUpsert = true});
        }

        public string Any(FeedsPersist request)
        {
            Database<PotatoFeed>("Feeds")
                .FindOneAndUpdate(Builders<PotatoFeed>.Filter.Eq(f => f.Id, request.Feed.Id),
                    Builders<PotatoFeed>.Update.Set(f => f.Location, request.Feed.Location.ToGeoJsonPoint())
                        .Set(f => f.Name, request.Feed.Name)
                        .SetOnInsert(f => f.Id, request.Feed.Id),
                    new FindOneAndUpdateOptions<PotatoFeed> {IsUpsert = true});
            return request.Feed.Name;
        }

        public List<FeedDto> Get(FeedsById request)
        {
            return
                Database<PotatoFeed>("Feeds")
                    .Find(Builders<PotatoFeed>.Filter.Eq(f => f.Id, request.Id))
                    .ToEnumerable()
                    .Select(f => f.ToDto())
                    .ToList();
        }

        public List<FeedDto> Get(FeedsByName request)
        {
            return
                Database<PotatoFeed>("Feeds")
                    .Find(Builders<PotatoFeed>.Filter.Eq(f => f.Name, request.FeedName))
                    .ToEnumerable()
                    .Select(f => f.ToDto())
                    .ToList();
        }

        public List<FeedDto> Get(FeedsByLocation request)
        {
            return
                ByLocation<PotatoFeed>("Feeds", request.Longitude, request.Latitude, request.Radius)
                    .Select(f => f.ToDto())
                    .ToList();
        }
    }
}