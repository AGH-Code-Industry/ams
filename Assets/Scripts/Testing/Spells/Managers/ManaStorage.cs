using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManaStorage : MonoBehaviour {

    [SerializeField] private float _maxMana = 50f;
    [SerializeField] private float _manaRegen = 4f;

    [SerializeField] public float _currentMana { get; private set; } = 0f;
    
    void Start() {
        _currentMana = _maxMana * 0.5f;
    }

    void Update() {
        _currentMana = Mathf.Clamp(_currentMana + _manaRegen * Time.deltaTime, 0, _maxMana);
    }

    public bool CanCastSpell(Spell spell) {
        return _currentMana > spell._manaCost;
    }

    public void CastSpell(Spell spell) {
        _currentMana -= spell._manaCost;
    }
}
