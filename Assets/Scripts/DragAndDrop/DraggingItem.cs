using UnityEngine;

namespace DragAndDrop
{
    public abstract class DraggingItem : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;

        public RectTransform RectTransform => _rectTransform;
        
        public abstract void NotifyDropFailed();
    }
    
    public abstract class DraggingItem<TItem> : DraggingItem
    {
        [SerializeField] private TItem _value;

        public TItem Value => _value;
    }
}