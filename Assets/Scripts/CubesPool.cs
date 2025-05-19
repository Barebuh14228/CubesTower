using Cube;
using DG.Tweening;
using Save;
using Settings;
using UnityEngine;
using UnityEngine.Pool;
    
public class CubesPool : MonoBehaviour
{
    [SerializeField] private CubeController _cubePrefab;

    private ObjectPool<CubeController> _cubesPool;

    private void Awake()
    {
        _cubesPool = new ObjectPool<CubeController>(
            InstantiateCube, 
            ActivateCube, 
            DeactivateCube, 
            DestroyCube, maxSize: 10);
    }

    private CubeController InstantiateCube()
    {
        return Instantiate(_cubePrefab, transform);
    }

    private void ActivateCube(CubeController cube)
    {
        cube.gameObject.SetActive(true);
        cube.GenerateId();
    }
    
    private void DeactivateCube(CubeController cube)
    {
        cube.gameObject.SetActive(false);
        cube.transform.SetParent(transform, false);
        cube.ResetState();
    }

    private void DestroyCube(CubeController cube)
    {
        cube.DOKill();
        Destroy(cube.gameObject);
    }
    
    public CubeController Get(CubeSettings cubeSettings)
    {
        var cube = Get();
        
        cube.Setup(cubeSettings.Color);

        return cube;
    }

    public CubeController Get()
    {
        return _cubesPool.Get();
    }

    public void Release(CubeController cubeController)
    {
        _cubesPool.Release(cubeController);
    }
}