using KnockKnock.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Route("/api/v1/knocks/{Id}", "GET")]
    public class KnockGetV1 : IReturn<KnockDto>
    {
        [ApiMember(DataType = "long")]
        public long Id { get; set; }
    }
}