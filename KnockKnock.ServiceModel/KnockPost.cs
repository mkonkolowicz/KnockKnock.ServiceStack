using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Api]
    [Route("/api/v1/knocks/", "POST, PUT")]
    public class KnockPost : IReturn<long>
    {
        [ApiMember]
        public Types.KnockDto Knock { get; set; }
    }
}
