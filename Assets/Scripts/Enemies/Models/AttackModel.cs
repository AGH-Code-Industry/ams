using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Models {
    public class AttackModel : MonoBehaviour
    {
        protected MovementModel movementModel;
        
        public void SetupMovementModel(MovementModel movModel) {
            movementModel = movModel;
        }
        
        public bool Attack() {
            return true;
        }
    }
}

