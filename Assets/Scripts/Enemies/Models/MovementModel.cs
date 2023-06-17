using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemies.Models {
    public abstract class MovementModel : MonoBehaviour {

        [SerializeField] [Range(0, 20)] [Tooltip("Max movement speed of your entity")]
        protected float movementSpeed = 1;
        
        [SerializeField] [Range(0, 10)] [Tooltip("How fast your entity will achieve max speed")]
        protected float acceleration = 1;
        public void SetupModel(Enemy enemy) {
            
        }
        
        public abstract void StartModel();
        
        public abstract void Move(Vector3 destination);
        public abstract bool IsDestinationReached();
        public abstract bool IsDestinationValid(Vector3 destination);
        public abstract void Stop();

    }
}

