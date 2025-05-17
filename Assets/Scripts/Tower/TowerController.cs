using System;
using System.Linq;
using Cube;
using DG.Tweening;
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

        private Lazy<Rect> _worldRect;

        //todo block from dropping while animating
        
        private void Start()
        {
            _worldRect = new (() => _rectTransform.GetWorldRect());
        }

        public void NotifyCubeDragged(CubeController cubeController)
        {
            if (!_towerModel.ContainItem(cubeController.Model))
                return;
            
            //todo учесть что может остаться 0
            
            var worldRect = cubeController.Model.RectTransform.GetWorldRect();
            var startDropping = false;
            
            var sequence = DOTween.Sequence();
            
            foreach (var cubeModel in _towerModel.Items)
            {
                if (cubeModel.Equals(cubeController.Model))
                {
                    startDropping = true;
                    continue;
                }
                
                if (!startDropping)
                    continue;

                sequence.Append(cubeModel.RectTransform.DOMoveY(cubeModel.RectTransform.position.y - worldRect.height, .2f));
                sequence.AppendInterval(0.05f);
            }
            
            _towerModel.RemoveItem(cubeController.Model);
            
            sequence
                .OnComplete(RecalculateBoundaries)
                .Play();
        }
        
        public void TryDropCube(CubeController cubeController)
        {
            var cubeWorldRect = cubeController.Model.RectTransform.GetWorldRect();
            var dropRect = CalculateDropRect(cubeWorldRect);
            
            if (!_rectTransform.ContainRect(dropRect))
            {
                //todo destroy
                return;
            }
                    
            _towerModel.AddItem(cubeController.Model);
                
            var cubeRectTransform = cubeController.Model.RectTransform;
            cubeRectTransform.SetParent(_rectTransform,true);
            
            var seq = DOTween.Sequence();
            var points = new Vector3[]
            {
                cubeWorldRect.center,
                dropRect.center + Vector2.up * 70,
                dropRect.center
            };

            seq.Append(cubeController.transform.DOPath(points, 0.5f, PathType.CatmullRom, PathMode.Sidescroller2D));
            seq.OnComplete(RecalculateBoundaries).Play();
        }

        private void RecalculateBoundaries()
        {
            _dropZone.RecalculateBoundaries(_towerModel.Items.SelectMany(i => i.RectTransform.GetWorldCornersArray()).ToArray());
        }
        
        private Rect CalculateDropRect(Rect worldRect)
        {
            var offset = CalculateDropOffset(worldRect);
            worldRect.position += offset;
            
            return worldRect;
        }
        
        private Vector2 CalculateDropOffset(Rect worldRect)
        {
            var lowCenter = worldRect.center - new Vector2(0, worldRect.height / 2);
            var towerHeight = _towerModel.Items.IsEmpty()
                ? _worldRect.Value.min.y
                : _towerModel.Items.Last().RectTransform.GetWorldRect().max.y;

            var towerCenter = _towerModel.Items.IsEmpty()
                ? worldRect.center.x
                : _towerModel.Items.Last().RectTransform.GetWorldRect().center.x;

            var centerOffset = _towerModel.Items.IsEmpty() 
                ? 0 
                : Random.Range(0, worldRect.width) - worldRect.width / 2;

            return new Vector2(towerCenter + centerOffset, towerHeight) - lowCenter;
        }
    }
}