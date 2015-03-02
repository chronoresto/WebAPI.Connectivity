using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.HttpRequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public class RequestBuilderCommandFactory : IRequestBuilderCommandFactory
    {
        private readonly IVerbPrefixes _verbPrefixes;

        public RequestBuilderCommandFactory(IVerbPrefixes verbPrefixes)
        {
            _verbPrefixes = verbPrefixes;
        }

        public IRequestBuilderCommand GetRequestBuilderCommand(string methodName)
        {
            if (methodName.StartsWith(_verbPrefixes.GetGetPrefix()))
                return new GetHttpRequestBuilderCommand(methodName);

            if (methodName.StartsWith(_verbPrefixes.GetPostPrefix()))
                return new PostHttpRequestBuilderCommand(methodName);

            if (methodName.StartsWith(_verbPrefixes.GetPutPrefix()))
                return new PutHttpRequestBuilderCommand(methodName);

            if (methodName.StartsWith(_verbPrefixes.GetDeletePrefix()))
                return new DeleteHttpRequestBuilderCommand(methodName);

            throw new CommandNotFoundException(methodName);
        }
    }
}