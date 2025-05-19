using System;
using System.Linq;
using Cube;
using DG.Tweening;
using DragAndDrop;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace Tower
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] private TowerModel _towerModel;
        [SerializeField] private EllipseDropZone _dropZone;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private UnityEvent _onCubeDropped;

        private Lazy<float> _bottomY;
        private Sequence _dropSequence;
        
        private void Start()
        {
            _bottomY = new (() => _rectTransform.GetWorldCornersArray().First().y);
        }

        public bool CanDropCube(CubeController cubeController, out Rect finalPositionRect)
        {
            finalPositionRect = default;

            if (_dropSequence != null && _dropSequence.IsActive())
                return false;
            
            var haveCubes = _towerModel.Cubes.Any();
            var cubeRect = cubeController.RectTransform.GetWorldRect();
            var cubeWidth = cubeRect.width;
            var offsetX = haveCubes ? Random.Range(0, cubeWidth) - cubeWidth / 2 : 0f;
            var dropPosition = GetDropPosition(cubeRect);
            
            finalPositionRect = cubeRect;
            finalPositionRect.position = dropPosition + Vector2.right * offsetX;
            
            return finalPositionRect.GetCorners()
                .Any(c => RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, c));
        }

        public void DropCube(CubeController cubeController)
        {
            var cubeRectTransform = cubeController.RectTransform;
            cubeRectTransform.SetParent(_rectTransform,true);
            _towerModel.AddCube(cubeController);
            RecalculateBoundaries();
            _onCubeDropped?.Invoke();
        }
        
        public void OnCubeDragged(CubeController cubeController)
        {
            if (!_towerModel.ContainCube(cubeController))
                return;
            
            var cubesToMoveDown = _towerModel.RemoveCube(cubeController);
            
            cubesToMoveDown.Reverse();
            cubesToMoveDown.ForEach(c => c.DragEventsProvider.IgnoreEvents());
            
            BlockTowerCubesDragging();
            
            var dropHeight = cubeController.RectTransform.GetWorldRect().height;
            
            _dropSequence = DOTween.Sequence();
            
            foreach (var cube in cubesToMoveDown)
            {
                var haveCubes = _towerModel.Cubes.Any();
                var topCubeRect = haveCubes
                    ? _towerModel.Cubes.First().RectTransform.GetWorldRect() 
                    : default;
                var cubeRect = cube.RectTransform.GetWorldRect();
                var dropPosition = cubeRect.center + dropHeight * Vector2.down;
                var centerOffset = haveCubes ? Mathf.Abs(topCubeRect.center.x - cubeRect.center.x) : 0f;
                
                var tween = cube.RectTransform.DOMove(dropPosition, 0.2f);

                if (centerOffset < cubeRect.width / 2)
                {
                    _towerModel.AddCube(cube);
                    tween.OnComplete(() => _onCubeDropped?.Invoke());
                }
                else
                {
                    dropHeight += cubeRect.height;
                    tween.OnComplete(() => cube.DestroyCube());
                }
                
                _dropSequence.Append(tween);
            }
            
            _dropSequence.OnComplete(() =>
            {
                UnblockTowerCubesDragging();
                RecalculateBoundaries();
            });
        }
        
        private void RecalculateBoundaries()
        {
            var points = _towerModel.Cubes
                .SelectMany(c => c.RectTransform.GetWorldCornersArray())
                .ToArray();
            
            _dropZone.RecalculateBoundaries(points);
        }

        private Vector2 GetDropPosition(Rect cubeRect)
        {
            var haveCubes = _towerModel.Cubes.Any();
            var topCubeRect = haveCubes 
                ? _towerModel.Cubes.First().RectTransform.GetWorldRect() 
                : default;
            
            var towerCenter = !haveCubes
                ? cubeRect.center.x
                : topCubeRect.center.x;
            
            var minY = !haveCubes
                ? _bottomY.Value
                : topCubeRect.max.y;

            var minX = towerCenter - cubeRect.width / 2;
            
            return new Vector2(minX, minY);
        }
        
        private void BlockTowerCubesDragging()
        {
            foreach (var cube in _towerModel.Cubes)
            {
                cube.DragEventsProvider.IgnoreEvents();
            }
        }
        
        private void UnblockTowerCubesDragging()
        {
            foreach (var cube in _towerModel.Cubes)
            {
                cube.DragEventsProvider.ListenEvents();
            }
        }
    }
}