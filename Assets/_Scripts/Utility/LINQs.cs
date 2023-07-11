using System;
using System.Collections.Generic;

public static class LINQs
{
    public static List<T2> CastTo<T1, T2>(this List<T1> list) where T2 : class
    {
        var newList = new List<T2>();
        foreach (var value in list) newList.Add(value as T2);

        return newList;
    }

    public static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector)
    {
        return source.MinBy(selector, null);
    }

    private static TSource MinBy<TSource, TKey>(this IEnumerable<TSource> source, Func<TSource, TKey> selector, IComparer<TKey> comparer)
    {
        if (source == null) throw new ArgumentNullException("source");
        if (selector == null) throw new ArgumentNullException("selector");
        comparer ??= Comparer<TKey>.Default;

        using (var sourceIterator = source.GetEnumerator())
        {
            if (!sourceIterator.MoveNext()) throw new InvalidOperationException("Sequence contains no elements");

            var min = sourceIterator.Current;
            var minKey = selector(min);
            while (sourceIterator.MoveNext())
            {
                var candidate = sourceIterator.Current;
                var candidateProjected = selector(candidate);
                if (comparer.Compare(candidateProjected, minKey) < 0)
                {
                    min = candidate;
                    minKey = candidateProjected;
                }
            }

            return min;
        }
    }

    public static bool AreElementsIdentical(this List<int> list)
    {
        if (list.Count == 0) return true;
        var first = list[0];
        for (var i = 1; i < list.Count; i++)
            if (list[i] != first)
                return false;

        return true;
    }
}