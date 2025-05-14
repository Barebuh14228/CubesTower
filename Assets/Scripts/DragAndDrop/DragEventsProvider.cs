using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public class DragEventsProvider : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
    {
        private CustomDraggable _target;

        public void SetTarget(CustomDraggable draggable)
        {
            _target = draggable;
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