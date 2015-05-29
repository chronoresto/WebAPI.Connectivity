using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Linq;
using Newtonsoft.Json;
using PageJaunesResto.WebAPI.Connectivity.Framework.Helpers;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.RequestCommands.Http
{
    public class GetHttpRequestBuilderCommand : IRequestBuilderCommand
    {
        private readonly string _methodName;
        private readonly IRequestSerializer _requestSerializer;

        public GetHttpRequestBuilderCommand(string methodName, IRequestSerializer requestSerializer)
        {
            _methodName = methodName;
            _requestSerializer = requestSerializer;
        }

        public async Task<TReturnType> BuildRequest<TReturnType>(string url, params KeyValuePair<string, object>[] parameters)
        {
            var result = await DoGet(url, parameters);

            Debug.WriteLine(result);

            try
            {
                return _requestSerializer.DeserializeObject<TReturnType>(result);
            }
            catch (JsonSerializationException)
            {
                // retry as an array 
                return _requestSerializer.DeserializeObject<IEnumerable<TReturnType>>(result).First();
            }
        }

        public async Task BuildRequest(string url, params KeyValuePair<string, object>[] parameters)
        {
            await DoGet(url, parameters);
        }

        private async Task<string> DoGet(string url, KeyValuePair<string, object>[] parameters)
        {
            var request = new HttpClient();
            Uri uri = new Uri(url);

            uri = new Uri(uri + _methodName.ToLower());

            if (parameters.Any())
                uri = UriBuildingHelpers.AttachParameters(uri,
                    parameters.Select(
                        x => new KeyValuePair<string, string>(x.Key, UriBuildingHelpers.SimpleTypeToString(x)))
                        .ToArray());


            Debug.WriteLine(uri.ToString() + "\r\n " +
                            parameters.Aggregate(string.Empty, (x, y) => x + (y.Key + " " + y.Value + "\r\n")));
            var result = await request.GetStringAsync(uri);
            Debug.WriteLine(uri.ToString() + "SUCCESS \r\n " +
                            parameters.Aggregate(string.Empty, (x, y) => x + (y.Key + " " + y.Value + "\r\n")));

            return result;
        }

    }
}