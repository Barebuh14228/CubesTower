using Cube;
using Settings;
using UnityEngine;

//todo objectpool
    
public class CubeCreator : MonoBehaviour
{
    [SerializeField] private CubeController _cubeViewPrefab;
        
    public CubeController CreateCube(CubeSettings cubeSettings)
    {
        var cube = Instantiate(_cubeViewPrefab);
            
        cube.Setup(cubeSettings.Sprite);

        return cube;
    }
        
    public CubeController CreateCube(CubeController cubeController)
    {
        return Instantiate(cubeController);
    }

    public void ReturnToPool(CubeController cubeController)
    {
        Destroy(cubeController.gameObject);
    }
}