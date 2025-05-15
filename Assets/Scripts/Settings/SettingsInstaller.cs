using UnityEngine;
using Zenject;

namespace Settings
{
    [CreateAssetMenu(fileName = "SettingsInstaller", menuName = "Settings/SettingsInstaller", order = 1)]
    public class SettingsInstaller : ScriptableObjectInstaller
    {
        [SerializeField] private CubePresets _cubePresets;

        public override void InstallBindings()
        {
            Container.BindInstances(_cubePresets);
        }
    }
}