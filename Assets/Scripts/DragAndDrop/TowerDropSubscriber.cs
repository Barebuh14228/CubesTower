using Tower;
using UnityEngine;

namespace DragAndDrop
{
    public class TowerDropSubscriber : DropSubscriber<DraggingCube>
    {
        [SerializeField] private TowerController _towerController;
        
        public override void Drop(DraggingCube item)
        {
            _towerController.TryDropCube(item.Item);
        }
    }
}