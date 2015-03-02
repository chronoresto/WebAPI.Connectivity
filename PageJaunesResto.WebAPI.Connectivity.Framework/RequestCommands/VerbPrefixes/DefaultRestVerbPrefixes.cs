using System.Collections.Generic;
using System.Linq;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes
{
    public class DefaultRestVerbPrefixes : IVerbPrefixes
    {
        public IEnumerable<string> GetGetPrefixs()
        {
            return new[] {"Get"};
        }

        public IEnumerable<string> GetDeletePrefixs()
        {
            return new[] { "Delete" };
        }

        public IEnumerable<string> GetPostPrefixs()
        {
            return new[] { "Set", "Post" };
        }

        public IEnumerable<string> GetPutPrefixs()
        {
            return new[] { "Update", "Put" };
        }
    }
}