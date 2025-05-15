using UnityEngine;

namespace Settings
{
    [CreateAssetMenu(fileName = "CubePresets", menuName = "Settings/CubePresets", order = 1)]
    public class CubePresets : ScriptableObject
    {
        [SerializeField] private CubeSettings[] _presets;
        
        public CubeSettings[] Presets => _presets;
    }
}