using UnityEngine;
using UnityEngine.EventSystems;

namespace DragEventsUtils
{
    public class DragEventsProvider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private DragEventsSubscriber _target;

        public void SetTarget(DragEventsSubscriber target)
        {
            _target = target;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            _target?.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _target?.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _target?.OnEndDrag(eventData);
        }
    }
}