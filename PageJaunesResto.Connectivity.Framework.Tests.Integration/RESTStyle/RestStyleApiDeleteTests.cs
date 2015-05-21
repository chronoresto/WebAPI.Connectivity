using System.Collections.Generic;
using NUnit.Framework;
using PageJaunesResto.Connectivity.Framework.Tests.Integration.RESTStyle.API;
using PageJaunesResto.WebAPI.Connectivity.Framework;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.NamingStrategies;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.Connectivity.Framework.Tests.Integration.RESTStyle
{
    [TestFixture]
    public class RestStyleApiDeleteTests
    {
        [Test]
        public async void try_delete_request_without_parameter()
        {
            // arrange
            const string baseUri = "http://jsonplaceholder.typicode.com/";

            // act
            await new RequestGenerator(baseUri, 15, new [] { new KeyValuePair<string, object>() }, new RequestBuilderCommandFactory(new DefaultRestVerbPrefixes(), new RestStyleNamingStrategy(), new JsonRequestSerializer()))
                .InterfaceAndMethodToRequest<IPosts>(x => x.Delete());
        }
    }
}