using UnityEngine;
using UnityEngine.EventSystems;

namespace DragEventsUtils
{
    public class DragEventsProvider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private DragEventsSubscriber _target;

        private bool _ignoreEvents;
        
        public void SetTarget(DragEventsSubscriber target)
        {
            _target = target;
        }

        public void IgnoreEvents()
        {
            _ignoreEvents = true;
        }

        public void ListenEvents()
        {
            _ignoreEvents = false;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (_ignoreEvents)
                return;
            
            _target?.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_ignoreEvents)
                return;
            
            _target?.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_ignoreEvents)
                return;
            
            _target?.OnEndDrag(eventData);
        }
    }
}