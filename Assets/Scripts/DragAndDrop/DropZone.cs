using System;
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
        public sealed override bool CanDrop(DraggingItem item)
        {
            return item is TDraggingItem draggingItem && CanDrop(draggingItem);
        }

        public sealed override void Drop(DraggingItem item)
        {
            if (item is TDraggingItem draggingItem)
            {
                Drop(draggingItem);
            }
            else
            {
                throw new Exception("Dragging item type is not valid!");
            }
        }

        protected abstract bool CanDrop(TDraggingItem item);
        protected abstract void Drop(TDraggingItem item);
    }
}