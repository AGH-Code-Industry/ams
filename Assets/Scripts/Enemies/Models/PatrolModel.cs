using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Models {
    public abstract class PatrolModel : MonoBehaviour {
        protected MovementModel movementModel;
        
        public void SetupMovementModel(MovementModel movModel) {
            movementModel = movModel;
        }
        
        public abstract bool Patrol();
    }
}

