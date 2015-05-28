using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PageJaunesResto.WebAPI.Connectivity.Framework.Helpers
{
    public static class ExpressionHelpers
    {
        public static string GetMethodName(this MethodCallExpression expression)
        {
            return expression.Method.Name;
        }

        public static IEnumerable<KeyValuePair<string, object>> GetKeyValuePairsFromParametersInMethodCallExpression(this MethodCallExpression expression)
        {
            var list = new List<KeyValuePair<string, object>>();
            var paramLength = expression.Method.GetParameters().Length;
            for (int index = 0; index < paramLength; index++)
            {
                var name = expression.Method.GetParameters()[index];
                object paramValue = null;
                if (index < expression.Arguments.Count())
                    paramValue = Expression.Lambda(expression.Arguments[index]).Compile().DynamicInvoke();

                var key = "";
                if (name != null)
                    key = name.Name;
                if (name != null && name.Name.Contains("_DOT_"))
                    key = name.Name.Replace("_DOT_", ".");

                object value = null;
                if (paramValue != null)
                    value = paramValue;
                    
                list.Add(new KeyValuePair<string, object>(key, value));
            }

            return list;
        }
    }
}