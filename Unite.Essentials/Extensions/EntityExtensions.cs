using System.Linq.Expressions;
using System.Reflection;

namespace Unite.Essentials.Extensions;

public static class EntityExtensions
{
    /// <summary>
    /// Assignes given value to given property of given entity.
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TValue">Property type</typeparam>
    /// <param name="target">Entity</param>
    /// <param name="property">Property</param>
    /// <param name="value">Value</param>
    /// <returns>Source entity.</returns>
    public static T With<T, TValue>(this T target, Expression<Func<T, TValue>> property, TValue value) where T : class
    {
        var memberExpression = property.Body as MemberExpression;
        if (memberExpression != null)
        {
            var propertyInfo = memberExpression.Member as PropertyInfo;
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(target, value, null);
            }
        }

        return target;
    }
}
