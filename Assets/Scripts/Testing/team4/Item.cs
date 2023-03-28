using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace team4.equipment {
    public abstract class Item : MonoBehaviour
    {
        public abstract string itemName {get; }
        public abstract string description {get; }
        public virtual int stackSize {
            get => Defaults.DEFAULT_STACK_SIZE;
            set => stackSize = value;
        }

        void Start()
        {
            
        }

        void Update()
        {
            
        }
    }

}
