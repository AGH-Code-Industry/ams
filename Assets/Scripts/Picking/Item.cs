using UnityEngine;

namespace testing {
    [CreateAssetMenu(fileName = "New Item", menuName = "Testing/Item/Create New Item")]
    public class Item : ScriptableObject
    {
        public int id;
        public string itemName;
        public int value;
        
        public Sprite icon;
        
    }
}