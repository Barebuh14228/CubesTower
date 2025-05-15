using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

namespace DragAndDrop
{
    public class CubeDraggable : CustomDraggable
    {
        [SerializeField] private UnityEvent _dragBeginEvent;
        [SerializeField] private UnityEvent _dragFinishEvent;
        
        private RectTransform _rectTransform;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            _dragBeginEvent?.Invoke();
        }
        
        public override void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position += (Vector3) eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            _dragFinishEvent?.Invoke();
        }
    }
}