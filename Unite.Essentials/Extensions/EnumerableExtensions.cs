namespace Unite.Essentials.Extensions;

public static class EnumerableExtensions
{
    /// <summary>
    /// Performs given action on each element of collection.
    /// </summary>
    /// <typeparam name="T">Collection element type.</typeparam>
    /// <param name="elements">Source collection of elements.</param>
    /// <param name="action">Action to perform.</param> 
    public static void ForEach<T>(this IEnumerable<T> elements, Action<T> action)
    {
        foreach (var element in elements)
        {
            action(element);
        }
    }

    /// <summary>
    /// Checks if collection null or empty.
    /// </summary>
    /// <typeparam name="T">Collection element type.</typeparam>
    /// <param name="elements">Source collection of elements.</param>
    /// <returns>'true' if collection is either null or empty, 'false' otherwise.</returns>
    public static bool IsEmpty<T>(this IEnumerable<T> elements)
    {
        return elements == null || !elements.Any();
    }

    /// <summary>
    /// Checks if collection is not null and have elements.
    /// </summary>
    /// <typeparam name="T">Collection element type.</typeparam>
    /// <param name="elements">Source collection of epements.</param>
    /// <returns>'true' if colletion is not null and has elements, 'false' otherwise.</returns>
    public static bool IsNotEmpty<T>(this IEnumerable<T> elements)
    {
        return elements != null && elements.Any();
    }

    /// <summary>
    /// Iterates collection with given bucket size.
    /// </summary>
    /// <typeparam name="T">Collection element type.</typeparam>
    /// <param name="elements">Source collection of elements.</param>
    /// <param name="buketSize">Bucket size.</param>
    /// <param name="action">Bucket processing action.</param>
    public static void Iterate<T>(this IEnumerable<T> elements, int buketSize, Action<T[]> action)
    {
        var queue = new Queue<T>(elements);

        while (queue.Count != 0)
        {
            var chunk = queue.Dequeue(buketSize).ToArray();

            action(chunk);
        }
    }
}
