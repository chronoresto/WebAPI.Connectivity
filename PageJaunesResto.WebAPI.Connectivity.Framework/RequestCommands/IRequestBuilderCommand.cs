using System.Collections.Generic;
using System.Threading.Tasks;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands
{
    public interface IRequestBuilderCommand
    {
        Task<TReturnType> BuildRequest<TReturnType>(string url, params KeyValuePair<string, string>[] parameters);
        Task BuildRequest(string url, params KeyValuePair<string, string>[] parameters);
    }
}