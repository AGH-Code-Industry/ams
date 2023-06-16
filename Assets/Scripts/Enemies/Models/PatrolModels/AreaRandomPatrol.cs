using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Models.PatrolModels {
    public class AreaRandomPatrol : PatrolModel {
        
        private enum PatrolStatus {
            Moving,
            WaitingInCheckpoint
        }
        
        [Header("Patrol")]
        [SerializeField] [Range(1, 20)] [Tooltip("Distance to the player that triggers enemy to go into chase mode.")] 
        private float triggerDistance = 10;
        
        [Header("Checkpoints")]
        [SerializeField] [Range(0, 25)] [Tooltip("Number of checkpoints to follow.")]
        private ushort checkpointsCount = 3;
        [SerializeField] [Range(0, 25)] [Tooltip("Radius of patrol area from starting position of Enemy.")]
        private float areaRadius = 5;
        [SerializeField] [Range(0.1f, 6f)] [Tooltip("Time to spent at checkpoint.")]
        private float checkPointTime = 3f;

        private List<Vector3> _checkpoints;
        private float _timeLastCheckpointEntered;
        private bool _hasBeenHit;
        private PatrolStatus _patrolStatus;

        public override void StartModel() {
            _checkpoints = new List<Vector3>();
            while (_checkpoints.Count < checkpointsCount) {
                Vector3 offset = (Random.insideUnitCircle * areaRadius);
                Vector3 point = new Vector3(transform.position.x + offset.x, transform.position.y,
                    transform.position.z + offset.y);
                if (movementModel.IsDestinationValid(point)) {
                    _checkpoints.Add(point);
                }
            }
            _checkpoints.Add(transform.position); // Add current position to have at least one point
            
            foreach (var checkpoint in _checkpoints) {
                Debug.Log(checkpoint);
            }
            
            _timeLastCheckpointEntered = Time.time;
            _patrolStatus = PatrolStatus.WaitingInCheckpoint;
        }

        public override bool Patrol() {
            if (IsTriggered()) {
                movementModel.Stop();
                return true;
            }
            if (_patrolStatus == PatrolStatus.WaitingInCheckpoint) {
                if (Time.time - _timeLastCheckpointEntered > checkPointTime) {
                    GoToNextCheckpoint();
                }
            }else if (_patrolStatus == PatrolStatus.Moving) {
                CheckIfEnteredCheckpoint();
            }
            return false;
        }

        private void GoToNextCheckpoint() {
            var nextCheckpoint = _checkpoints[Random.Range(0, _checkpoints.Count)];
            _patrolStatus = PatrolStatus.Moving;
            movementModel.Move(nextCheckpoint);
        }

        private void CheckIfEnteredCheckpoint() {
            if (movementModel.IsDestinationReached()) {
                _patrolStatus = PatrolStatus.WaitingInCheckpoint;
                _timeLastCheckpointEntered = Time.time;
            }
        }

        private bool IsTriggered() {
            if (_hasBeenHit) return true;
            return Vector3.Distance(transform.position, player.position) < triggerDistance;
        }

        public void OnHitRegister() {
            _hasBeenHit = true;
        }
    }
}

