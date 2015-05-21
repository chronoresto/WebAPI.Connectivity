using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace PageJaunesResto.WebAPI.Connectivity.Framework
{
    public interface IRequestGenerator
    {
        Task<TReturnType> InterfaceAndMethodToRequest<T, TReturnType>(Expression<Func<T, TReturnType>> action, int timeoutSeconds = 15);
        Task InterfaceAndMethodToRequest<T>(Expression<Action<T>> action, int timeoutSeconds = 15);
        void SetBaseUrl(string baseUrl);
    }
}