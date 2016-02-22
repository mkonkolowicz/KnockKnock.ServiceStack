using System;
using System.Linq;
using KnockKnock.ServiceModel;
using KnockKnock.ServiceModel.Types;
using KnockKnockSS.ServiceInterface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MongoDB.Driver;
using NUnit.Framework;
using ServiceStack.ServiceClient.Web;
using ServiceStack.Text;
using Assert = NUnit.Framework.Assert;

namespace KnockKnock.ServiceStackTests
{
    [TestFixture]
    public class UnitTest1
    {
        [OneTimeSetUp]
        public void Populate()
        {
            ////var svc = new KnockKnockMongo();
            //var db = svc.Database<PotatoKnock>();
            //db.DeleteMany(Builders<PotatoKnock>.Filter.Empty);
            
            var knock = new KnockDto {
                FeedId = Guid.NewGuid(),
                Id = Guid.NewGuid(),
                Location = new LocationDto
                {
                    Latitude = 45,
                    Longitude = 60
                },
                Message = "Turn me into a french fry?"
            };
            using (var svc = new JsonServiceClient("http://localhost:40300/"))
            {
                var knockstr = knock.SerializeToString();
                svc.Post(new KnockPost { Knock = knock });
            }
            //svc.Any(new KnockPost {Knock = knock});

            Console.WriteLine(knock.Id);
        }

        [Test]
        public void Gets()
        {
            var svc = new KnockKnockMongo();
            var knockDtos = svc.Get(new KnocksByLocation {Latitude = 45.4, Longitude = 60.5, Radius = 75000.0});
            Assert.That(knockDtos.Any(), "Got no Knocks. Expected 1.");
        }
    }
}
