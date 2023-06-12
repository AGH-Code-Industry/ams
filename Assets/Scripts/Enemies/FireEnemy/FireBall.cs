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

    public float height;

    private bool _isFlying = true;
    private bool _isPlayingFlattenAnimation = false;
    private float _minYScale = 0.3f;

    private bool _isTriggeringPlayer = false;
    private float _lastTimeAttackPlayer = 0.0f;
    private float _deltaTimeAttackPlayer = 2f;

    // Start is called before the first frame update
    void Awake()
    {
        CalculateHeight();
    }

    // Update is called once per frame
    void Update()
    {
        PlayFlattenAnimation();
    }

    private void CalculateHeight() 
    {        
        // if (!Physics.Raycast(transform.position, Vector3.down, out RaycastHit hit)) return;

        // Vector3 blockPos = hit.point;            
        // height = MathF.Abs(blockPos.y - transform.position.y)*2;        
        Debug.Log(boxCollider);
        float colliderHeight = boxCollider.bounds.size.y;

        // height = transform.TransformVector(Vector3.up * colliderHeight).magnitude;
        height = colliderHeight;
    }

    public float GetBottomY() {        
        // if (!Physics.Raycast(transform.position - new Vector3(0, 1,0), Vector3.down, out RaycastHit hit)) return 0;        

        // return hit.point.y; 
        Debug.Log(boxCollider);       
        Vector3 bottomPosition = boxCollider.bounds.min;

        // Vector3 globalBottomPosition = transform.TransformPoint(bottomPosition);

        // return globalBottomPosition.y;
        return bottomPosition.y;
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

    private void OnTriggerPlayer(Collider other){
        if(!other.CompareTag("Player") || Time.time -_lastTimeAttackPlayer < _deltaTimeAttackPlayer ) return;

        DamageInfo info = new DamageInfo(elementals , gameObject);
        other.GetComponent<Damageable>().TakeDamage(info);        
    }

    private void PlayFlattenAnimation()
    {
        if (_isFlying || _isPlayingFlattenAnimation) return;

        _anim.enabled = true;
    }
}