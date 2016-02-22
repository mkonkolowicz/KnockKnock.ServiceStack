using System;

namespace KnockKnock.ServiceModel.Types
{
    public class FeedDto : IHasId
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public LocationDto Location { get; set; }
    }
}