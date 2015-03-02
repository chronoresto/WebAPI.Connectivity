namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.NamingStrategies
{
    public class TraditionalStyleNamingStrategy : IRequestBaseNameStrategy
    {
        public string GetBaseName(string className, string methodName)
        {
            return className + "/" + methodName + "/";
        }
    }
}