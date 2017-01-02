using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.Helpers
{
    public static class UriBuildingHelpers
    {
        public static Uri AttachParameters(Uri uri, params KeyValuePair<string, string>[] parameters)
        {
            var stringBuilder = new StringBuilder();
            // Check if Query already contains elements - if yes add parameters with &, else ? to start the query string
            string str = string.IsNullOrWhiteSpace(uri.Query) ? "?" : "&";
            for (int index = 0; index < parameters.Count(); ++index)
            {
                if (!string.IsNullOrWhiteSpace(parameters.ElementAt(index).Value))
                {
                    var key = parameters.ElementAt(index).Key;
                    if (key.Contains("__DOT__"))
                        key = key.Replace("__DOT__", ".");

                    stringBuilder.Append(str + parameters.ElementAt(index).Key + "=" + parameters.ElementAt(index).Value);
                    str = "&";
                }
            }
            return new Uri(uri + stringBuilder.ToString());
        }


        public static string SimpleTypeToString(KeyValuePair<string, object> x)
        {
            if (x.Value == null)
                return string.Empty;

            if (x.Value is DateTime)
                return ((DateTime)x.Value).ToString(CultureInfo.InvariantCulture);

            if (x.Value is float)
                return ((float)x.Value).ToString(CultureInfo.InvariantCulture.NumberFormat);

            if (x.Value is double)
                return ((double)x.Value).ToString(CultureInfo.InvariantCulture.NumberFormat);

            if (x.Value is decimal)
                return ((decimal)x.Value).ToString(CultureInfo.InvariantCulture.NumberFormat);

            return x.Value.ToString();
        }

        public static string EnumerableTypeToString(KeyValuePair<string, object> x)
        {
            // Check that both Key and Value are not null
            if (x.Key == null || x.Value == null)
                return string.Empty;

            var result = string.Empty;

            // Check if Item is Enumerable
            if (IsEnumerable(x))
            {
                // Handle as Enumerable
                var enumerable = x.Value as IEnumerable;
                if (enumerable != null)
                {
                    // Convert to String List
                    List<string> listStr = new List<string>();
                    foreach (var item in enumerable)
                    {
                        listStr.Add(SimpleTypeToString(new KeyValuePair<string, object>(x.Key, item)));
                    }
                    // Join into query string
                    result = string.Join($"&{x.Key}=", listStr);
                }
            }

            return result;
        }

        public static bool IsSimpleType(KeyValuePair<string, object> x)
        {
            if (x.Value == null || x.Key == null)
                return false;

            return x.Value is string || x.Value is Guid || x.Value is int || x.Value.GetType().GetTypeInfo().IsValueType;
        }

        public static bool IsEnumerable(KeyValuePair<string, object> x)
        {
            if (x.Value == null || x.Key == null)
            {
                return false;
            }

            if (typeof(IEnumerable).GetTypeInfo().IsAssignableFrom(x.Value.GetType().GetTypeInfo()))
            {
                return true;
            }
            return false;
        }
    }
}