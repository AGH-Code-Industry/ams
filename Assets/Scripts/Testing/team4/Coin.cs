using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace team4.equipment {

    public class Coin : Item
    {
        [SerializeField] private string _name = "Coin";
        [SerializeField] private string _description = "This is a coin";

        public override string itemName {
            get => _name;
        }
        public override string description {
            get => _description;
        }
    }

}
