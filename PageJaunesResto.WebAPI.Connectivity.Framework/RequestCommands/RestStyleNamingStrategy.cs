namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public class RestStyleNamingStrategy : IRequestBaseNameStrategy
    {
        public string GetBaseName(string className, string methodName)
        {
            // throw away method name, only used for identitfication here

            return className;
        }
    }

    public class TraditionalStyleNamingStrategy : IRequestBaseNameStrategy
    {
        public string GetBaseName(string className, string methodName)
        {
            return className + "/" + methodName + "/";
        }
    }
}