using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace DragAndDrop
{
    [RequireComponent(typeof(RectTransform))]
    public class CubeDraggable : CustomDraggable
    {
        public event Action OnBeginDragEvent;
        public event Action OnEndDragEvent;
        
        [Inject] private UIController _uiController;
        
        private RectTransform _rectTransform;
        private RectTransform _draggingParent;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _draggingParent = _uiController.DraggingParent;
        }

        public override void OnBeginDrag(PointerEventData eventData)
        {
            transform.SetParent(_draggingParent,true);
            OnBeginDragEvent?.Invoke();
        }
        
        public override void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position += (Vector3) eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            OnEndDragEvent?.Invoke();
        }
    }
}