using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using PageJaunesResto.WebAPI.Connectivity.Framework.Helpers;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.NamingStrategies;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.WebAPI.Connectivity.Framework
{
    public class RequestGenerator : IRequestGenerator
    {
        private readonly string _baseUrl;
        private readonly IEnumerable<KeyValuePair<string, object>> _defaultParams;
        private readonly IRequestBuilderCommandFactory _requestBuilderCommandFactory;

        public RequestGenerator(string baseUrl)
            : this(baseUrl, new List<KeyValuePair<string, object>>(), new RequestBuilderCommandFactory(new DefaultRestVerbPrefixes(), new RestStyleNamingStrategy()))
        {
        }

        public RequestGenerator(string baseUrl, IEnumerable<KeyValuePair<string, object>> defaultParams, IRequestBuilderCommandFactory requestBuilderCommandFactory)
        {
            _baseUrl = baseUrl;
            _defaultParams = defaultParams;
            _requestBuilderCommandFactory = requestBuilderCommandFactory;
        }

        public async Task<TReturnType> InterfaceAndMethodToRequest<T, TReturnType>(Expression<Func<T, TReturnType>> action)
        {
            var methodBody = ((MethodCallExpression)action.Body);

            var methodName = methodBody.GetMethodName();
            var typeOfT = typeof (T);
            var className = typeOfT.GetTypeInfo().IsInterface ? typeOfT.Name.Remove(0, 1) : typeOfT.Name;

            var paramsToPass = methodBody.GetKeyValuePairsFromParametersInMethodCallExpression();

            var requestBuilder = _requestBuilderCommandFactory.GetRequestBuilderCommand(className, methodName);

            var paramsToGo = _defaultParams.ToList();
            paramsToGo.AddRange(paramsToPass);

            return await requestBuilder.BuildRequest<TReturnType>(_baseUrl, paramsToGo.ToArray());
        }

        public async Task InterfaceAndMethodToRequest<T>(Expression<Action<T>> action)
        {
            var methodBody = ((MethodCallExpression)action.Body);

            var methodName = methodBody.GetMethodName();
            var typeOfT = typeof(T);
            var className = typeOfT.GetTypeInfo().IsInterface ? typeOfT.Name.Remove(0, 1) : typeOfT.Name;

            var paramsToPass = methodBody.GetKeyValuePairsFromParametersInMethodCallExpression();

            var requestBuilder = _requestBuilderCommandFactory.GetRequestBuilderCommand(className, methodName);

            var paramsToGo = _defaultParams.ToList();
            paramsToGo.AddRange(paramsToPass);

            await requestBuilder.BuildRequest(_baseUrl, paramsToGo.ToArray());
        }
    }
}
