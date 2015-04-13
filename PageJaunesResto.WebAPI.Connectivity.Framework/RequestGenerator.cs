using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Newtonsoft.Json;
using PageJaunesResto.WebAPI.Connectivity.Framework.Helpers;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.NamingStrategies;
using PageJaunesResto.WebAPI.Connectivity.Framework.RequestCommands.VerbPrefixes;

namespace PageJaunesResto.WebAPI.Connectivity.Framework
{
    public class RequestGenerator : IRequestGenerator
    {
        private readonly string _baseUrl;
        private readonly IEnumerable<KeyValuePair<string, object>> _defaultParams;
        private readonly IRequestBuilderCommandFactory _requestBuilderCommandFactory;

        public RequestGenerator(string baseUrl)
            : this(baseUrl, new List<KeyValuePair<string, object>>(), new RequestBuilderCommandFactory(new DefaultRestVerbPrefixes(), new RestStyleNamingStrategy(), new JsonRequestSerializer()))
        {
        }

        public RequestGenerator(string baseUrl, IEnumerable<KeyValuePair<string, object>> defaultParams, IRequestBuilderCommandFactory requestBuilderCommandFactory)
        {
            _baseUrl = baseUrl;
            _defaultParams = defaultParams;
            _requestBuilderCommandFactory = requestBuilderCommandFactory;
        }

        public async Task<TReturnType> InterfaceAndMethodToRequest<T, TReturnType>(Expression<Func<T, TReturnType>> action)
        {
            var methodBody = ((MethodCallExpression)action.Body);

            var methodName = methodBody.GetMethodName();
            var typeOfT = typeof(T);
            var className = typeOfT.GetTypeInfo().IsInterface ? typeOfT.Name.Remove(0, 1) : typeOfT.Name;

            var paramsToPass = methodBody.GetKeyValuePairsFromParametersInMethodCallExpression();

            var requestBuilder = _requestBuilderCommandFactory.GetRequestBuilderCommand(className, methodName);

            var paramsToGo = _defaultParams.ToList();
            paramsToGo.AddRange(paramsToPass);

            return await requestBuilder.BuildRequest<TReturnType>(_baseUrl, paramsToGo.ToArray());
        }

        public async Task InterfaceAndMethodToRequest<T>(Expression<Action<T>> action)
        {
            var methodBody = ((MethodCallExpression)action.Body);

            var methodName = methodBody.GetMethodName();
            var typeOfT = typeof(T);
            var className = typeOfT.GetTypeInfo().IsInterface ? typeOfT.Name.Remove(0, 1) : typeOfT.Name;

            var paramsToPass = methodBody.GetKeyValuePairsFromParametersInMethodCallExpression();

            var requestBuilder = _requestBuilderCommandFactory.GetRequestBuilderCommand(className, methodName);

            var paramsToGo = _defaultParams.ToList();
            paramsToGo.AddRange(paramsToPass);

            await requestBuilder.BuildRequest(_baseUrl, paramsToGo.ToArray());
        }
    }

    public class JsonRequestSerializer : IRequestSerializer
    {
        public T DeserializeObject<T>(string data)
        {
            return JsonConvert.DeserializeObject<T>(data);
        }

        public string SerializeObject(object data)
        {
            return JsonConvert.SerializeObject(data);
        }
    }

    public class XmlRequestSerializer : IRequestSerializer
    {
        public T DeserializeObject<T>(string data)
        {
            var serializer = new XmlSerializer(typeof(T));
            using (var s = GenerateStreamFromString(data))
            {
                var reader = new StreamReader(s);

                return (T)serializer.Deserialize(reader);
            }
        }

        public string SerializeObject(object data)
        {
            return Serialize(data);
        }

        public Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        public string Serialize(object obj)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new XmlSerializer(obj.GetType());
                serializer.Serialize(stream, obj);
                var bytes = new byte[stream.Length];
                stream.Position = 0;
                stream.Read(bytes, 0, bytes.Length);

                return Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            }
        }
    }

    public interface IRequestSerializer
    {
        string SerializeObject(object data);
        T DeserializeObject<T>(string data);
    }
}
