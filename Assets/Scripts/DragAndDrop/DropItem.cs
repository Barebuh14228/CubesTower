using UnityEngine;

namespace DragAndDrop
{
    [RequireComponent(typeof(RectTransform))]
    public class DropItem : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        private RectTransform _draggingParent;
        
        public Vector3[] GetCorners()
        {
            var corners = new Vector3[4];
            _rectTransform.GetWorldCorners(corners);

            return corners;
        }

        public void SetDraggingParent(RectTransform draggingParent) //todo
        {
            _draggingParent = draggingParent;
        }
        
        public void MoveToDraggingParent()
        {
            transform.SetParent(_draggingParent, true);
        }
    }
}