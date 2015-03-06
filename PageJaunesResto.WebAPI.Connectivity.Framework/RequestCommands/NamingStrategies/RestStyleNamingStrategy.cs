namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.NamingStrategies
{
    public class RestStyleNamingStrategy : IRequestBaseNameStrategy
    {
        public string GetBaseName(string className, string methodName)
        {
            // throw away method name, only used for identitfication here

            return className;
        }
    }
}