using System.Collections.Generic;
using System.Threading.Tasks;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.RequestCommands
{
    public interface IRequestBuilderCommand
    {
        Task<TReturnType> BuildRequest<TReturnType>(string url, params KeyValuePair<string, object>[] parameters);
        Task BuildRequest(string url, params KeyValuePair<string, object>[] parameters);
    }
}