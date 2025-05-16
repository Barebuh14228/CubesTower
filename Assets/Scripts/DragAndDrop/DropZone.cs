using System.Linq;
using UnityEngine;

namespace DragAndDrop
{
    public abstract class DropZone : MonoBehaviour
    {
        [SerializeField] private RectTransform _rectTransform;
        
        public bool TryDropItem(DropItem dropItem)
        {
            if (IsItemInside(dropItem))
            {
                Drop(dropItem);
                return true;
            }

            return false;
        }

        protected virtual bool IsItemInside(DropItem dropItem)
        {
            return dropItem.GetCorners().All(p => RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, p));
        }

        protected abstract void Drop(DropItem item);

    }
}