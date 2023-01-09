using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class DummyEnemy : MonoBehaviour {
    private Animator _animator;
    private float _startTime;
    private float _avaitingTime;
    private void Start() {
        _animator = GetComponent<Animator>();
        _startTime = Time.time;
        _avaitingTime = Random.Range(2f, 5f);
    }

    public void OnDead() {
        Destroy(gameObject);
    }
}
