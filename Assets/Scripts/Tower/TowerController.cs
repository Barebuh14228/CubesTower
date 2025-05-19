using System;
using System.Linq;
using Cube;
using DG.Tweening;
using DragAndDrop;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

namespace Tower
{
    public class TowerController : DropSubscriber<DraggingCube>
    {
        [SerializeField] private TowerModel _towerModel;
        [SerializeField] private EllipseDropZone _dropZone;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private UnityEvent _onCubeDropped;

        [Inject] private GameManager _gameManager;
        
        private Lazy<float> _bottomY;
        private Sequence _sequence;

        public TowerModel TowerModel => _towerModel;
        
        private void Start()
        {
            _bottomY = new (() => _rectTransform.GetWorldCornersArray().First().y);
        }
        
        public override void NotifyOnDrop(DraggingCube item)
        {
            var cube = item.Value;
            
            if (_sequence != null && _sequence.IsActive())
            {
                cube.DestroyCube();
                return;
            }
            
            var haveCubes = _towerModel.Cubes.Any();
            var cubeRect = cube.RectTransform.GetWorldRect();
            var cubeWidth = cubeRect.width;
            var offsetX = haveCubes ? Random.Range(0, cubeWidth) - cubeWidth / 2 : 0f;
            var dropPosition = GetDropPosition(cubeRect);
            
            var finalPositionRect = cubeRect;
            finalPositionRect.position = dropPosition + Vector2.right * offsetX;
            
            var isBounds = finalPositionRect.GetCorners()
                .Any(c => RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, c));
            
            if (!isBounds)
            {
                cube.DestroyCube();
                return;
            }
            
            _sequence = DOTween.Sequence();
            
            var points = new Vector3[]
            {
                item.RectTransform.position,
                finalPositionRect.center + Vector2.up * 70,
                finalPositionRect.center
            };
            
            cube.DragEventsProvider.IgnoreEvents();

            _sequence.OnStart(() => BlockTowerCubesDragging());
            _sequence.Append(cube.transform.DOPath(points, 0.5f, PathType.CatmullRom, PathMode.Sidescroller2D));
            _sequence.OnComplete(() =>
            {
                UnblockTowerCubesDragging();
                DropCube(cube);
                cube.DragEventsProvider.ListenEvents();
            });
            _sequence.Play();
        }

        public void DropCube(CubeController cubeController)
        {
            var cubeRectTransform = cubeController.RectTransform;
            cubeRectTransform.SetParent(_rectTransform,true);
            _towerModel.AddCube(cubeController);
            RecalculateBoundaries();
            _onCubeDropped?.Invoke();
            _gameManager.SaveState();
        }
        
        public void OnCubeDragged(CubeController cubeController)
        {
            if (!_towerModel.ContainCube(cubeController))
                return;
            
            var cubesToMoveDown = _towerModel.RemoveCube(cubeController);
            
            cubesToMoveDown.Reverse();
            
            BlockTowerCubesDragging();
            
            var dropHeight = cubeController.RectTransform.GetWorldRect().height;
            
            _sequence = DOTween.Sequence();
            
            foreach (var cube in cubesToMoveDown)
            {
                cube.DragEventsProvider.IgnoreEvents();
                
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
                
                _sequence.Append(tween);
            }
            
            _sequence.OnComplete(() =>
            {
                RecalculateBoundaries();
                UnblockTowerCubesDragging();
            });
        }
        
        public void RecalculateBoundaries()
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
        
        public void BlockTowerCubesDragging()
        {
            foreach (var cube in _towerModel.Cubes)
            {
                cube.DragEventsProvider.IgnoreEvents();
            }
        }
        
        public void UnblockTowerCubesDragging()
        {
            foreach (var cube in _towerModel.Cubes)
            {
                cube.DragEventsProvider.ListenEvents();
            }
        }
    }
}