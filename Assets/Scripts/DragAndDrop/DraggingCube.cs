using Cube;
using UnityEngine;

namespace DragAndDrop
{
    public class DraggingCube : DraggingItem<CubeController>
    {
        [SerializeField] private RectTransform _rectTransform;
        
        public Rect GetWorldRect()
        {
            return _rectTransform.GetWorldRect();
        }
    }
}