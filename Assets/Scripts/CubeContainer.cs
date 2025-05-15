using Cube;
using UnityEngine;

public class CubeContainer : MonoBehaviour
{
    [SerializeField] private RectTransform _container;
    [SerializeField] private LayoutComponentsDisabler _layoutComponentsDisabler;
    
    private CubeController _currentCube;

    public bool WaitForReplace { get; private set; }
    
    public void Setup(CubeController cubeController)
    {
        _currentCube = cubeController;
        _currentCube.transform.SetParent(_container, false);
        _layoutComponentsDisabler.RebuildAndDisable();
        WaitForReplace = false;
    }
    
    public void OnPressAnimationComplete()
    {
        _currentCube.SetCubeAsDraggableTarget();
        WaitForReplace = true;
    }
}
