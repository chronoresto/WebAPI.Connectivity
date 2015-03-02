using System.Collections.Generic;

namespace PageJaunesResto.Connectivity.Framework.Tests.Integration.RESTStyle.API
{
    public interface IPosts
    {
        IEnumerable<Post> Get();
        Post Get(int id);
        // todo Post Post(int id);
        string Delete();
    }
}