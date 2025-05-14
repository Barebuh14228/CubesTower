using UnityEngine;
using UnityEngine.EventSystems;

namespace DefaultNamespace
{
    public abstract class CustomDraggable : MonoBehaviour
    {
        public abstract void OnBeginDrag(PointerEventData eventData);
        public abstract void OnDrag(PointerEventData eventData);
        public abstract void OnEndDrag(PointerEventData eventData);
    }
}