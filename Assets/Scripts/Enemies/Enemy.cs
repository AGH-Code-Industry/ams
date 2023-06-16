using System;
using System.Collections;
using System.Collections.Generic;
using Enemies.Models;
using UnityEngine;

namespace Enemies {
    public class Enemy : MonoBehaviour {
        [NonSerialized] public MovementModel movementModel;
        [NonSerialized] public Transform player;
        
        private PatrolModel _patrolModel;
        private ChaseModel _chaseModel;
        private AttackModel _attackModel;
        private DeathModel _deathModel;


        private bool _isTriggered;
        private bool _isChasing;
        private bool _isAttacking;

        private void Awake() {
            player = GameObject.Find("Player").transform;
            
            movementModel = gameObject.GetComponent<MovementModel>();
            _patrolModel = gameObject.GetComponent<PatrolModel>();
            _attackModel = gameObject.GetComponent<AttackModel>();
            _chaseModel = gameObject.GetComponent<ChaseModel>();
            _deathModel = gameObject.GetComponent<DeathModel>();
        }

        private void Start() {
            SetupModels();
            StartModels();
        }

        private void Update() {
            if (!_isTriggered) {
                _isTriggered = _patrolModel.Patrol();
                return;
            }
            Debug.Log("Out of patrol.");
            if (!_chaseModel.Chase()) {
                return;
            }
            
            _attackModel.Attack();
        }

        private void SetupModels() {
            if (movementModel is null) throw new Exception("Enemy must have MovementModel component added.");
            Debug.Log(movementModel.ToString());
            
            if (_attackModel is null) throw new Exception("Enemy must have AttackModel component added.");
            _attackModel.SetupModel(this);
            
            if (_chaseModel is null) throw new Exception("Enemy must have ChaseModel component added.");
            _chaseModel.SetupModel(this);
            
            if (_patrolModel is null) throw new Exception("Enemy must have PatrolModel component added.");
            
            movementModel.SetupModel(this);
            _patrolModel.SetupModel(this);
        }

        private void StartModels() {
            movementModel.StartModel();
            _attackModel.StartModel();
            _chaseModel.StartModel();
            _patrolModel.StartModel();
        }

        public void OnDie() {
            _deathModel.Die();
        }
    }
}
