using System;
using Funq;
using KnockKnockSS.ServiceInterface;
using ServiceStack.Api.Swagger;
using ServiceStack.WebHost.Endpoints;
using ServiceStack.Text;

namespace KnockKnock.ServiceStack
{
    public class AppHost : AppHostBase
    {
        public AppHost() : base("KnockKnock Web Api", typeof(KnockKnockMongo).Assembly)
        {
            JsConfig<Guid>.SerializeFn = guid => guid.ToString("D");
            JsConfig<Guid>.DeSerializeFn = delegate (string strGuid)
            {
                Guid guid;
                Guid.TryParse(strGuid, out guid);
                return guid;
            };

        }

        public override void Configure(Container container)
        {
            Plugins.Add(new SwaggerFeature());
        }
    }
}