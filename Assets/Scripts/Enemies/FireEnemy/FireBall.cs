using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;

public class FireBall : MonoBehaviour
{
    [SerializeField] private BoxCollider boxCollider;
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] Animator _anim;
    [SerializeField] private List<AttackElemental> elementals;
    

    private bool _isFlying = true;
    private bool _isPlayingFlattenAnimation = false;    
    private float _lastTimeAttackPlayer = -6f;
    private float _deltaTimeAttackPlayer = 5f;    
    private bool triggeredPlayerOnce = false;

    // Update is called once per frame
    void Update()
    {
        PlayFlattenAnimation();
    }    


    private void OnTriggerEnter(Collider other) {
        OnTriggerFloor(other);        
    }    

    private void OnTriggerFloor(Collider other) {
        if (other.name != "EnemyTesting" && other.name != "Cube" && !other.CompareTag("Floor")) return;        

        _isFlying = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;        
    }

    private void OnTriggerStay(Collider other) {
        OnTriggerPlayer(other);
    }    

    private void OnTriggerPlayer(Collider other) {
        if(!other.CompareTag("Player") || Time.time - _lastTimeAttackPlayer < _deltaTimeAttackPlayer ) return;        

        DamageInfo info = new DamageInfo(elementals , gameObject);
        other.GetComponent<Damageable>().TakeDamage(info);        
        _lastTimeAttackPlayer = Time.time;

        if(_isFlying && !triggeredPlayerOnce) {
            triggeredPlayerOnce = true;
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;  
            transform.position = new Vector3(other.transform.position.x, transform.position.y, other.transform.position.z);            
        }
    }

    private void PlayFlattenAnimation() {
        if (_isFlying || _isPlayingFlattenAnimation) return;

        _anim.enabled = true;
    }
}