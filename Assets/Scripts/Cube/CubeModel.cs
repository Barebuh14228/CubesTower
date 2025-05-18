using System;
using Settings;
using UnityEngine;

namespace Cube
{
    public class CubeModel : MonoBehaviour
    {
        public event Action OnDestroyCalledEvent;
        public event Action BlockDraggingEvent;
        public event Action UnblockDraggingEvent;
        
        [SerializeField] private RectTransform _rectTransform;
        
        public Sprite CubeSprite { get; private set; }
        public RectTransform RectTransform => _rectTransform;

        public void Setup(CubeSettings settings)
        {
            CubeSprite = settings.Sprite;
        }

        public void CallDestroy()
        {
            OnDestroyCalledEvent?.Invoke();
        }

        public override bool Equals(object other)
        {
            if (other is CubeModel cubeModel)
            {
                return gameObject.GetInstanceID() == cubeModel.gameObject.GetInstanceID();
            }

            return false;
        }
        
        //todo hash code

        public void BlockDragging()
        {
            BlockDraggingEvent?.Invoke();
        }

        public void UnblockDragging()
        {
            UnblockDraggingEvent?.Invoke();
        }
    }
}