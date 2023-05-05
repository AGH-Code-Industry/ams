using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public abstract class Enemy : MonoBehaviour {

    [SerializeField] protected float _maxHP = 100f;
    [SerializeField] protected float _currentHP;
    [SerializeField] protected bool _immortal = false; // HP will never go below 1

    public delegate void OnDamageReceivedDelegate(float amount, string source);
    public event OnDamageReceivedDelegate OnDamageReceived;

    public delegate void OnDeadDelegate();
    public event OnDeadDelegate OnDead;

    protected void Start() {
        _currentHP = _maxHP;
    }

    public float DealDamage(float amount, string source) {
        // change amount by resistances etc
        float delta = amount;

        _currentHP = Mathf.Clamp(_currentHP - delta, (_immortal ? 1 : 0), _maxHP);

        OnDamageReceived?.Invoke(delta, source);

        if(CheckIfDefeated() == true) {
            Destroy(gameObject);

            OnDead?.Invoke();
        }

        return delta;
    }

    protected bool CheckIfDefeated() {
        return !_immortal && _currentHP <= 0;
    }
}
