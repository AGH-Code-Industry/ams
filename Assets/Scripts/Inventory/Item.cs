using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace team4.equipment {
    [CreateAssetMenu(fileName = "Item", menuName = "team4/Item")]
    public class Item : ScriptableObject
    {
        public string description;
        public Sprite icon;
        public int stackSize = Defaults.DEFAULT_STACK_SIZE;
    }
}
