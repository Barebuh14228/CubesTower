using UnityEngine;

namespace DragAndDrop
{
    public abstract class DropSubscriber : MonoBehaviour
    {
        public abstract void NotifyOnDrop(DraggingItem draggingItem);
    }

    public abstract class DropSubscriber<TDraggingItem> : DropSubscriber where TDraggingItem : DraggingItem
    {
        public sealed override void NotifyOnDrop(DraggingItem draggingItem) => NotifyOnDrop(draggingItem as TDraggingItem);
        protected abstract void NotifyOnDrop(TDraggingItem draggingItem);
    }
}