using System.Linq;
using UnityEngine;

public static class RectTransformExtensions
{
    public static bool ContainRect(this RectTransform target, RectTransform rectTransform)
    {
        return rectTransform.GetWorldCornersArray().All(p => RectTransformUtility.RectangleContainsScreenPoint(target, p));
    }
    
    public static bool ContainRect(this RectTransform target, Rect worldRect)
    {
        return worldRect.GetCorners().All(p => RectTransformUtility.RectangleContainsScreenPoint(target, p));
    }
        
    public static Vector3[] GetWorldCornersArray(this RectTransform rectTransform)
    {
        var corners = new Vector3[4];
        rectTransform.GetWorldCorners(corners);
        return corners;
    }

    public static Rect GetWorldRect(this RectTransform rectTransform)
    {
        var corners = rectTransform.GetWorldCornersArray();
        return new Rect(corners[0], corners[2] - corners[0]);
    }

    public static Vector3[] GetCorners(this Rect rect)
    {
        return new[]
        {
            new Vector3(rect.xMin, rect.yMin, 0),
            new Vector3(rect.xMin, rect.yMax, 0),
            new Vector3(rect.xMax, rect.yMax, 0),
            new Vector3(rect.xMax, rect.yMin, 0)
        };
    }
}