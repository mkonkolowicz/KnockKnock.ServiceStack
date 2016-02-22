using System;
using KnockKnock.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Route("/api/v1/knocks/{Id}", "GET")]
    public class KnockGetV1 : IHasId, IReturn<KnockDto>
    {
        [ApiMember(DataType = "Guid")]
        public Guid Id { get; set; }
    }
}