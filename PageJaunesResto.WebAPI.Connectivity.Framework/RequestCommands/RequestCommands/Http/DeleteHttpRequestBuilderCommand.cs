using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PageJaunesResto.WebAPI.Connectivity.Framework.Helpers;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.RequestCommands.Http
{
    public class DeleteHttpRequestBuilderCommand : IRequestBuilderCommand
    {
        private readonly string _methodName;

        public DeleteHttpRequestBuilderCommand(string methodName)
        {
            _methodName = methodName;
        }

        public async Task<TReturnType> BuildRequest<TReturnType>(string url, params KeyValuePair<string, object>[] parameters)
        {
            var request = new HttpClient();
            Uri uri = new Uri(url);

            if (parameters.Any())
                uri = UriBuildingHelpers.AttachParameters(uri, parameters.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString())).ToArray());

            var result = await request.DeleteAsync(uri);

            return JsonConvert.DeserializeObject<TReturnType>(await result.Content.ReadAsStringAsync());
        }

        public async Task BuildRequest(string url, params KeyValuePair<string, object>[] parameters)
        {
            var request = new HttpClient();
            Uri uri = new Uri(url);

            if (parameters.Any())
                uri = UriBuildingHelpers.AttachParameters(uri, parameters.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString())).ToArray());

            await request.DeleteAsync(uri);
        }
    }
}