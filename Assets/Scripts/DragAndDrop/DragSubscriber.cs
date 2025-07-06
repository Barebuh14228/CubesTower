using UnityEngine;
using UnityEngine.Events;

namespace DragAndDrop
{
    public abstract class DragSubscriber : MonoBehaviour
    {
        public abstract void NotifyOnDrag(DraggingItem item);
    }

    public abstract class DragSubscriber<TDraggingItem> : DragSubscriber where TDraggingItem : DraggingItem
    {
        public sealed override void NotifyOnDrag(DraggingItem item)
        {
            if (item is TDraggingItem draggingItem)
            {
                NotifyOnDrag(draggingItem);
            }
        }
        protected abstract void NotifyOnDrag(TDraggingItem draggingItem);
    }

    public abstract class MonoDragSubscriber<TDraggingItem> : DragSubscriber<TDraggingItem> where TDraggingItem : DraggingItem
    {
        [SerializeField] private UnityEvent<TDraggingItem> _onDrag;
        
        protected override void NotifyOnDrag(TDraggingItem draggingItem)
        {
            _onDrag?.Invoke(draggingItem);
        }
    }
}