using System.Linq;
using DragAndDrop;
using UnityEngine;

namespace Tower
{
    public class TowerController : MonoBehaviour
    {
        [SerializeField] private TowerModel _towerModel;
        [SerializeField] private EllipseDropZone _dropZone;

        private void Awake()
        {
            _dropZone.OnItemDropped += OnItemDropped;
        }

        private void OnItemDropped(DropItem item)
        {
            //todo необходимо расчитать точку приземления!
            //todo проверить точку преземления через базовый метод
            
            _towerModel.AddItem(item);
            _dropZone.RecalculateBoundaries(_towerModel.Items.SelectMany(i => i.GetCorners()).ToArray());
        }
    }
}