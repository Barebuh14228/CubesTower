using System.Linq;
using UnityEngine;

public static class RectTransformExtensions
{
    public static bool ContainRect(this RectTransform target, RectTransform transformToCheck)
    {
        return transformToCheck.GetWorldCornersArray().All(p => RectTransformUtility.RectangleContainsScreenPoint(target, p));
    }

    public static bool ContainsScreenPoints(this RectTransform target, Vector2[] points)
    {
        return points.All(p => RectTransformUtility.RectangleContainsScreenPoint(target, p));
    }
        
    public static Vector2[] GetWorldCornersArray(this RectTransform rectTransform)
    {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        return corners.Select(c => (Vector2) c).ToArray();
    }

    public static Rect GetWorldRect(this RectTransform rectTransform)
    {
        var corners = rectTransform.GetWorldCornersArray();
        return new Rect(corners[0], corners[2] - corners[0]);
    }

    public static Vector2[] GetCorners(this Rect rect)
    {
        return new[]
        {
            rect.min,
            new Vector2(rect.xMin, rect.yMax),
            new Vector2(rect.xMax, rect.yMax),
            rect.max
        };
    }
}