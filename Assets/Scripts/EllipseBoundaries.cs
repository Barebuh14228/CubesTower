using System;
using System.Linq;
using DefaultNamespace;
using UnityEngine;

namespace DragAndDrop
{
    public class EllipseBoundaries : MonoBehaviour
    {
        [SerializeField] private float _ellipseScalingStep = 0.01f;
        
        private EllipseUtils.EllipseParams _boundingEllipse;
        
        public void RecalculateBoundaries(Vector3[] points)
        {
            _boundingEllipse = EllipseUtils.CalculateBoundingEllipse(points, _ellipseScalingStep);
        }
        
        public bool IsPointInBoundaries(Vector3 point)
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