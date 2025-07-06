using System;
using DragEventsUtils;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Serialization;
using Zenject;

namespace DragAndDrop
{
    public class CircleDragListener : DragEventsListener
    {
        [SerializeField] private DraggingCircle _draggingCircle;
        [SerializeField] private DragEventsRouter _router;

        [Inject] private DraggingController _draggingController;

        private void Start()
        {
            _router.SetTarget(this);
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            _draggingController.DragItem(_draggingCircle);
        }

        public override void OnDrag(PointerEventData eventData)
        {
            transform.position += (Vector3) eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            _draggingController.DropItem(_draggingCircle);
        }
    }
}