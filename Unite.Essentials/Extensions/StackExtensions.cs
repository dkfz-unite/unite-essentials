namespace Unite.Essentials.Extensions;

public static class StackExtensions
{
    /// <summary>
    /// Takes number of elements from the stack.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="stack">Stack.</param>
    /// <param name="number">Number of elements.</param>
    /// <returns>Collection with number of elements taken from the stack.</returns>
    public static IEnumerable<T> Pop<T>(this Stack<T> stack, int number)
    {
        for (var i = 0; i < number && stack.Count > 0; i++)
        {
            yield return stack.Pop();
        }
    }

    /// <summary>
    /// Adds elements to the stack.
    /// </summary>
    /// <typeparam name="T">Element type.</typeparam>
    /// <param name="stack">Stack.</param>
    /// <param name="elements">Collection of elements.</param>
    public static void Push<T>(this Stack<T> stack, IEnumerable<T> elements)
    {
        foreach (var element in elements)
        {
            stack.Push(element);
        }
    }
}
