using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Models {
    public abstract class AttackModel : MonoBehaviour {
        protected MovementModel movementModel;
        protected Transform player;
        
        public void SetupModel(Enemy enemy) {
            movementModel = enemy.movementModel;
            player = enemy.player;
        }
        
        public abstract void StartModel();

        /*
         * Performs an attack by the enemy. First thing this method should do is check whether attack
         * can be performed. If not, false should be returned and no action taken. If yes, attack should
         * take place and true returned, regardless if attack was performed successfully.
         */
        public abstract bool Attack();
    }
}

