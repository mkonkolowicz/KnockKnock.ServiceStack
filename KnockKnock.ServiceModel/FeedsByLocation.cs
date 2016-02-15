using System.Collections.Generic;
using KnockKnock.ServiceModel.Types;
using ServiceStack.ServiceHost;

namespace KnockKnock.ServiceModel
{
    [Route("/api/v1/Feeds/location/", "GET")]
    public class FeedsByLocation : IReturn<List<FeedDto>> 
    {
        [ApiMember(DataType = "double", ParameterType = "query")]
        public double Latitude { get; set; }
        [ApiMember(DataType = "double", ParameterType = "query")]
        public double Longitude { get; set; }
        [ApiMember(DataType = "double", ParameterType = "query")]
        public double Radius { get; set; }
    }
}