using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

namespace ScriptableObjects {
    public class ItemSO : ScriptableObject {
        [Header("Basic information")]
        public uint id;
        public string description;
        public Image icon;
    }
}

