namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public interface IRequestBuilderCommandFactory
    {
        IRequestBuilderCommand GetRequestBuilderCommand(string methodName);
    }
}