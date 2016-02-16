using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Api]
    [Route("/api/v1/knocks/", "POST, PUT")]
    public class KnockPost : IReturn<long>
    {
        [ApiMember(DataType = "KnockDto", ParameterType = "body", Description = "The Knock to save.", Name="Knock")]
        public Types.KnockDto Knock { get; set; }
    }
}