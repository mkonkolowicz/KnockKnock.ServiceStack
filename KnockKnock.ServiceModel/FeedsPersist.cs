using System;
using KnockKnock.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Route("/api/v1/feeds/", "POST, PUT")]
    public class FeedsPersist : IReturn<string>
    {
        [ApiMember(DataType = "FeedDto", ParameterType = "body")]
        public FeedDto Feed { get; set; }
    }
}