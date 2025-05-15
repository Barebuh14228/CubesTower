using System;
using UnityEngine;

namespace Settings
{
    [Serializable]
    public class CubeSettings
    {
        [SerializeField] private Sprite _sprite;
        
        public Sprite Sprite => _sprite;
    }
}