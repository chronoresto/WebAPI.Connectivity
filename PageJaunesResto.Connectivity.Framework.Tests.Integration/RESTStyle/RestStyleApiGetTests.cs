using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PageJaunesResto.Connectivity.Framework.Tests.Integration.RESTStyle.API;
using PageJaunesResto.WebAPI.Connectivity.Framework;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.NamingStrategies;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.Connectivity.Framework.Tests.Integration.RESTStyle
{
    [TestFixture]
    public class RestStyleApiGetTests
    {
        [Test]
        public async void try_get_request_without_parameters()
        {
            // arrange
            const string baseUri = "http://jsonplaceholder.typicode.com/";

            // act
            var result = await new RequestGenerator(baseUri, 15, new[] { new KeyValuePair<string, object>() }, new RequestBuilderCommandFactory(new DefaultRestVerbPrefixes(), new RestStyleNamingStrategy(), new JsonRequestSerializer()))
                                                        .InterfaceAndMethodToRequest<IPosts, IEnumerable<Post>>(x => x.Get());

           // assert
            Assert.That(result.Count(), Is.EqualTo(100));
        }

        [Test]
        public async void try_get_request_with_parameter()
        {
            // arrange
            const string baseUri = "http://jsonplaceholder.typicode.com/";

            // act
            var result = await new RequestGenerator(baseUri, 15, new[] { new KeyValuePair<string, object>() }, new RequestBuilderCommandFactory(new DefaultRestVerbPrefixes(), new RestStyleNamingStrategy(), new JsonRequestSerializer()))
                                                        .InterfaceAndMethodToRequest<IPosts, Post>(x => x.Get(1));

            // assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.UserId, Is.EqualTo(1));
            Assert.That(result.Title, Is.EqualTo("sunt aut facere repellat provident occaecati excepturi optio reprehenderit"));
        }
    }
}
