using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.HttpRequestCommands
{
    public class PutHttpRequestBuilderCommand : IRequestBuilderCommand
    {
        private readonly string _methodName;

        public PutHttpRequestBuilderCommand(string methodName)
        {
            _methodName = methodName;
        }

        public async Task<TReturnType> BuildRequest<TReturnType>(string url, params KeyValuePair<string, string>[] parameters)
        {
            var request = new HttpClient();
            Uri uri = new Uri(url);

            var content = new FormUrlEncodedContent(parameters);

            var result = await request.PutAsync(uri, content);
            return JsonConvert.DeserializeObject<TReturnType>(await result.Content.ReadAsStringAsync());
        }

        public async Task BuildRequest(string url, params KeyValuePair<string, string>[] parameters)
        {
            var request = new HttpClient();
            Uri uri = new Uri(url);

            var content = new FormUrlEncodedContent(parameters);

            await request.PutAsync(uri, content);
        }
    }
}