using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DragEventsUtils
{
    [RequireComponent(typeof(ScrollRect))]
    public class ScrollDragListener : DragEventsListener
    {
        private ScrollRect _scrollRect;
        
        private void Awake()
        {
            _scrollRect = GetComponent<ScrollRect>();
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            _scrollRect?.OnBeginDrag(eventData);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            _scrollRect?.OnDrag(eventData);
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            _scrollRect?.OnEndDrag(eventData);
        }
    }
}