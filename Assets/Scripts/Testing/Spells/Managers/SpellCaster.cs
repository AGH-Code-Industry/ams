using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ManaStorage))]
[RequireComponent(typeof(SpellStorage))]
public class SpellCaster : MonoBehaviour {

    [SerializeField] private Transform castSource;

    private float canCastAgain = 0f;

    private ManaStorage _manaStorage;
    private SpellStorage _spellStorage;

    private Spell _spellToCast { get { return _spellStorage.activeSpell; } }

    void Start() {
        _manaStorage = GetComponent<ManaStorage>();
        _spellStorage = GetComponent<SpellStorage>();
    }

    void Update() {
        if(Input.GetAxis("SpellCast") > 0 && CanCastSpell(_spellToCast)) {
            CastSpell(_spellToCast);

            canCastAgain = Time.time + _spellToCast._cooldown;
        }
    }

    private bool CanCastSpell(Spell spell) {
        return Time.time >= canCastAgain && _manaStorage.CanCastSpell(spell);
    }

    private void CastSpell(Spell spell) {
        _manaStorage.CastSpell(spell);

        GameObject spellObject;

        if(spell is Projectile) {
            Projectile projectile = (Projectile)spell;

            spellObject = Instantiate(projectile.model, castSource.position, castSource.rotation);
        } else {
            throw new System.Exception("Unknown spell type");
        }

        SpellEntity se = spellObject.AddComponent<SpellEntity>();
        se.Assign(spell);
    }
}
