using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpellStorage : MonoBehaviour {

    [SerializeField] private Spell[] _spells;

    [SerializeField, ReadOnly] private int _activeSpellIndex = 0;
    [SerializeField] public Spell activeSpell { get; private set; }

    void Start() {
        if(_spells.Length == 0) {
            throw new System.Exception("Spell list is empty");
        }

        activeSpell = _spells[0];
    }

    void Update() {
        int delta = (int)Input.GetAxis("SpellCycle");

        if(delta < 0) {
            _activeSpellIndex = (_activeSpellIndex + 1) % _spells.Length;
        } else if(delta > 0) {
            _activeSpellIndex = (_activeSpellIndex - 1 + _spells.Length) % _spells.Length;
        }

        activeSpell = _spells[_activeSpellIndex];
    }

}
