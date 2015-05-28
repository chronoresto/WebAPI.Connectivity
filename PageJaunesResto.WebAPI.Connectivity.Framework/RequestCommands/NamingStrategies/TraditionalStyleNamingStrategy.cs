namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.NamingStrategies
{
    public class TraditionalStyleNamingStrategy : IRequestBaseNameStrategy
    {
        public string GetBaseName(string className, string methodName)
        {
            var name = className.EndsWith("Controller") ? className.Replace("Controller", "") : className;
            return name + "/" + methodName + "/";
        }
    }

    public class TraditionalStyleNamingNoFinalSlashStrategy : IRequestBaseNameStrategy
    {
        public string GetBaseName(string className, string methodName)
        {
            var name = className.EndsWith("Controller") ? className.Replace("Controller", "") : className;
            return name + "/" + methodName;
        }
    }

    public class AspxStyleNamingStrategy : IRequestBaseNameStrategy
    {
        public string GetBaseName(string className, string methodName)
        {
            var name = className.EndsWith("Controller") ? className.Replace("Controller", "") : className;
            return name + "/" + methodName + ".aspx";
        }
    }
}