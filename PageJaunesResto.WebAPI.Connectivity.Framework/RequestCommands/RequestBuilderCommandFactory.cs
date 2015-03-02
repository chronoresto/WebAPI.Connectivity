using System.Linq;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.HttpRequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public class RequestBuilderCommandFactory : IRequestBuilderCommandFactory
    {
        private readonly IVerbPrefixes _verbPrefixes;
        private readonly IRequestBaseNameStrategy _requestBaseNameStrategy;

        public RequestBuilderCommandFactory(IVerbPrefixes verbPrefixes, IRequestBaseNameStrategy requestBaseNameStrategy)
        {
            _verbPrefixes = verbPrefixes;
            _requestBaseNameStrategy = requestBaseNameStrategy;
        }

        public IRequestBuilderCommand GetRequestBuilderCommand(string className, string methodName)
        {
            if (_verbPrefixes.GetGetPrefixs().Any(methodName.StartsWith))
                return new GetHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName));

            if (_verbPrefixes.GetPostPrefixs().Any(methodName.StartsWith))
                return new PostHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName));

            if (_verbPrefixes.GetPutPrefixs().Any(methodName.StartsWith))
                return new PutHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName));

            if (_verbPrefixes.GetDeletePrefixs().Any(methodName.StartsWith))
                return new DeleteHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName));

            throw new CommandNotFoundException(methodName);
        }
    }
}