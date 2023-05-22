using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] Animator _anim;

    public float height;

    private bool _isFlying = true;
    private bool _isPlayingFlattenAnimation = false;
    private float _minYScale = 0.3f;

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
        RaycastHit hit;
        if (Physics.Raycast(transform.position, Vector3.down, out hit)) {            
            Vector3 blockPos = hit.point;
            
            height = MathF.Abs(blockPos.y - transform.position.y)*2;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.name != "EnemyTesting" && other.name != "Cube") return;        

        _isFlying = false;
        _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;        
    }

    private void PlayFlattenAnimation()
    {
        if (_isFlying || _isPlayingFlattenAnimation) return;

        _anim.enabled = true;
    }
}