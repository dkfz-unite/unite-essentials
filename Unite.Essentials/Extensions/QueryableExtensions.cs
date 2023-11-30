using System.Linq.Expressions;

namespace Unite.Essentials.Extensions;

public static class QueryableExtensions
{
    /// <summary>
    /// Iterates entities with buckets of given size
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="query">Database query</param>
    /// <param name="bucketSize">Bucket size</param>
    /// <param name="handler">Handler</param>
    public static void Batch<T>(this IQueryable<T> query,
        int bucketSize,
        Action<IEnumerable<T>> handler)
        where T : class
    {
        var condition = CreateExpression<T, bool>(entity => true);
        var selector = CreateExpression<T, T>(entity => entity);

        query.Batch(condition, selector, bucketSize, handler);
    }

    /// <summary>
    /// Iterates entities with buckets of given size
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <param name="query">Database query</param>
    /// <param name="condition">Condition</param>
    /// <param name="bucketSize">Bucket size</param>
    /// <param name="handler">Handler</param>
    public static void Batch<T>(this IQueryable<T> query,
        Expression<Func<T, bool>> condition,
        int bucketSize,
        Action<IEnumerable<T>> handler)
        where T : class
    {
        var selector = CreateExpression<T, T>(source => source);

        query.Batch(condition, selector, bucketSize, handler);
    }

    /// <summary>
    /// Iterates entities with buckets of given size
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TResult">Selection type</typeparam>
    /// <param name="query">Database query</param>
    /// <param name="selector">Selector</param>
    /// <param name="bucketSize">Bucker size</param>
    /// <param name="handler">Handler</param>
    public static void Batch<T, TResult>(this IQueryable<T> query,
        Expression<Func<T, TResult>> selector,
        int bucketSize,
        Action<IEnumerable<TResult>> handler)
        where T : class
    {
        var condition = CreateExpression<T, bool>(source => true);

        query.Batch(condition, selector, bucketSize, handler);
    }

    /// <summary>
    /// Iterates entities with buckets of given size
    /// </summary>
    /// <typeparam name="T">Entity type</typeparam>
    /// <typeparam name="TResult">Selection type</typeparam>
    /// <param name="query">Database query</param>
    /// <param name="condition">Condition</param>
    /// <param name="selector">Selector</param>
    /// <param name="bucketSize">Bucket size</param>
    /// <param name="handler">Handler</param>
    public static void Batch<T, TResult>(this IQueryable<T> query,
        Expression<Func<T, bool>> condition,
        Expression<Func<T, TResult>> selector,
        int bucketSize,
        Action<IEnumerable<TResult>> handler)
        where T : class
    {
        var position = 0;

        var entities = Enumerable.Empty<TResult>();

        do
        {
            entities = query
                .Where(condition)
                .Skip(position)
                .Take(bucketSize)
                .Select(selector)
                .ToArray();

            handler.Invoke(entities);

            position += entities.Count();

        }
        while (entities.Count() == bucketSize);
    }


    private static Expression<Func<T, TResult>> CreateExpression<T, TResult>(Expression<Func<T, TResult>> source)
        where T : class
    {
        return source;
    }
}
