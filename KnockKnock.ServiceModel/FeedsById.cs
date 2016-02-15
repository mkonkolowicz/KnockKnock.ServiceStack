using System.Collections.Generic;
using KnockKnock.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Route("/api/v1/Feeds/id/{FeedId}", "GET")]
    public class FeedsById : IReturn<List<FeedDto>>
    {
        [ApiMember]
        public string FeedId { get; set; }
    }
}