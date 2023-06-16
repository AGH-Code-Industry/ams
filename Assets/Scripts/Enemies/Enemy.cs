using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Models;
using UnityEngine;

namespace Enemies {
    public class Enemy : MonoBehaviour {
        private PatrolModel _patrolModel;
        private ChaseModel _chaseModel;
        private AttackModel _attackModel;
        private MovementModel _movementModel;

        private bool _isPatrolling;
        private bool _isChasing;
        private bool _isAttacking;

        private Transform _player;

        private void Start() {
            _player = GameObject.Find("Player").transform;
            SetupModels();
        }

        private void Update() {
            if (_isPatrolling) {
                _isPatrolling = _patrolModel.Patrol();
                return;
            }
            if (!_chaseModel.Chase()) {
                return;
            }

            _attackModel.Attack();
        }

        private void SetupModels() {
            _movementModel = gameObject.GetComponent<MovementModel>();
            if (_movementModel is null) throw new Exception("Enemy must have MovementModel component added.");

            _attackModel = gameObject.GetComponent<AttackModel>();
            if (_attackModel is null) throw new Exception("Enemy must have AttackModel component added.");
            _attackModel.SetupModel(_movementModel, _player);

            _chaseModel = gameObject.GetComponent<ChaseModel>();
            if (_chaseModel is null) throw new Exception("Enemy must have ChaseModel component added.");
            _chaseModel.SetupModel(_movementModel, _player);

            _patrolModel = gameObject.GetComponent<PatrolModel>();
            if (_patrolModel is null) throw new Exception("Enemy must have PatrolModel component added.");
            _patrolModel.SetupModel(_movementModel, _player);
        }
    }
}
