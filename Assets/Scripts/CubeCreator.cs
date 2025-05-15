using Cube;
using Settings;
using UnityEngine;
using UnityEngine.Serialization;

//todo objectpool
    
public class CubeCreator : MonoBehaviour
{
    [SerializeField] private CubeModel _cubePrefab;
        
    public CubeModel CreateCube(CubeSettings cubeSettings)
    {
        var cube = Instantiate(_cubePrefab);
            
        cube.SetupModel(cubeSettings.Sprite);

        return cube;
    }

    public void ReturnToPool(CubeModel cubeController)
    {
        Destroy(cubeController.gameObject);
    }
}