using UnityEngine;

namespace Enemies.Models {
    public abstract class DeathModel : MonoBehaviour{
        
        protected MovementModel movementModel;
        protected Transform player;
        
        /*
         * Performs death by the enemy.
         * First thing this method should do is do whatever you want.
         * Second thing is removing GameObject.
         */
        public abstract void Die();
        
        public void SetupModel(Enemy enemy) {
            
        }

        public void StartModel() {
            
        }
    }
}