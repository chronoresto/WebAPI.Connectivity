using System;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public class CommandNotFoundException : Exception
    {
        public CommandNotFoundException(string methodName) :
            base(string.Format("{0} method name command not found.", methodName))
        {
        }
    }
}