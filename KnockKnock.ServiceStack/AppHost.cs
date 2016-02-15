using Funq;
using KnockKnockSS.ServiceInterface;
using ServiceStack.Api.Swagger;
using ServiceStack.WebHost.Endpoints;

namespace KnockKnock.ServiceStack
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("KnockKnock Web Api", typeof(KnockKnockMongo).Assembly)
        {
        }

        public override void Configure(Container container)
        {
            Plugins.Add(new SwaggerFeature());
        }
    }
}