using System.Collections.Generic;
using UnityEngine;

public static class Randoms
{
    /// <summary>
    ///     Shuffle the list in-place.
    /// </summary>
    public static void Shuffle<T>(this IList<T> list)
    {
        var n = list.Count;
        while (n > 1)
        {
            n--;
            var k = Random.Range(0, n + 1);
            (list[k], list[n]) = (list[n], list[k]);
        }
    }

    public static Vector3 RandomVector3((float, float) xBounds, (float, float) yBounds, (float, float) z)
    {
        return new Vector3(Random.Range(xBounds.Item1, xBounds.Item2),
            Random.Range(yBounds.Item1, yBounds.Item2),
            Random.Range(z.Item1, z.Item2));
    }

    /// <summary>
    ///     Generate a Vector3 with randomly generated components in ranges (inclusive.
    /// </summary>
    /// <returns></returns>
    public static Vector3 RandomVector3((float, float) xBounds, (float, float) yBounds)
    {
        return new Vector3(Random.Range(xBounds.Item1, xBounds.Item2), Random.Range(yBounds.Item1, yBounds.Item2), 0);
    }

    /// <summary>
    ///     Generate a Vector3Int with randomly generated components in ranges.
    /// </summary>
    /// <param name="x">Start of xBounds (inclusive) and end of xBounds (exclusive).</param>
    /// <param name="y">Start of yBounds (inclusive) and end of yBounds (exclusive).</param>
    /// <returns></returns>
    public static Vector3 RandomVector3Int((int, int) x, (int, int) y)
    {
        return new Vector3(Random.Range(x.Item1, x.Item2), Random.Range(y.Item1, y.Item2), 0);
    }

    /// <summary>
    ///     Get a random point that is inside or on the surface of the collider.
    /// </summary>
    public static Vector3 GetRandomPointInsideCollider(this BoxCollider boxCollider)
    {
        var extents = boxCollider.size / 2f;
        var point = new Vector3(Random.Range(-extents.x, extents.x),
            Random.Range(-extents.y, extents.y),
            Random.Range(-extents.z, extents.z));

        return boxCollider.transform.TransformPoint(point);
    }

    public static Vector2 GetRandomPointInsideCollider(this BoxCollider2D boxCollider)
    {
        var extents = boxCollider.size / 2f;
        var point = new Vector2(Random.Range(-extents.x, extents.x), Random.Range(-extents.y, extents.y));

        return boxCollider.transform.TransformPoint(point);
    }
}