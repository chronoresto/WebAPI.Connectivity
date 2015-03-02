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

    /// <summary>
    /// If your own service has a crazy number like ours had,
    /// I suggest inheriting from this, and addranging this guy at the 
    /// End of your own for reuse.
    /// </summary>
    public class TraditionServiceDefaultVerbPrefixes : IVerbPrefixes
    {
        // We are insane apparently.
        public IEnumerable<string> GetGetPrefixs()
        {
            return new[] { "Get", "Search", "Find", "Confirm", "Clone", "Recover", "Validate", "Fidelity", "Convert" };
        }

        public IEnumerable<string> GetDeletePrefixs()
        {
            return new[] { "Delete", "Remove" };
        }

        public IEnumerable<string> GetPostPrefixs()
        {
            return new[] { "Set", "Submit", "Create", "Update", "Control",  };
        }

        public IEnumerable<string> GetPutPrefixs()
        {
            return  new[] { "Add" };
        }
    }
}