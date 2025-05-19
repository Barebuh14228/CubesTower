using System;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class CubeSettings
    {
        [SerializeField] private Color _cubeColor;
        
        public Color Color => _cubeColor;
    }
}