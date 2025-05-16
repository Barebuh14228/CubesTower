using System.Linq;
using Cube;
using DragAndDrop;
using ModestTree;
using UnityEngine;

namespace Tower
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] private TowerModel _towerModel;
        [SerializeField] private EllipseBoundaries _boundaries;
        [SerializeField] private RectTransform _rectTransform;

        public bool TryDropCube(CubeController cubeController)
        {
            var droppedCubeWorldRect = cubeController.Model.RectTransform.GetWorldRect();
            
            if (_towerModel.Items.IsEmpty())
            {
                var worldRect = _rectTransform.GetWorldRect();

                var offsetY = worldRect.min.y - droppedCubeWorldRect.min.y;
                
                //todo animation
                
                cubeController.Model.RectTransform.position += new Vector3(0,offsetY,0);
                
                _towerModel.AddItem(cubeController.Model);
            }
            else
            {
                var cubeCorners = cubeController.Model.RectTransform.GetWorldCornersArray();
                
                if (cubeCorners.Any(_boundaries.IsPointInBoundaries))
                {
                    var topPoint = _towerModel.Items.Last().RectTransform.GetWorldRect().max;
                    var height = droppedCubeWorldRect.height;
                    
                    if (!RectTransformUtility.RectangleContainsScreenPoint(_rectTransform, new Vector3(topPoint.x, topPoint.y + height, 0)))
                    {
                        return false;
                    }

                    var topCubeWorldRect = _towerModel.Items.Last().RectTransform.GetWorldRect();
                    
                    var droppedCubeCenterPoint = new Vector2(droppedCubeWorldRect.min.x + droppedCubeWorldRect.width / 2, droppedCubeWorldRect.min.y);
                    
                    var offset = Random.Range(0, droppedCubeWorldRect.width / 2);
                    var centerPoint = new Vector2(topCubeWorldRect.max.x - topCubeWorldRect.width / 2 + offset, topCubeWorldRect.max.y);

                    var positionOffset = centerPoint - droppedCubeCenterPoint;
                    
                    //todo animation!
                    cubeController.Model.RectTransform.position += (Vector3) positionOffset;
                    
                    _towerModel.AddItem(cubeController.Model);
                }
                else
                {
                    return false;
                }
            }
            
            _boundaries.RecalculateBoundaries(_towerModel.Items.SelectMany(i => i.RectTransform.GetWorldCornersArray()).ToArray());
            
            return true;
        }

    }
}