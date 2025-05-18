using Cube;
using DG.Tweening;
using Tower;
using UnityEngine;
using UnityEngine.Events;

namespace DragAndDrop
{
    public class TowerDropSubscriber : DropSubscriber<DraggingCube>
    {
        [SerializeField] private TowerController _towerController;
        [SerializeField] private DropZone _dropZone;
        [SerializeField] private UnityEvent<CubeController> _onDrop;
        
        private Sequence _sequence;
        
        public override void NotifyOnDrop(DraggingCube item)
        {
            var cubeController = item.Value;
            
            if (_sequence != null && _sequence.IsActive())
            {
                cubeController.DestroyCube();
                return;
            }
            
            var cubeWorldRect = item.GetWorldRect();
            
            if (!_towerController.CanDropCube(item.Value, out var finalPositionRect))
            {
                cubeController.DestroyCube();
                return;
            }
            
            _sequence = DOTween.Sequence();
            
            var points = new Vector3[]
            {
                cubeWorldRect.center,
                finalPositionRect.center + Vector2.up * 70,
                finalPositionRect.center
            };
            
            _sequence.Append(cubeController.transform.DOPath(points, 0.5f, PathType.CatmullRom, PathMode.Sidescroller2D));
            _sequence.OnComplete(() => _onDrop?.Invoke(cubeController));
            _sequence.Play();
        }
    }
}