using System;
using UnityEngine;

namespace DefaultNamespace
{
    public class UIController : MonoBehaviour //todo naming
    {
        [SerializeField] private Canvas _mainCanvas;
        [SerializeField] private ScrollDraggable _scrollDragable;
        [SerializeField] private CubesPalette _cubesPalette;
        [SerializeField] private Transform _draggingParent;
        [SerializeField] private RectTransform _holeParent;
        [SerializeField] private RectTransform _dropParent;
        
        public Canvas MainCanvas => _mainCanvas;
        public ScrollDraggable ScrollDraggable => _scrollDragable;
        public CubesPalette CubesPalette => _cubesPalette;
        public Transform DraggingParent => _draggingParent;
        public RectTransform HoleParent => _holeParent;
        public RectTransform DropParent => _dropParent;
    }
}