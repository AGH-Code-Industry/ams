using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private Rigidbody _rigidbody;
    [SerializeField] Animator _anim;

    private bool _isFlying = true;
    private bool _isPlayingFlattenAnimation = false;
    private float _minYScale = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        PlayFlattenAnimation();
    }

    private void OnTriggerEnter(Collider other)
        // void OnCollisionEnter(Collision other) 
    {
        if (other.CompareTag("Player") && _isFlying)
        {
            // Destroy(gameObject);
            _rigidbody.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezePositionZ |
                                     RigidbodyConstraints.FreezeRotation;
            return;
        }

        if (other.name == "EnemyTesting" || other.name == "Cube")
        {
            _isFlying = false;
            _rigidbody.constraints = RigidbodyConstraints.FreezePosition | RigidbodyConstraints.FreezeRotation;
        }
    }

    private void PlayFlattenAnimation()
    {
        if (_isFlying || _isPlayingFlattenAnimation) return;

        _anim.enabled = true;
    }
}