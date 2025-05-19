using UnityEngine;

namespace DragAndDrop
{
    public abstract class DropSubscriber : MonoBehaviour
    {
        public abstract void NotifyOnDrop(DraggingItem item);
    }
    
    public abstract class DropSubscriber<TDraggingItem> : DropSubscriber where TDraggingItem : DraggingItem
    {
        public sealed override void NotifyOnDrop(DraggingItem item) => NotifyOnDrop(item as TDraggingItem);

        protected abstract void NotifyOnDrop(TDraggingItem item);
    }
}