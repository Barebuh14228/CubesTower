using DefaultNamespace;
using Settings;
using UnityEngine;

public class SpawnersContainer : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawnerPrefab;
    
    public void CreateSpawner(CubeSettings settings)
    {
        var spawner = Instantiate(_spawnerPrefab, transform);
        
        spawner.Setup(settings);
    }
}