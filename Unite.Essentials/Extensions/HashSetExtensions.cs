namespace Unite.Essentials.Extensions;

public static class HashSetExtensions
{
	/// <summary>
	/// Adds elements to the hash set.
	/// </summary>
	/// <typeparam name="T">Element type.</typeparam>
	/// <param name="hashSet">Hash set.</param>
	/// <param name="elements">Collection of elements.</param>
    public static void AddRange<T>(this HashSet<T> hashSet, IEnumerable<T> elements)
	{
		foreach (var value in elements)
		{
			hashSet.Add(value);
		}
	}
}
