namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes
{
    public interface IVerbPrefixes
    {
        string GetGetPrefix();
        string GetDeletePrefix();
        string GetPostPrefix();
        string GetPutPrefix();

    }
}