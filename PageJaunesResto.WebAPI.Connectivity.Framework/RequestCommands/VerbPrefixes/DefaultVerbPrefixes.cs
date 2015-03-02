namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes
{
    public class DefaultVerbPrefixes : IVerbPrefixes
    {
        public string GetGetPrefix()
        {
            return "Get";
        }

        public string GetDeletePrefix()
        {
            return "Delete";
        }

        public string GetPostPrefix()
        {
            return "Set";
        }

        public string GetPutPrefix()
        {
            return "Update";
        }
    }
}