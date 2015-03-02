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

        public static IEnumerable<KeyValuePair<string, string>> GetKeyValuePairsFromParametersInMethodCallExpression(this MethodCallExpression expression)
        {
            var list = new List<KeyValuePair<string, string>>();
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

                var value = "";
                if (paramValue != null)
                    value = paramValue.ToString();
                    
                list.Add(new KeyValuePair<string, string>(key, value));
            }

            return list;
        }
    }
}