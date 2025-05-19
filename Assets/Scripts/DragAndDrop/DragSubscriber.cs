using UnityEngine;

namespace DragAndDrop
{
    public abstract class DragSubscriber : MonoBehaviour
    {
        public abstract void NotifyOnDrag(DraggingItem draggingItem);
    }

    public abstract class DragSubscriber<TDraggingItem> : DragSubscriber where TDraggingItem : DraggingItem
    {
        public sealed override void NotifyOnDrag(DraggingItem draggingItem) => NotifyOnDrag(draggingItem as TDraggingItem);
        protected abstract void NotifyOnDrag(TDraggingItem draggingItem);
    }
}