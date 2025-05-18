using UnityEngine;

namespace DragAndDrop
{
    public abstract class DropZone : MonoBehaviour
    {
        public abstract void Drop(DraggingItem draggingItem);

        public abstract bool CanDrop(DraggingItem draggingItem);
    }
    
    public abstract class DropZoneWithSubscriber<TDraggingItem> : DropZone where TDraggingItem : DraggingItem
    {
        [SerializeField] private DropSubscriber<TDraggingItem> _subscriber;

        public sealed override void Drop(DraggingItem draggingItem)
        {
            DropItem(draggingItem as TDraggingItem);
        }

        public sealed override bool CanDrop(DraggingItem draggingItem)
        {
            return CanDropItem(draggingItem as TDraggingItem);
        }
        
        private void DropItem(TDraggingItem draggingItem)
        {
            _subscriber.NotifyOnDrop(draggingItem);
        }
        
        protected abstract bool CanDropItem(TDraggingItem draggingItem);
    }
}