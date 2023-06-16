
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

        public override bool IsDestinationReached() {
            if (!_navMeshAgent.pathPending) {
                if (_navMeshAgent.remainingDistance <= _navMeshAgent.stoppingDistance) {
                    if (!_navMeshAgent.hasPath || _navMeshAgent.velocity.sqrMagnitude == 0f) {
                        return true;
                    }
                }
            }
            return false;
        }

        public override bool IsDestinationValid(Vector3 destination) {
            var newPath = new NavMeshPath();
            _navMeshAgent.CalculatePath(destination, newPath);
            return newPath.status == NavMeshPathStatus.PathComplete;
        }
        
        public override void Stop() {
            _navMeshAgent.ResetPath();
        }
        
        public void SetPath(NavMeshPath inputPath) {
            _navMeshAgent.SetPath(inputPath);
        }
    }
}