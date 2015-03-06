namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.NamingStrategies
{
    public interface IRequestBaseNameStrategy
    {
        string GetBaseName(string className, string methodName);
    }
}