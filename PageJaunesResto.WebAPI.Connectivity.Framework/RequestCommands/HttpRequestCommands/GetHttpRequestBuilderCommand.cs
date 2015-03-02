using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using PageJaunesResto.WebAPI.Connectivity.Framework.Helpers;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.HttpRequestCommands
{
    public class GetHttpRequestBuilderCommand : IRequestBuilderCommand
    {
        private readonly string _methodName;

        public GetHttpRequestBuilderCommand(string methodName)
        {
            _methodName = methodName;
        }

        public async Task<TReturnType> BuildRequest<TReturnType>(string url, params KeyValuePair<string, string>[] parameters)
        {
            var request = new HttpClient();
            Uri uri = new Uri(url);

            // TODO : Add method name

            if (parameters.Any())
                uri = UriBuildingHelpers.AttachParameters(uri, parameters);

            var result = await request.GetStringAsync(uri);
            return JsonConvert.DeserializeObject<TReturnType>(result);
        }

        public async Task BuildRequest(string url, params KeyValuePair<string, string>[] parameters)
        {
            var request = new HttpClient();
            Uri uri = new Uri(url);

            // TODO : Add method name

            if (parameters.Any())
                uri = UriBuildingHelpers.AttachParameters(uri, parameters);

            await request.GetStringAsync(uri);
        }

    }
}