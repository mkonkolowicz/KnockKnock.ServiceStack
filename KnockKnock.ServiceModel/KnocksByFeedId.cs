using System;
using System.Collections.Generic;
using KnockKnock.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Route("/api/v1/feeds/{Id}/knocks", "GET")]
    public class KnocksByFeedId : IReturn<List<KnockDto>>
    {
        [ApiMember(DataType = "Guid")]
        public Guid Id { get; set; }
    }
}