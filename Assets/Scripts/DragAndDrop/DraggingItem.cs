using UnityEngine;

namespace DragAndDrop
{
    public abstract class DraggingItem : MonoBehaviour { }
    
    public abstract class DraggingItem<TItem> : DraggingItem
    {
        [SerializeField] private TItem _value;

        public TItem Value => _value;
    }
}