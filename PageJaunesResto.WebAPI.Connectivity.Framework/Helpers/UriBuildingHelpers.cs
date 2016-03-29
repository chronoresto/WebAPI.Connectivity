using System;
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
            string str = "?";
            for (int index = 0; index < parameters.Count(); ++index)
            {
                if (!string.IsNullOrWhiteSpace(parameters.ElementAt(index).Value))
                {
					var key = parameters.ElementAt (index).Key;
					if (key.Contains ("__DOT__"))
						key = key.Replace ("__DOT__", ".");
					
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

        public static bool IsSimpleType(KeyValuePair<string, object> x)
        {
            if (x.Value == null || x.Key == null)
                return false;

            return x.Value is string || x.Value is Guid || x.Value is int || x.Value.GetType().GetTypeInfo().IsValueType;
        }
    }
}