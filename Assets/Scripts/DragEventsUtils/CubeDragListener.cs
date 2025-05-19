using DragAndDrop;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using Zenject;

namespace DragEventsUtils
{
    [RequireComponent(typeof(RectTransform))]
    public class CubeDragListener : DragEventsListener
    {
        [SerializeField] private DraggingCube _draggingCube;

        [Inject] private DraggingController _draggingController;
        
        public bool IsDragging { get; private set; }
        
        public override void OnBeginDrag(PointerEventData eventData)
        {
            IsDragging = true;
            _draggingController.DragItem(_draggingCube);
        }
        
        public override void OnDrag(PointerEventData eventData)
        {
            transform.position += (Vector3) eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            IsDragging = false;
            _draggingController.DropItem(_draggingCube);
        }
    }
}