using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using NUnit.Framework;
using PageJaunesResto.WebAPI.Connectivity.Framework;
using PageJaunesResto.WebAPI.Connectivity.Framework.Helpers;
using PageJaunesResto.WebAPI.Connectivity.Frameworks.Tests.TestClasses;

namespace PageJaunesResto.WebAPI.Connectivity.Frameworks.Tests
{
    [TestFixture]
    public class ExpressionHelpersTests
    {
        // implicit casting will do the job here
        private Expression<Func<IITestInterface, TestObjectShape>> GenerateExpression(Expression<Func<IITestInterface, TestObjectShape>> exp)
        {
            return exp;
        }

        [Test]
        public void given_some_method_with_params_is_parsed_properly()
        {
            // Arrange
            const int intParam = 1;
            const string stringParam = "Hello";

            var expression = GenerateExpression(x => x.GetItemsWithLoadsOfParams(stringParam, intParam));

            // Act
            var result = (expression.Body as MethodCallExpression).GetKeyValuePairsFromParametersInMethodCallExpression();
            var keyValuePairs = result as KeyValuePair<string, string>[] ?? result.ToArray();

            // Assert
            Assert.That(keyValuePairs.First().Key, Is.EqualTo("a"));
            Assert.That(keyValuePairs.First().Value, Is.EqualTo(stringParam));

            Assert.That(keyValuePairs.ElementAt(1).Key, Is.EqualTo("b"));
            Assert.That(keyValuePairs.ElementAt(1).Value, Is.EqualTo(intParam.ToString()));
        }

    }
}