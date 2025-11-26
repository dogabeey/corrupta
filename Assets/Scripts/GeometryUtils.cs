using System;
using System.Collections.Generic;
using UnityEngine;

public static class GeometryUtils
{
    public static (Vector2, Vector2) GetFarthestPoints(this List<Vector2> allPoints)
    {
        if (allPoints == null || allPoints.Count < 2)
            throw new ArgumentException("Need at least 2 points");

        // 1. Compute convex hull
        var hull = ConvexHull(allPoints);

        if (hull.Count == 2)
            return (hull[0], hull[1]);

        // 2. Rotating calipers to find farthest points on hull
        float maxDist = 0f;
        Vector2 bestA = hull[0], bestB = hull[1];

        int j = 1;
        int h = hull.Count;

        for (int i = 0; i < h; i++)
        {
            int nextI = (i + 1) % h;

            // Move j while area increases
            while (Cross(
                hull[nextI] - hull[i],
                hull[(j + 1) % h] - hull[i]
            ) > Cross(hull[nextI] - hull[i], hull[j] - hull[i]))
            {
                j = (j + 1) % h;
            }

            // Check distance for pair (i, j)
            float dist = (hull[i] - hull[j]).sqrMagnitude;
            if (dist > maxDist)
            {
                maxDist = dist;
                bestA = hull[i];
                bestB = hull[j];
            }
        }

        return (bestA, bestB);
    }

    // ----------------------------- //
    //  Convex Hull (Graham Scan)    //
    // ----------------------------- //

    private static List<Vector2> ConvexHull(List<Vector2> pts)
    {
        var points = new List<Vector2>(pts);
        points.Sort((a, b) =>
            a.x == b.x ? a.y.CompareTo(b.y) : a.x.CompareTo(b.x));

        List<Vector2> lower = new List<Vector2>();
        foreach (var p in points)
        {
            while (lower.Count >= 2 &&
                Cross(lower[lower.Count - 1] - lower[lower.Count - 2], p - lower[lower.Count - 1]) <= 0)
                lower.RemoveAt(lower.Count - 1);
            lower.Add(p);
        }

        List<Vector2> upper = new List<Vector2>();
        for (int i = points.Count - 1; i >= 0; i--)
        {
            var p = points[i];
            while (upper.Count >= 2 &&
                Cross(upper[upper.Count - 1] - upper[upper.Count - 2], p - upper[upper.Count - 1]) <= 0)
                upper.RemoveAt(upper.Count - 1);
            upper.Add(p);
        }

        // Remove duplicates of first/last
        upper.RemoveAt(upper.Count - 1);
        lower.RemoveAt(lower.Count - 1);

        lower.AddRange(upper);
        return lower;
    }

    private static float Cross(Vector2 a, Vector2 b)
        => a.x * b.y - a.y * b.x;
}

