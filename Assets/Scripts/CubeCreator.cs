using Cube;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;

//todo objectpool
    
public class CubeCreator : MonoBehaviour
{
    [SerializeField] private CubeController _cubePrefab;
        
    public CubeController CreateCube(CubeSettings cubeSettings)
    {
        var cube = Instantiate(_cubePrefab);
        
        cube.Setup(cubeSettings);

        return cube;
    }

    public void ReturnToPool(CubeController cubeController)
    {
        Destroy(cubeController.gameObject);
    }
}