using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Cube;
using DefaultNamespace;
using DG.Tweening;
using DG.Tweening.Core;
using DG.Tweening.Plugins.Options;
using DragAndDrop;
using DragEventsUtils;
using ModestTree;
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
        
        private void Start()
        {
            _worldRect = new (() => _rectTransform.GetWorldRect());
        }

        public bool CanDropCube(CubeController cubeController, out Rect finalPositionRect)
        {
            finalPositionRect = default;
            
            //todo cant drop while moving cubes down
            
            var cubeRect = cubeController.Model.RectTransform.GetWorldRect();
            var cubeWidth = cubeRect.width;
            var offsetX = Random.Range(0, cubeWidth) - cubeWidth / 2;
            
            finalPositionRect = cubeRect;
            finalPositionRect.position = GetDropPosition(finalPositionRect) + Vector2.right * offsetX;
            
            return _rectTransform.ContainRect(finalPositionRect);
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
            var worldRect = cubeController.Model.RectTransform.GetWorldRect();

            cubesToMoveDown.Reverse();
            
            var sequence = DOTween.Sequence();
            
            foreach (var cube in cubesToMoveDown)
            {
                sequence.Append(cube.RectTransform.DOMoveY(cube.RectTransform.position.y - worldRect.height, .2f));
                sequence.AppendInterval(0.05f);
            }
            
            cubesToMoveDown.ForEach(_towerModel.AddCube);
            
            sequence.OnComplete(RecalculateBoundaries);
        }
        
        private void RecalculateBoundaries()
        {
            _dropZone.RecalculateBoundaries(_towerModel.GetCubesCorners());
        }

        private Vector2 GetDropPosition(Rect cubeRect)
        {
            var towerCenter = !_towerModel.HaveCubes()
                ? cubeRect.center.x
                : _towerModel.TopCubeRect.center.x;
            
            var minY = !_towerModel.HaveCubes()
                ? _worldRect.Value.min.y
                : _towerModel.TopCubeRect.max.y;

            var minX = towerCenter - cubeRect.width / 2;
            
            return new Vector2(minX, minY);
        }
    }
}