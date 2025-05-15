using DefaultNamespace;
using UnityEngine;
using Zenject;

public class CubeContainer : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    
    [Inject] private CubeCreator _cubeCreator;
    
    private CubeController _currentCube;

    public void Setup(CubeController cubeController)
    {
        _currentCube = cubeController;
        _currentCube.transform.SetParent(_container);
    }
    
    public void OnPressAnimationComplete()
    {
        _currentCube.SetCubeAsDraggableTarget();
    }
}
