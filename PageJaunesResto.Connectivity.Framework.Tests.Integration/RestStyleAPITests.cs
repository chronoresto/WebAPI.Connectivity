using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using PageJaunesResto.WebAPI.Connectivity.Framework;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.Connectivity.Framework.Tests.Integration
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
            var result = await new RequestGenerator(baseUri, new RequestBuilderCommandFactory(new DefaultVerbPrefixes(), new RestStyleNamingStrategy()))
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
            var result = await new RequestGenerator(baseUri, new RequestBuilderCommandFactory(new DefaultVerbPrefixes(), new RestStyleNamingStrategy()))
                                                        .InterfaceAndMethodToRequest<IPosts, Post>(x => x.Get(1));

            // assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.UserId, Is.EqualTo(1));
            Assert.That(result.Title, Is.EqualTo("sunt aut facere repellat provident occaecati excepturi optio reprehenderit"));
        }
    }

    [TestFixture]
    public class RestStyleApiDeleteTests
    {
        [Test]
        public async void try_delete_request_without_parameter()
        {
            // arrange
            const string baseUri = "http://jsonplaceholder.typicode.com/";

            // act
            await new RequestGenerator(baseUri, new RequestBuilderCommandFactory(new DefaultVerbPrefixes(), new RestStyleNamingStrategy()))
                                                        .InterfaceAndMethodToRequest<IPosts>(x => x.Delete());
        }
    }



    public class Post
    {
        public int UserId { get; set; }
        public int Id { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
    }

    public interface IPosts
    {
        IEnumerable<Post> Get();
        Post Get(int id);
        // todo Post Post(int id);
        string Delete();
    }
}
