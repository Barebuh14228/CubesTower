using System;
using UnityEngine;
using UnityEngine.EventSystems;
using Zenject;

namespace DefaultNamespace
{
    public class CubeDraggable : CustomDraggable
    {
        public event Action OnDragFinished; 
        
        private RectTransform _rectTransform;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }
        
        public override void OnBeginDrag(PointerEventData eventData) { }
        
        public override void OnDrag(PointerEventData eventData)
        {
            _rectTransform.position += (Vector3) eventData.delta;
        }

        public override void OnEndDrag(PointerEventData eventData)
        {
            OnDragFinished?.Invoke();
            //todo сменить parent
            //todo вызывать создание нового кубика, причем именно в своем контейнере
        }
    }
}