using System;
using System.Linq;
using Cube;
using DefaultNamespace;
using DG.Tweening;
using DragAndDrop;
using UnityEngine;
using UnityEngine.Events;
using Zenject;
using Random = UnityEngine.Random;

namespace Tower
{
    public class TowerController : DragSubscriber<DraggingCube>
    {
        [SerializeField] private TowerModel _towerModel;
        [SerializeField] private EllipseDropZone _dropZone;
        [SerializeField] private RectTransform _rectTransform;
        [SerializeField] private UnityEvent _onCubeDropped;

        [Inject] private GameManager _gameManager;
        
        private Lazy<float> _bottomY;
        private Sequence _sequence;

        private bool HaveCubes => _towerModel.Cubes.Any();
        
        public TowerModel TowerModel => _towerModel;
        
        private void Start()
        {
            _bottomY = new (() => _rectTransform.GetWorldCornersArray().First().y);
        }
        
        public void OnCubeDropped(DraggingCube item)
        {
            var cube = item.Value;
            
            if (_sequence != null && _sequence.IsActive())
            {
                cube.DestroyCube();
                UITextLogger.Instance.LogText(TextProvider.Get("explode_miss"));
                return;
            }
            
            var cubeRect = cube.RectTransform.GetWorldRect();

            SetRectOnDropPosition(ref cubeRect);
            
            var inBounds = cubeRect.GetCorners()
                .Any(c => RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, c));
            
            if (!inBounds)
            {
                cube.DestroyCube();
                UITextLogger.Instance.LogText(TextProvider.Get("explode_no_space"));
                return;
            }
            
            var waypoints = new Vector3[]
            {
                item.RectTransform.position,
                cubeRect.center + Vector2.up * 70,
                cubeRect.center
            };
            
            cube.DragEventsRouter.IgnoreEvents();
            
            _sequence = DOTween.Sequence();
            _sequence.OnStart(OnDropStart);
            _sequence.Append(cube.transform.DOPath(waypoints, 0.5f, PathType.CatmullRom, PathMode.Sidescroller2D));
            _sequence.OnComplete(() => OnDropComplete(cube));
            _sequence.Play();
        }

        private void OnDropStart()
        {
            var uiLoggerText = HaveCubes 
                ? TextProvider.Get("set_at_top") 
                : TextProvider.Get("set_at_bottom");
            
            UITextLogger.Instance.LogText(uiLoggerText);

            BlockTowerCubesDragging();
        }

        private void OnDropComplete(CubeController cube)
        {
            UITextLogger.Instance.LogText(TextProvider.Get("set"));
            
            _towerModel.AddCube(cube);
            _gameManager.SaveState();
            
            cube.RectTransform.SetParent(_rectTransform,true);
            cube.DragEventsRouter.ListenEvents();
            
            RecalculateBoundaries();
            UnblockTowerCubesDragging();
            
            _onCubeDropped?.Invoke();
        }
        
        protected override void NotifyOnDrag(DraggingCube draggingItem)
        {
            var draggedCube = draggingItem.Value;
            
            if (!_towerModel.ContainCube(draggedCube))
                return;
            
            UITextLogger.Instance.LogText(TextProvider.Get("left_the_tower"));
            
            var cubesToMoveDown = _towerModel.RemoveCube(draggedCube);
            
            cubesToMoveDown.Reverse();
            
            _sequence = DOTween.Sequence();

            var fallHeight = draggedCube.RectTransform.GetWorldRect().height;
            var topCenter = HaveCubes ? GetTopCenterPosition() : default;
            
            foreach (var cube in cubesToMoveDown)
            {
                cube.DragEventsRouter.IgnoreEvents();
                
                var cubeRect = cube.RectTransform.GetWorldRect();
                var centerOffset = HaveCubes
                    ? Mathf.Abs(topCenter.x - cubeRect.center.x) 
                    : 0f;
                
                cubeRect.position -= Vector2.up * fallHeight;
                
                var tween = cube.RectTransform.DOMove(cubeRect.center, 0.2f);

                if (centerOffset < cubeRect.width / 2)
                {
                    _towerModel.AddCube(cube);
                    tween.OnComplete(() => _onCubeDropped?.Invoke());
                    topCenter = cubeRect.center;
                }
                else
                {
                    fallHeight += cubeRect.height;
                    tween.OnComplete(() =>
                    {
                        cube.DestroyCube();
                        UITextLogger.Instance.LogText(TextProvider.Get("explode_center_miss", centerOffset, cubeRect.width / 2));
                    });
                }
                
                _sequence.Append(tween);
            }

            _sequence.OnStart(BlockTowerCubesDragging);
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

        private void SetRectOnDropPosition(ref Rect cubeRect)
        {
            var cubeWidth = cubeRect.width;
            var offsetX = HaveCubes ? Random.Range(0, cubeWidth) - cubeWidth / 2 : 0f;
            var dropPosition = HaveCubes
                ? GetTopCenterPosition() - new Vector2(cubeWidth / 2, 0)
                : new Vector2(cubeRect.min.x, _bottomY.Value);
            
            cubeRect.position = dropPosition + new Vector2(offsetX, 0); // получаем финальное состояние Rect после дропа
        }

        private Vector2 GetTopCenterPosition()
        {
            var topCubeRect = _towerModel.Cubes.First().RectTransform.GetWorldRect();

            return new Vector2(topCubeRect.center.x, topCubeRect.max.y);
        }
        
        private void BlockTowerCubesDragging()
        {
            foreach (var cube in _towerModel.Cubes)
            {
                cube.DragEventsRouter.IgnoreEvents();
            }
        }
        
        private void UnblockTowerCubesDragging()
        {
            foreach (var cube in _towerModel.Cubes)
            {
                cube.DragEventsRouter.ListenEvents();
            }
        }
    }
}