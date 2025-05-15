using System;
using ModestTree.Util;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class CubeController : MonoBehaviour //todo naming
    {
        [SerializeField] private CubeView _cubeView;
        [SerializeField] private CubeDraggable _cubeDraggable;
        [SerializeField] private DragEventsProvider _dragEventsProvider;
        
        [Inject] private GameManager _gameManager;
        
        private void Start()
        {
            _cubeDraggable.SetDragFinishAction(DragFinishAction);
        }

        public void Setup(Sprite sprite)
        {
            _cubeView.Setup(sprite);
        }

        public void SetDraggableTarget(CustomDraggable draggableTarget)
        {
            _dragEventsProvider.SetTarget(draggableTarget);
        }
        
        public void SetCubeAsDraggableTarget()
        {
            SetDraggableTarget(_cubeDraggable);
        }

        private void DragFinishAction()
        {
            _gameManager.OnCubeDropped(this);
        }
    }
}