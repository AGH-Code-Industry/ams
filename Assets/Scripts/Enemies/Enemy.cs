using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Models;
using UnityEngine;

namespace Enemies {
    public class Enemy : MonoBehaviour {
        [Header("Enabled Models")]
        [SerializeField] private PatrolModel patrolModel;
        [SerializeField] private ChaseModel chaseModel;
        [SerializeField] private AttackModel attackModel;
        [SerializeField] private MovementModel movementModel;

        private bool _isPatrolling;
        private bool _isChasing;
        private bool _isAttacking;

        private void Start() {
            Setup();
        }

        private void Update() {
            if (_isPatrolling) {
                _isPatrolling = patrolModel.Patrol();
                return;
            }
            if (!chaseModel.Chase(new GameObject())) {
                return;
            }

            attackModel.Attack();
        }

        private void Setup() {
            movementModel = gameObject.GetComponent<MovementModel>();
            if (movementModel is null) throw new Exception("Enemy must have MovementModel component added.");

            attackModel = gameObject.GetComponent<AttackModel>();
            if (attackModel is null) throw new Exception("Enemy must have AttackModel component added.");

            chaseModel = gameObject.GetComponent<ChaseModel>();
            if (chaseModel is null) throw new Exception("Enemy must have ChaseModel component added.");

            patrolModel = gameObject.GetComponent<PatrolModel>();
            if (patrolModel is null) throw new Exception("Enemy must have PatrolModel component added.");
        }
    }
}
