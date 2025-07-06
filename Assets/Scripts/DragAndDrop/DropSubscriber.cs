using UnityEngine;
using UnityEngine.Events;

namespace DragAndDrop
{
    public abstract class DropSubscriber : MonoBehaviour
    {
        public abstract void NotifyOnDrop(DraggingItem item);
    }

    public abstract class DropSubscriber<TDraggingItem> : DropSubscriber where TDraggingItem : DraggingItem
    {
        public sealed override void NotifyOnDrop(DraggingItem item)
        {
            if (item is TDraggingItem draggingItem)
            {
                NotifyOnDrop(draggingItem);
            }
        }
        protected abstract void NotifyOnDrop(TDraggingItem draggingItem);
    }

    public abstract class MonoDropSubscriber<TDraggingItem> : DropSubscriber<TDraggingItem> where TDraggingItem : DraggingItem
    {
        [SerializeField] private UnityEvent<TDraggingItem> _onDrop;
        
        protected override void NotifyOnDrop(TDraggingItem draggingItem)
        {
            _onDrop?.Invoke(draggingItem);
        }
    }
}