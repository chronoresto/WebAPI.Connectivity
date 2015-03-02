using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading.Tasks;
using PageJaunesResto.WebAPI.Connectivity.Framework.Helpers;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.WebAPI.Connectivity.Framework
{
    public class RequestGenerator : IRequestGenerator
    {
        private readonly string _baseUrl;
        private readonly IRequestBuilderCommandFactory _requestBuilderCommandFactory;

        public RequestGenerator(string baseUrl)
            : this(baseUrl, new RequestBuilderCommandFactory(new DefaultVerbPrefixes(), new RestStyleNamingStrategy()))
        {
        }

        public RequestGenerator(string baseUrl, IRequestBuilderCommandFactory requestBuilderCommandFactory)
        {
            _baseUrl = baseUrl;
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

            return await requestBuilder.BuildRequest<TReturnType>(_baseUrl, paramsToPass.ToArray());
        }

        public async Task InterfaceAndMethodToRequest<T>(Expression<Action<T>> action)
        {
            var methodBody = ((MethodCallExpression)action.Body);

            var methodName = methodBody.GetMethodName();
            var typeOfT = typeof(T);
            var className = typeOfT.GetTypeInfo().IsInterface ? typeOfT.Name.Remove(0, 1) : typeOfT.Name;

            var paramsToPass = methodBody.GetKeyValuePairsFromParametersInMethodCallExpression();

            var requestBuilder = _requestBuilderCommandFactory.GetRequestBuilderCommand(className, methodName);

            await requestBuilder.BuildRequest(_baseUrl, paramsToPass.ToArray());
        }
    }

    public interface IRequestGenerator
    {
        Task<TReturnType> InterfaceAndMethodToRequest<T, TReturnType>(Expression<Func<T, TReturnType>> action);
        Task InterfaceAndMethodToRequest<T>(Expression<Action<T>> action);
    }
}
