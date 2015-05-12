using System.Collections.Generic;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes
{
    /// <summary>
    /// One of our services is insane, this is an example of how you can bring 
    /// order from chaos using this API. Devs consuming your API just need to 
    /// look at the methods theyll be executing on the server without worrying about 
    /// what is happening on the web server tooooo much.
    /// </summary>
    public class TraditionServiceDefaultVerbPrefixes : IVerbPrefixes
    {
        // We are insane apparently.
        public IEnumerable<string> GetGetPrefixs()
        {
            return new[] { "Get", "Search", "Find", "Confirm", "Clone", "Recover", "Validate", "Fidelity", "Convert", "Json" };
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
            return  new[] { "Add", "Clear" };
        }
    }
}