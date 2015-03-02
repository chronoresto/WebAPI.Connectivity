using System;
using System.Collections.Generic;
using NUnit.Framework;
using PageJaunesResto.WebAPI.Connectivity.Framework.Helpers;

namespace PageJaunesResto.WebAPI.Connectivity.Frameworks.Tests
{
    [TestFixture]
    public class UrlHelperTests
    {
        [Test]
        public void given_some_parameters_correct_url_built()
        {
            // Arrange 
            var uri = new Uri("http://www.chronoresto.fr/");
            
            // Act
            var result = UriBuildingHelpers.AttachParameters(uri, new KeyValuePair<string, string>("food", "lovely"),
                new KeyValuePair<string, string>("code", "this"));

            // Assert
            Assert.That(result.ToString(), Is.EqualTo("http://www.chronoresto.fr/?food=lovely&code=this"));
        }
    }
}
