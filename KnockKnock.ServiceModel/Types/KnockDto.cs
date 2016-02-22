using System;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel.Types
{
    public class KnockDto : IHasId
    {
        [ApiMember(DataType = "Guid")]
        public Guid FeedId { get; set; }

        [ApiMember(DataType = "byte[]")]
        public byte[] Content { get; set; }

        [ApiMember]
        public string Message { get; set; }

        [ApiMember(DataType = "LocationDto")]
        public LocationDto Location { get; set; }

        [ApiMember(DataType = "Guid")]
        public Guid Id { get; set; }
    }
}