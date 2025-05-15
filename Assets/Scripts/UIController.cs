using UnityEngine;

namespace DefaultNamespace
{
    public class UIController : MonoBehaviour //todo naming
    {
        [SerializeField] private ScrollDraggable _scrollDragable;
        [SerializeField] private CubesPalette _cubesPalette;
        
        public ScrollDraggable ScrollDraggable => _scrollDragable;
        public CubesPalette CubesPalette => _cubesPalette;
    }
}