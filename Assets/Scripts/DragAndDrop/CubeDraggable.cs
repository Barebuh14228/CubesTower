using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace DefaultNamespace
{
    public class CubeDraggable : CustomDraggable
    {
        private RectTransform _rectTransform;
        private Action _dragFinishAction;
        
        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        public void SetDragFinishAction(Action action)
        {
            _dragFinishAction = action;
        }
        
        public override void OnBeginDrag(PointerEventData eventData) { }
        
        public override void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position += (Vector3) eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            _dragFinishAction?.Invoke();
        }
    }
}