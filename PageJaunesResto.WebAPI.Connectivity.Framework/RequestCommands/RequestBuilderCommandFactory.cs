using System.Linq;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.NamingStrategies;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.RequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.RequestCommands.Http;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public class RequestBuilderCommandFactory : IRequestBuilderCommandFactory
    {
        private readonly IVerbPrefixes _verbPrefixes;
        private readonly IRequestBaseNameStrategy _requestBaseNameStrategy;
        private readonly IRequestSerializer _requestSerializer;

        public RequestBuilderCommandFactory(IVerbPrefixes verbPrefixes, IRequestBaseNameStrategy requestBaseNameStrategy, IRequestSerializer requestSerializer)
        {
            _verbPrefixes = verbPrefixes;
            _requestBaseNameStrategy = requestBaseNameStrategy;
            _requestSerializer = requestSerializer;
        }

        public IRequestBuilderCommand GetRequestBuilderCommand(string className, string methodName)
        {
            if (_verbPrefixes.GetGetPrefixs().Any(methodName.StartsWith))
                return new GetHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName), _requestSerializer);

            if (_verbPrefixes.GetPostPrefixs().Any(methodName.StartsWith))
                return new PostHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName), _requestSerializer);

            if (_verbPrefixes.GetPutPrefixs().Any(methodName.StartsWith))
                return new PutHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName), _requestSerializer);

            if (_verbPrefixes.GetDeletePrefixs().Any(methodName.StartsWith))
                return new DeleteHttpRequestBuilderCommand(_requestBaseNameStrategy.GetBaseName(className, methodName), _requestSerializer);

            throw new CommandNotFoundException(methodName);
        }
    }
}