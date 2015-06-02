namespace VendingMachine.UI
{
    using System;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class GetName
    {
        public static string Of<TSource>(TSource member,Expression<Func<TSource, object>> propertyLambda)
        {
            return Of<TSource>(propertyLambda);
        }

        public static string Of<TSource>(Expression<Func<TSource, object>> propertyLambda)
        {
            Type type = typeof(TSource);

            var member = propertyLambda.Body as MemberExpression;
            if (member == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a method, not a property.",
                    propertyLambda.ToString()));

            var propInfo = member.Member as PropertyInfo;
            if (propInfo == null)
                throw new ArgumentException(string.Format(
                    "Expression '{0}' refers to a field, not a property.",
                    propertyLambda.ToString()));

            if (type != propInfo.ReflectedType &&
                !type.IsSubclassOf(propInfo.ReflectedType))
                throw new ArgumentException(string.Format(
                    "Expresion '{0}' refers to a property that is not from type {1}.",
                    propertyLambda.ToString(),
                    type));

            return propInfo.Name;
        }
    }
}