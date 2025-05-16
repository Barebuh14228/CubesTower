using System.Collections.Generic;
using Cube;
using UnityEngine;

namespace Tower
{
    public class TowerModel : MonoBehaviour
    {
        private List<CubeModel> _items = new List<CubeModel>();

        public List<CubeModel> Items => _items;
        
        public void AddItem(CubeModel item)
        {
            _items.Add(item);
        }
    }
}