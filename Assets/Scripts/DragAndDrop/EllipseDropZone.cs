using System;
using System.Linq;
using UnityEngine;

namespace DragAndDrop
{
    public class EllipseDropZone : DropZone
    {
        public event Action<DropItem> OnItemDropped;
        
        [SerializeField] private float _ellipseScalingStep = 0.01f;
        
        private EllipseUtils.EllipseParams _boundingEllipseParams;
        private bool _isParamsSet;
        
        protected override bool IsItemInside(DropItem dropItem)
        {
            if (!base.IsItemInside(dropItem))
                return false;

            if (!_isParamsSet)
                return true;
            
            var corners = dropItem.GetCorners();
            
            return corners.Any(IsPointInBoundaries);
        }

        protected override void Drop(DropItem item)
        {
            OnItemDropped?.Invoke(item);
        }

        public void RecalculateBoundaries(Vector3[] points)
        {
            _isParamsSet = true;
            _boundingEllipseParams = EllipseUtils.CalculateBoundingEllipse(points, _ellipseScalingStep);
        }

        private bool IsPointInBoundaries(Vector3 point)
        {
            return _boundingEllipseParams.ContainsPoint(point);
        }
        
        private void OnDrawGizmos()
        {
            var points = _boundingEllipseParams.GetBoundaryPoints();

            var span = new ReadOnlySpan<Vector3>(points);
            
            Gizmos.color = Color.blue;
            Gizmos.DrawLineStrip(span,true);
        }
    }
}