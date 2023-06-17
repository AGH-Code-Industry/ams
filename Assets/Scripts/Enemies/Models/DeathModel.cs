using UnityEngine;

namespace Enemies.Models {
    public abstract class DeathModel : MonoBehaviour {

        protected MovementModel movementModel;
        protected Transform player;

        public void SetupModel(Enemy enemy) {
            movementModel = enemy.movementModel;
            player = enemy.player;
        }

        /*
         * Performs death by the enemy.
         * First thing this method should do is do whatever you want.
         * Second thing is removing GameObject.
         */
        public abstract void Die();

        public abstract void StartModel();
    }
}