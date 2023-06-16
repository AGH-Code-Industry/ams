using System.Collections;
using System.Collections.Generic;
using Enemies.Models;
using UnityEngine;

namespace Enemies.Models.ChaseModels {
    public class SimpleChaise : ChaseModel {
        [Header("Chase")] [SerializeField] [Range(0, 30)] [Tooltip("Range at which enemy can stop chasing and attack.")]
        private float targetRange = 5f;
        
        public override void StartModel() {
            
        }

        public override bool Chase() {
            if (Vector3.Distance(transform.position, player.position) < targetRange) {
                movementModel.Stop();
                return true;
            }
            movementModel.Move(player.position);
            return false;
        }
    }
}

