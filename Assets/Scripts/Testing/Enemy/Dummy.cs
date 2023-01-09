using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : Enemy {
    [SerializeField] private TMPro.TMP_Text _healthText;
    [SerializeField] private TMPro.TMP_Text _spellText;

    private new void Start() {
        base.Start();

        UpdateDebugDisplay(0f, "None");

        OnDamageReceived += UpdateDebugDisplay;
    }

    void UpdateDebugDisplay(float dmg, string source) {
        return;
        _healthText.text = $"Health: {_currentHP}/{_maxHP}";
        _spellText.text = $"Spell: {source} ({dmg} dmg)";
    }
}
