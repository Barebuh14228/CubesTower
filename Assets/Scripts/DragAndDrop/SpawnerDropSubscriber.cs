using UnityEngine;

namespace DragAndDrop
{
    public class SpawnerDropSubscriber : DropSubscriber<DraggingCube>
    {
        [SerializeField] private CubeSpawnContainer _spawnContainer;
        
        public override void NotifyOnDrop(DraggingCube item)
        {
            _spawnContainer.SetCube(item.Value, false);
        }
    }
}