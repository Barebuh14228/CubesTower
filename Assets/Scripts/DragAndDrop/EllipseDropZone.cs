using System;
using System.Linq;
using UnityEngine;

namespace DragAndDrop
{
    public class EllipseDropZone : DropZone
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private float _ellipseScalingStep = 0.01f;
        
        private EllipseUtils.EllipseParams _boundingEllipse;
        
        public override bool CanDrop(DraggingItem draggingItem)
        {
            var worldCornerPoints = draggingItem.RectTransform.GetWorldCornersArray();

            if (!_boundingEllipse.IsExist())
                return _rectTransform.ContainsScreenPoints(worldCornerPoints);
            
            return worldCornerPoints.Any(IsPointInBoundaries);
        }
        
        public void RecalculateBoundaries(Vector2[] points)
        {
            if (points.Length == 0)
            {
                _boundingEllipse = default;
                return;
            }
            
            _boundingEllipse = EllipseUtils.CalculateBoundingEllipse(points, _ellipseScalingStep);
        }
        
        private bool IsPointInBoundaries(Vector2 point)
        {
            return _boundingEllipse.ContainsPoint(point);
        }
        
        private void OnDrawGizmos()
        {
            var points = _boundingEllipse.GetBoundaryPoints();

            var span = new ReadOnlySpan<Vector3>(points);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLineStrip(span,true);
        }
    }
}