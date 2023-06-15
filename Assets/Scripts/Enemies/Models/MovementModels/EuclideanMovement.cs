
using System;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;

namespace Enemies.Models.MovementModels {
    public class EuclideanMovement : MovementModel{
        private NavMeshAgent _navMeshAgent;

        public override void Move(Vector3 destination) {
            if (!IsDestinationValid(destination)) {
                return;
            }
            
            _navMeshAgent.Move(destination);
        }

        public override bool IsDestinationValid(Vector3 destination) {
            var newPath = new NavMeshPath();
            _navMeshAgent.CalculatePath(destination, newPath);
            return newPath.status == NavMeshPathStatus.PathComplete;
        }
    }
}