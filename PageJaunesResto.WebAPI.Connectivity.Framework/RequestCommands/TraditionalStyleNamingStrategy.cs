namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public class TraditionalStyleNamingStrategy : IRequestBaseNameStrategy
    {
        public string GetBaseName(string className, string methodName)
        {
            return className + "/" + methodName + "/";
        }
    }
}