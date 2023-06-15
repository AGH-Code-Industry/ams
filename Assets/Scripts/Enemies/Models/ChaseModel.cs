using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Models {
    public class ChaseModel : MonoBehaviour {
        protected MovementModel movementModel;
        
        public void SetupMovementModel(MovementModel movModel) {
            movementModel = movModel;
        }
    
        public bool Chase(GameObject go) {
            return true;
        }
    } 
}

