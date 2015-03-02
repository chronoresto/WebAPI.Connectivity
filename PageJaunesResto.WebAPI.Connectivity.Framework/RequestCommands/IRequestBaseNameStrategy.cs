namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public interface IRequestBaseNameStrategy
    {
        string GetBaseName(string className, string methodName);
    }
}