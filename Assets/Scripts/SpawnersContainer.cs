using System.Collections.Generic;
using DefaultNamespace;
using Settings;
using UnityEngine;

public class SpawnersContainer : MonoBehaviour
{
    [SerializeField] private CubeSpawner _spawnerPrefab;

    private List<CubeSpawner> _spawners;
    
    public void CreateSpawner(CubeSettings settings)
    {
        _spawners ??= new List<CubeSpawner>();
        
        var spawner = Instantiate(_spawnerPrefab, transform);
            
        spawner.transform.SetParent(transform);
        spawner.SetSettings(settings);
        spawner.SpawnCube();
        
        _spawners.Add(spawner);
    }

    public void RespawnCubes()
    {
        _spawners.ForEach(s => s.RespawnIfEmpty());
    }
}