using System.Collections.Generic;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes
{
    public interface IVerbPrefixes
    {
        IEnumerable<string> GetGetPrefixs();
        IEnumerable<string> GetDeletePrefixs();
        IEnumerable<string> GetPostPrefixs();
        IEnumerable<string> GetPutPrefixs();
    }
}