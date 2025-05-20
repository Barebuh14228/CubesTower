using DragAndDrop;
using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private GameManager _gameManager;
    [SerializeField] private DraggingController _draggingController;
    [SerializeField] private CubesPool _cubesPool;

    public override void InstallBindings()
    {
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
        Container.Bind<DraggingController>().FromInstance(_draggingController).AsSingle().NonLazy();
        Container.Bind<CubesPool>().FromInstance(_cubesPool).AsSingle().NonLazy();
    }
}