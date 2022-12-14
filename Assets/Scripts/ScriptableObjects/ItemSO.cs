using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ScriptableObjects {
    public class ItemSO : ScriptableObject {
        [Header("Basic information")]
        public uint id;
        public string itemName;
        public string description;
        [SerializeField] public Sprite icon;
    }
}

