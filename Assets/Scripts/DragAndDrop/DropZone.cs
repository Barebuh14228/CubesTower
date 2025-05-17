using UnityEngine;

namespace DragAndDrop
{
    public abstract class DropZone : MonoBehaviour
    {
        public abstract void Drop(DraggingItem draggingItem);

        public abstract bool CanDrop(DraggingItem draggingItem);
    }
    
    //todo
    
    public abstract class DropZone<TDraggingItem> : DropZone where TDraggingItem : DraggingItem
    {
        [SerializeField] private DropSubscriber<TDraggingItem> _subscriber;

        public sealed override void Drop(DraggingItem draggingItem)
        {
            DropItem(draggingItem as TDraggingItem); //todo мне не нравиться
        }

        public sealed override bool CanDrop(DraggingItem draggingItem)
        {
            return CanDropItem(draggingItem as TDraggingItem); //todo мне не нравиться
        }
        
        private void DropItem(TDraggingItem draggingItem)
        {
            _subscriber.NotifyOnDrop(draggingItem);
        }
        
        protected abstract bool CanDropItem(TDraggingItem draggingItem);
    }
}