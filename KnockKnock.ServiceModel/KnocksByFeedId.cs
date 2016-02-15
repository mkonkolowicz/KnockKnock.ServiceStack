using System.Collections.Generic;
using KnockKnock.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Route("/api/v1/feeds/{FeedId}/knocks", "GET")]
    public class KnocksByFeedId : IReturn<List<KnockDto>>
    {
        [ApiMember]
        public string FeedId { get; set; }
    }
}