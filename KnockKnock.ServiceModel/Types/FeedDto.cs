using MongoDB.Bson.Serialization.Attributes;

namespace KnockKnock.ServiceModel.Types
{
    public class FeedDto
    {
        [BsonId]
        public string FeedId { get; set; }
        public string Name { get; set; }
        public LocationDto Location { get; set; }
    }
}