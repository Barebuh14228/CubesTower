using UnityEngine;
using UnityEngine.EventSystems;

namespace DragEventsUtils
{
    public class DragEventsProvider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private DragEventsSubscriber _target;

        private bool _ignoreEvents;
        
        // _dragBegin - это костыль на случай, когда мы зажали кнопку на объекте игнорирующем события.
        // Когда объект перестает игнорировать события, а кнопака мыши зажата на нем, он потенциально может начать
        // перемещение не вызывая OnBeginDrag, что в свою очередь приводит к ошибкам
        private bool _dragBegin;
        
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

            _dragBegin = true;
            _target?.OnBeginDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_ignoreEvents)
                return;
            
            if (!_dragBegin)
                return;
            
            _target?.OnDrag(eventData);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (_ignoreEvents)
                return;
            
            if (!_dragBegin)
                return;

            _dragBegin = false;
            _target?.OnEndDrag(eventData);
        }
    }
}