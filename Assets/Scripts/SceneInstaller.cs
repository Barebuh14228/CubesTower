using UnityEngine;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [SerializeField] private CubeCreator _cubeCreator;
    [SerializeField] private UIController _uiController;
    [SerializeField] private GameManager _gameManager;

    public override void InstallBindings()
    {
        Container.Bind<CubeCreator>().FromInstance(_cubeCreator).AsSingle().NonLazy();
        Container.Bind<UIController>().FromInstance(_uiController).AsSingle().NonLazy();
        Container.Bind<GameManager>().FromInstance(_gameManager).AsSingle().NonLazy();
    }
}