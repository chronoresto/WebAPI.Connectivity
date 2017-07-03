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

        public async Task<TReturnType> BuildRequest<TReturnType>(string url, int timeoutSeconds, params KeyValuePair<string, object>[] parameters)
        {
            var result = await DoGet(url, timeoutSeconds, parameters);

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

        public async Task BuildRequest(string url, int timeoutSeconds, params KeyValuePair<string, object>[] parameters)
        {
            await DoGet(url, timeoutSeconds, parameters);
        }

        private async Task<string> DoGet(string url, int timeoutSeconds, KeyValuePair<string, object>[] parameters)
        {
            var request = new HttpClient { Timeout = new TimeSpan(0, 0, timeoutSeconds) };          
            Uri uri = new Uri(url);

            uri = new Uri(uri + _methodName.ToLower());

            if (parameters.Any())
            {
                // Get all simple Type Parameters 
                var simpleTypeParameters = parameters
                    .Where(x => x.Key != null && x.Value != null && UriBuildingHelpers.IsSimpleType(x))
                    .Select(x => new KeyValuePair<string, string>(x.Key, UriBuildingHelpers.SimpleTypeToString(x)))
                    .ToArray();
                // Add simple type parameters to Uri
                uri = UriBuildingHelpers.AttachParameters(uri, simpleTypeParameters);

                // Get all non-simple type parameters
                var enumerableParameters =
                    parameters.Where(x => x.Key != null && x.Value != null && !UriBuildingHelpers.IsSimpleType(x))
                        .Select(x => new KeyValuePair<string, string>(x.Key, UriBuildingHelpers.EnumerableTypeToString(x)))
                        .ToArray();

                // Add all non-simple paramters to uri
                uri = UriBuildingHelpers.AttachParameters(uri, enumerableParameters);
            }
            Debug.WriteLine(uri.ToString() + "\r\n " +
                            parameters
                        .Where(x => x.Key != null && x.Value != null).Aggregate(string.Empty, (x, y) => x + (y.Key + " " + y.Value + "\r\n")));
            var result = await request.GetStringAsync(uri);            
            Debug.WriteLine(uri.ToString() + "SUCCESS \r\n " +
                            parameters
                        .Where(x => x.Key != null && x.Value != null).Aggregate(string.Empty, (x, y) => x + (y.Key + " " + y.Value + "\r\n")));

            return result;
        }

    }
}