using System;
using Cube;
using DG.Tweening;
using DragAndDrop;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tower
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] private TowerModel _towerModel;
        [SerializeField] private EllipseDropZone _dropZone;
        [SerializeField] private RectTransform _rectTransform;

        private Lazy<Rect> _worldRect; //todo remove
        private Sequence _dropSequence;
        
        private void Start()
        {
            _worldRect = new (() => _rectTransform.GetWorldRect());
        }

        public bool CanDropCube(CubeController cubeController, out Rect finalPositionRect)
        {
            finalPositionRect = default;

            if (_dropSequence != null && _dropSequence.IsActive())
                return false;
            
            var cubeRect = cubeController.Model.RectTransform.GetWorldRect();
            var cubeWidth = cubeRect.width;
            var offsetX = Random.Range(0, cubeWidth) - cubeWidth / 2;
            
            finalPositionRect = cubeRect;
            finalPositionRect.position = GetDropPosition(finalPositionRect) + Vector2.right * offsetX;
            
            return _rectTransform.ContainsScreenPoints(finalPositionRect.GetCorners());
        }

        public void OnCubeDropped(CubeController cubeController)
        {
            var cubeRectTransform = cubeController.Model.RectTransform;
            cubeRectTransform.SetParent(_rectTransform,true);
            _towerModel.AddCube(cubeController.Model);
            RecalculateBoundaries();
        }
        
        public void OnCubeDragged(CubeController cubeController)
        {
            if (!_towerModel.ContainCube(cubeController.Model))
                return;
            
            
            var cubesToMoveDown = _towerModel.RemoveCube(cubeController.Model);
            
            cubesToMoveDown.Reverse();
            cubesToMoveDown.ForEach(c => c.BlockDragging());
            
            _towerModel.BlockDragging();
            
            var dropHeight = cubeController.Model.RectTransform.GetWorldRect().height;
            _dropSequence = DOTween.Sequence();
            
            foreach (var cube in cubesToMoveDown)
            {
                var topCubeRect = _towerModel.GetTopCubeRect();
                var cubeRect = cube.RectTransform.GetWorldRect();
                var dropPosition = cubeRect.center + dropHeight * Vector2.down;
                var centerOffset = _towerModel.HaveCubes() ? Mathf.Abs(topCubeRect.center.x - cubeRect.center.x) : 0f;
                
                var tween = cube.RectTransform.DOMove(dropPosition, 0.2f);

                if (centerOffset < cubeRect.width / 2)
                {
                    _towerModel.AddCube(cube); //todo
                }
                else
                {
                    dropHeight += cubeRect.height;
                    tween.OnComplete(() => cube.CallDestroy());
                }
                
                _dropSequence.Append(tween);
                _dropSequence.AppendInterval(0.05f);
            }
            
            _dropSequence.OnComplete(() =>
            {
                _towerModel.UnblockDragging();
                RecalculateBoundaries();
            });
        }
        
        private void RecalculateBoundaries()
        {
            _dropZone.RecalculateBoundaries(_towerModel.GetCubesCorners());
        }

        private Vector2 GetDropPosition(Rect cubeRect)
        {
            var topCubeRect = _towerModel.GetTopCubeRect();
            var towerCenter = !_towerModel.HaveCubes()
                ? cubeRect.center.x
                : topCubeRect.center.x;
            
            var minY = !_towerModel.HaveCubes()
                ? _worldRect.Value.min.y
                : topCubeRect.max.y;

            var minX = towerCenter - cubeRect.width / 2;
            
            return new Vector2(minX, minY);
        }
    }
}