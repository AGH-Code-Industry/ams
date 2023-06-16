using UnityEngine;

namespace Enemies.Models {
    public abstract class DeathModel : MonoBehaviour{
        /*
         * Performs death by the enemy.
         * First thing this method should do is do whatever you want.
         * Second thing is removing GameObject.
         */
        public abstract void Die();
    }
}