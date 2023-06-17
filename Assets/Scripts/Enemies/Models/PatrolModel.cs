using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Models {
    public abstract class PatrolModel : MonoBehaviour {
        protected MovementModel movementModel;
        protected Transform player;
        
        public void SetupModel(Enemy enemy) {
            movementModel = enemy.movementModel;
            player = enemy.player;
        }

        public abstract void StartModel();
        
        /*
         * This method should return true at the if player has been in patrol range and triggered the
         * attack. If not, method should continue with its normal behaviour and return false regardless
         * of whether player is in range of triggering an enemy.
         */
        public abstract bool Patrol();
    }
}

