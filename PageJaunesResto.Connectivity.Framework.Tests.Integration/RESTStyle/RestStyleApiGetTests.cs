﻿using System;
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
    public class ResponseBase<T>
    {
        public T Result { get; set; }
    }

    public class RestaurantResponse
    {
        public string Address { get; set; }
    }

    public interface ICatalogController
    {
        ResponseBase<RestaurantResponse[]> GetRestaurants(string placeSlug = null, int? serviceId = null,
            string categorieSlugs = null, string occasionSlugs = null, string convictionSlugs = null,
            string departmentSlug = null, System.DateTime? desiredDate = null, int? pageNumber = 1, int? pageSize = 20,
            int? sort = null, string filter = null, int? imageWidth = null, int? imageHeight = null,
            string imageBackground = null, string imageAlignment = "center");
    }


    public class PagesJaunesApiRequestGenerator : RequestGenerator
    {
        public PagesJaunesApiRequestGenerator()
            : base("http://api.chronoresto.com/api/", new List<KeyValuePair<string, object>>() { new KeyValuePair<string, object>("ApiKey", "EE9C8401-1436-4957-A464-2212718B6FBF") },
                new RequestBuilderCommandFactory(new TraditionServiceDefaultVerbPrefixes(), new TraditionalStyleNamingStrategy(), new JsonRequestSerializer()))
        {
        }
    }

    [TestFixture]
    public class TraditionalStyleApiTests
    {
        [Test]
        public async void try_get_with_trad()
        {
            var res = await new PagesJaunesApiRequestGenerator()
                .InterfaceAndMethodToRequest<ICatalogController, ResponseBase<RestaurantResponse[]>>(x => x.GetRestaurants("75017", 1, null, null, null, null, null, null, 0, null, null,
                                    null, null, null, "center"), 150);

            Assert.That(res.Result.First().Address, Is.Not.Null);
        }
    }
    [TestFixture]
    public class RestStyleApiGetTests
    {
        [Test]
        public async void try_get_request_without_parameters()
        {
            // arrange
            const string baseUri = "http://jsonplaceholder.typicode.com/";

            // act
            var result = await new RequestGenerator(baseUri, new[] { new KeyValuePair<string, object>() }, new RequestBuilderCommandFactory(new DefaultRestVerbPrefixes(), new RestStyleNamingStrategy(), new JsonRequestSerializer()))
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
            var result = await new RequestGenerator(baseUri, new[] { new KeyValuePair<string, object>() }, new RequestBuilderCommandFactory(new DefaultRestVerbPrefixes(), new RestStyleNamingStrategy(), new JsonRequestSerializer()))
                                                        .InterfaceAndMethodToRequest<IPosts, Post>(x => x.Get(1));

            // assert
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.UserId, Is.EqualTo(1));
            Assert.That(result.Title, Is.EqualTo("sunt aut facere repellat provident occaecati excepturi optio reprehenderit"));
        }
    }

    [TestFixture]
    public class GroodStyleApiTests
    {
        public class GroodFlattened
        {
            public int GroodGuid { get; set; }
            public DateTime DateCreated { get; set; }
            public bool WhenAsap { get; set; }
            public DateTime DeliveryDate { get; set; }
            public int Status { get; set; }

            public Guid RestaurantGuid { get; set; }
            public string RestaurantName { get; set; }
            public string RestaurantImage { get; set; }

            public bool Private { get; set; }
            public string LeaderMessage { get; set; }

            public Guid DeliveryAddressGuid { get; set; }
            public Guid DeliveryAddressPlaceId { get; set; } // todo add on create grood, speeds up look ups

            public float Lat { get; set; }
            // Unrolled access to lat, lng 
            public float Lng { get; set; }

            public int CountdownSeconds { get; set; }
            public int JoinCode { get; set; }

        }

        public interface IGroodFlattenedController
        {
            GroodFlattened Post(GroodFlattened groodFlattened);
            GroodFlattened Get(int id);
            GroodFlattened[] Get();
        }

        [Test]
        public async void get()
        {
            var result = await new RequestGenerator("http://pagesjaunesresto-grood-apibranch.azurewebsites.net/api/", new[] { new KeyValuePair<string, object>() }, new RequestBuilderCommandFactory(new DefaultRestVerbPrefixes(), new RestStyleNamingStrategy(), new JsonRequestSerializer()))
                                                          .InterfaceAndMethodToRequest<IGroodFlattenedController, GroodFlattened[]>(
                            x => x.Get());

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public async void create()
        {
            var result = await new RequestGenerator("http://pagesjaunesresto-grood-apibranch.azurewebsites.net/api/", new[] { new KeyValuePair<string, object>() }, new RequestBuilderCommandFactory(new DefaultRestVerbPrefixes(), new RestStyleNamingStrategy(), new JsonRequestSerializer()))
                                                 .InterfaceAndMethodToRequest<IGroodFlattenedController, GroodFlattened>(
                   x => x.Post(new GroodFlattened() { RestaurantName = "peter"}));

            Assert.That(result, Is.Not.Null);

        }
    }
}
