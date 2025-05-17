using UnityEngine;

namespace DragAndDrop
{
    public abstract class DropSubscriber<T> : MonoBehaviour where T : DraggingItem
    {
        public abstract void Drop(T item);
    }
}