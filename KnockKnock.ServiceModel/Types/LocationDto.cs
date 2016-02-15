using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel.Types
{
    public class LocationDto
    {
        [ApiMember(DataType = "double")]
        public double Latitude { get; set; }
        [ApiMember(DataType = "double")]
        public double Longitude { get; set; }
    }
}