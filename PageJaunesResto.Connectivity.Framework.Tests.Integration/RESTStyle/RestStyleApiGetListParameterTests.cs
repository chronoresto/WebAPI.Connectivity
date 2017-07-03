using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using PageJaunesResto.WebAPI.Connectivity.Framework;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.NamingStrategies;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.Connectivity.Framework.Tests.Integration.RESTStyle
{
    public class RestoItem
    {
        public Guid Id { get; set; }
        public int NoGroods { get; set; }
        public int NoReviews { get; set; }
        public bool IsPreOrder { get; set; }
        public bool IsClosed { get; set; }
        public string Name { get; set; }
        public string[] Categories { get; set; }
        public string RestuarantImageUrl { get; set; }
        public decimal Rating { get; set; }
        public decimal RatingDelivery { get; set; }
        public decimal RatingQuality { get; set; }
        public int MinutesUntilNextDelivery { get; set; }
        public string AddressLine1 { get; set; }
        public string AddressLine2 { get; set; }
        public string RestaurantUrl { get; set; }
    }
    public class GroodDiscountLevelFlattenedMini
    {
        public int NoConfirmedGrooders { get; set; }
        public int DiscountPercentage { get; set; }
        public decimal MinOrderPrice { get; set; }
    }

    public class RestoGroodItem : RestoItem
    {
        public RestoGroodItem()
        {
            this.GroodDiscountLevels = new List<GroodDiscountLevelFlattenedMini>();
        }
        public List<GroodDiscountLevelFlattenedMini> GroodDiscountLevels { get; set; }
    }

    public interface IRestaurantGroodInfoController
    {
        IEnumerable<RestoGroodItem> Get(IEnumerable<Guid>restaurantIds);
    }

    public class GroodApiRequestGenerator : RequestGenerator
    {
        // TODO: Alter Ip address to your local machine
        public GroodApiRequestGenerator()
            : base("http://192.168.0.18:1250/api/", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("ApiKey", "EE9C8401-1436-4957-A464-2212718B6FBF") },
                new RequestBuilderCommandFactory(new TraditionServiceDefaultVerbPrefixes(), new TraditionalStyleNamingStrategy(), new JsonRequestSerializer()))
        {
        }
    }

    [TestFixture]
    public class RestStyleApiGetListParameterTests
    {
        private List<Guid> _restaurantIds = new List<Guid>
        {
            // TODO: Add restaurant Ids here
            //Guid.Parse(""),            
            Guid.NewGuid(),
        };

        [Test]
        public async void try_get_with_parameters()
        {
            var res = await new GroodApiRequestGenerator()
                .InterfaceAndMethodToRequest<IRestaurantGroodInfoController, IEnumerable<RestoGroodItem>>(
                    x => x.Get(_restaurantIds.ToArray()), 150);

            Assert.That(res.First().AddressLine1, Is.Not.Null);
        }
    }
}
