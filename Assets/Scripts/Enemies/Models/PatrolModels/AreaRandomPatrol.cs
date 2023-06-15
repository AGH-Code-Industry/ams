using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Models;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemies.Models.PatrolModels {
    public class AreaRandomPatrol : PatrolModel {
        [Header("Patrol")]
        [SerializeField] [Tooltip("Distance to the player that triggers enemy to go into chase mode.")] 
        private float triggerDistance = 10;
        
        [Header("Checkpoints")]
        [SerializeField] [Range(0, 25)] [Tooltip("Number of checkpoints to follow.")]
        private ushort checkpointsCount = 3;
        [SerializeField] [Range(0, 25)] [Tooltip("Radius of patrol area from starting position of Enemy.")]
        private float areaRadius = 5;
        [SerializeField] [Tooltip("Time to spent at checkpoint.")]
        private float checkPointTime = 3f;
        
        private List<Vector3> _checkpoints;

        private void Start() {
            _checkpoints = new List<Vector3>();
            while (_checkpoints.Count < checkpointsCount) {
                Vector3 point = Random.insideUnitCircle * areaRadius;
                // TODO: Add check if point is a valid NavMesh point
                _checkpoints.Add(point);
            }
            _checkpoints.Add(transform.position); // Add current position to have at least one point
        }

        public override bool Patrol() {
            return true;
        }
    }
}

