using System.Collections.Generic;
using KnockKnock.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Route("/api/v1/feeds/name/{FeedName}", "GET")]
    public class FeedsByName : IReturn<List<FeedDto>>
    {
        [ApiMember]
        public string FeedName { get; set; }
    }
}