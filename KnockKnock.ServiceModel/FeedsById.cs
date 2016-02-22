using System;
using System.Collections.Generic;
using KnockKnock.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Route("/api/v1/Feeds/id/{Id}", "GET")]
    public class FeedsById : IHasId, IReturn<List<FeedDto>>
    {
        [ApiMember(DataType = "Guid")]
        public Guid Id { get; set; }
    }
}