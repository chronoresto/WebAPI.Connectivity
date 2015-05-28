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
        private readonly IRequestSerializer _requestSerializer;

        public DeleteHttpRequestBuilderCommand(string methodName, IRequestSerializer requestSerializer)
        {
            _methodName = methodName;
            _requestSerializer = requestSerializer;
        }

        public async Task<TReturnType> BuildRequest<TReturnType>(string url, int timeoutSeconds, params KeyValuePair<string, object>[] parameters)
        {
            var request = new HttpClient { Timeout = new TimeSpan(0, 0, timeoutSeconds) };
            Uri uri = new Uri(url);

            uri = new Uri(uri + _methodName.ToLower());

            if (parameters.Any())
                uri = UriBuildingHelpers.AttachParameters(uri, parameters.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString())).ToArray());

            var result = await request.DeleteAsync(uri);

            return _requestSerializer.DeserializeObject<TReturnType>(await result.Content.ReadAsStringAsync());
        }

        public async Task BuildRequest(string url, int timeoutSeconds, params KeyValuePair<string, object>[] parameters)
        {
            var request = new HttpClient {Timeout = new TimeSpan(0, 0, timeoutSeconds)};

            Uri uri = new Uri(url);

            uri = new Uri(uri + _methodName.ToLower());

            if (parameters.Any())
                uri = UriBuildingHelpers.AttachParameters(uri, parameters.Select(x => new KeyValuePair<string, string>(x.Key, x.Value.ToString())).ToArray());

            await request.DeleteAsync(uri);
        }
    }
}