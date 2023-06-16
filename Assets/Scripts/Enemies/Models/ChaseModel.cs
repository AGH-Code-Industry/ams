using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Models {
    public abstract class ChaseModel : MonoBehaviour {
        protected MovementModel movementModel;
        protected Transform player;
        
        public void SetupModel(Enemy enemy) {
            movementModel = enemy.movementModel;
            player = enemy.player;
        }
        
        public abstract void StartModel();
    
        /*
         * This method should return true if player is in the chase range. If not, method should
         * continue with chasing and return false regardless of chase outcome in this frame.
         */
        public abstract bool Chase();
    } 
}

