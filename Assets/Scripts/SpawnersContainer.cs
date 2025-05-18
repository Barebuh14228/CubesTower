using Settings;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnersContainer : MonoBehaviour
{
    [SerializeField] private CubeSpawnContainer _spawnContainerPrefab;
    
    public void CreateSpawner(CubeSettings settings)
    {
        var spawner = Instantiate(_spawnContainerPrefab, transform);
        
        spawner.Setup(settings);
    }
}