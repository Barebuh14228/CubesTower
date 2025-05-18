using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DragEventsUtils
{
    [RequireComponent(typeof(RectTransform))]
    public class CubeDragSubscriber : DragEventsSubscriber
    {
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private UnityEvent OnBeginDragEvent;
        [SerializeField] private UnityEvent OnEndDragEvent;
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            OnBeginDragEvent?.Invoke();
        }
        
        public override void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position += (Vector3) eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragEvent?.Invoke();
        }
    }
}