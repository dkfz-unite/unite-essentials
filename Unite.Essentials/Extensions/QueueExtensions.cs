namespace Unite.Essentials.Extensions;

public static class QueueExtensions
{
    /// <summary>
    /// Takes number of elements from the queue.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="queue">Queue.</param>
    /// <param name="number">Number of elements.</param>
    /// <returns>Collection with number of elements taken from the queue.</returns>
    public static IEnumerable<T> Dequeue<T>(this Queue<T> queue, int number)
    {
        for (var i = 0; i < number && queue.Count > 0; i++)
        {
            yield return queue.Dequeue();
        }
    }

    /// <summary>
    /// Adds elements to the queue.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="queue">Queue.</param>
    /// <param name="elements">Collection of elements.</param>
    public static void Enqueue<T>(this Queue<T> queue, IEnumerable<T> elements)
    {
        foreach (var element in elements)
        {
            queue.Enqueue(element);
        }
    }
}
