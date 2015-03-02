namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public interface IRequestBuilderCommandFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="methodName"></param>
        /// <exception cref="CommandNotFoundException">If no command found matching methodName, throw CommandNotFoundException</exception>
        /// <returns></returns>
        IRequestBuilderCommand GetRequestBuilderCommand(string className, string methodName);
    }
}