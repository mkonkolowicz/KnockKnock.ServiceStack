using System;
using MongoDB.Bson.Serialization.Attributes;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel.Types
{
    public interface IHasId
    {
        long Id { get; set; }
    }

    public class KnockDto : IHasId
    {
        [ApiMember]
        public string FeedId { get; set; }
        [ApiMember (DataType = "byte[]")]
        public byte[] Content { get; set; }
        [ApiMember]
        public string Message { get; set; }
        [ApiMember (DataType = "LocationDto")]
        public LocationDto Location { get; set; }
        [BsonId]
        [ApiMember(DataType = "long")]
        public long Id { get; set; }
    }
}