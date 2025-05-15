using System.Collections.Generic;
using System.Linq;
using Cube;
using UnityEngine;

public class CubesPalette : MonoBehaviour //todo rename
{
    [SerializeField] private CubeContainer _cubeContainerPrefab;

    private List<CubeContainer> _containers;
        
    public void AddCube(CubeController cubeController)
    {
        var cubeContainer = Instantiate(_cubeContainerPrefab, transform);
            
        cubeContainer.transform.SetParent(transform);
        cubeContainer.Setup(cubeController);
            
        _containers ??= new List<CubeContainer>();
        _containers.Add(cubeContainer);
    }

    public void ReplaceCube(CubeController cubeController)
    {
        _containers.First(c => c.WaitForReplace).Setup(cubeController);
    }
}