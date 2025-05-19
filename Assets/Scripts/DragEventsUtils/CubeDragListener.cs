using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DragEventsUtils
{
    [RequireComponent(typeof(RectTransform))]
    public class CubeDragListener : DragEventsListener
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private UnityEvent OnBeginDragEvent;
        [SerializeField] private UnityEvent OnEndDragEvent;
        
        public bool IsDragging { get; private set; }
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            IsDragging = true;
            OnBeginDragEvent?.Invoke();
        }
        
        public override void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position += (Vector3) eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            IsDragging = false;
            OnEndDragEvent?.Invoke();
        }
    }
}