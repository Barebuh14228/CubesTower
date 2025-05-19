using UnityEngine;

namespace DragAndDrop
{
    public abstract class DropZone : MonoBehaviour
    {
        public abstract bool CanDrop(DraggingItem item);
        public abstract void Drop(DraggingItem item);
    }
    
    public abstract class DropZone<TDraggingItem> : DropZone where TDraggingItem : DraggingItem
    {
        public sealed override bool CanDrop(DraggingItem item) => CanDrop(item as TDraggingItem);
        public sealed override void Drop(DraggingItem item) => Drop(item as TDraggingItem);

        protected abstract bool CanDrop(TDraggingItem item);
        protected abstract void Drop(TDraggingItem item);
    }
}