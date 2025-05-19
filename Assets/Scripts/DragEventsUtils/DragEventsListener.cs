using UnityEngine;
using UnityEngine.EventSystems;

namespace DragEventsUtils
{
    public abstract class DragEventsListener : MonoBehaviour
    {
        public abstract void OnBeginDrag(PointerEventData eventData);
        public abstract void OnDrag(PointerEventData eventData);
        public abstract void OnEndDrag(PointerEventData eventData);
    }
}