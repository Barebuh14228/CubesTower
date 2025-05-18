using UnityEngine;
using UnityEngine.EventSystems;

namespace DragEventsUtils
{
    public class DragEventsProvider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private DragEventsSubscriber _target;

        public bool BlockDragging { get; set; }

        public void SetTarget(DragEventsSubscriber target)
        {
            _target = target;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (BlockDragging)
                return;
            
            _target?.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (BlockDragging)
                return;
            
            _target?.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (BlockDragging)
                return;
            
            _target?.OnEndDrag(eventData);
        }
    }
}