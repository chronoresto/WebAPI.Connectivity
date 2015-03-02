using System.Net.Http.Headers;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.HttpRequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public class RequestBuilderCommandFactory : IRequestBuilderCommandFactory
    {
        private readonly IVerbPrefixes _verbPrefixes;
        private readonly IRequestBaseNameStrategy _requestBaseNameStrategy;
        private readonly bool _trimMethodName;

        public RequestBuilderCommandFactory(IVerbPrefixes verbPrefixes, IRequestBaseNameStrategy requestBaseNameStrategy)
        {
            _verbPrefixes = verbPrefixes;
            _requestBaseNameStrategy = requestBaseNameStrategy;
        }

        public IRequestBuilderCommand GetRequestBuilderCommand(string className, string methodName)
        {
            if (methodName.StartsWith(_verbPrefixes.GetGetPrefix()))
                return new GetHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName));

            if (methodName.StartsWith(_verbPrefixes.GetPostPrefix()))
                return new PostHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName));

            if (methodName.StartsWith(_verbPrefixes.GetPutPrefix()))
                return new PutHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName));

            if (methodName.StartsWith(_verbPrefixes.GetDeletePrefix()))
                return new DeleteHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName));

            throw new CommandNotFoundException(methodName);
        }
    }

    public interface IRequestBaseNameStrategy
    {
        string GetBaseName(string className, string methodName);
    }

    public class RestStyleNamingStrategy : IRequestBaseNameStrategy
    {
        public string GetBaseName(string className, string methodName)
        {
            // throw away method name, only used for identitfication here

            return className;
        }
    }
}