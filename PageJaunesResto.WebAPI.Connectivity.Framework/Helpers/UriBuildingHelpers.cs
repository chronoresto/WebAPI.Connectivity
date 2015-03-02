using System;
using System.Collections.Generic;
using System.Linq;
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
                stringBuilder.Append(str + parameters.ElementAt(index).Key + "=" + parameters.ElementAt(index).Value);
                str = "&";
            }
            return new Uri(uri + stringBuilder.ToString());
        }
    }
}