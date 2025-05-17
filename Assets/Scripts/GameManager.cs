using Cube;
using DG.Tweening;
using Settings;
using Tower;
using UnityEngine;
using Zenject;
    
public class GameManager : MonoBehaviour
{
    [SerializeField] private TowerController _towerController;
    
    [Inject] private CubeCreator _cubeCreator;
    [Inject] private CubePresets _cubePresets;
    [Inject] private UIController _uiController;
    
    private void Start()
    {
        CreateSpawners();
    }

    private void CreateSpawners()
    {
        var presets = _cubePresets.Presets;

        foreach (var settings in presets)
        {
            _uiController.SpawnersContainer.CreateSpawner(settings);
        }
    }

    public bool TryDropCubeOnTower(CubeController cubeController)
    {
        return _towerController.TryDropCube(cubeController);
    }

    public void DropCubeInHole(CubeController cubeController) //todo кидать много кубиков подряд
    {
        

        var path = new Vector3[]
        {
            cubeController.Model.RectTransform.GetWorldRect().center,
            _uiController.HoleBottomPoint.transform.position + Vector3.up * 350,
        };
        
        var seq = DOTween.Sequence();

        cubeController.transform.DORotate(new Vector3(0, 0, 720), 1.5f, RotateMode.FastBeyond360).SetLoops(-1, LoopType.Restart);
        seq
            .Append(cubeController.transform.DOPath(path, 0.5f, PathType.CatmullRom).SetEase(Ease.InQuad))
            .AppendCallback(() =>
            {
                cubeController.transform.SetParent(_uiController.HoleMask.transform, true);
            })
            .Append(cubeController.transform.DOMove(_uiController.HoleBottomPoint.transform.position, 0.2f).SetEase(Ease.Linear))
            .OnComplete(() =>
            {
                cubeController.ReturnToPool();
            })
            .Play();
    }

    public void NotifyCubeDragged(CubeController cubeController)
    {
        _towerController.NotifyCubeDragged(cubeController);
    }
}