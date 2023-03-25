using DamageSystem.ReceiveDamage.Elementals.Elementals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DamageSystem.NewSpellSystem.SpellTypes.PulseFire {

    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Spells/PulseFire", order = 2)]
    public class PulseFireInfo : ScriptableObject {
        public float castTime;
        public float cooldown;
        public Rigidbody pellet;
        public int pelletLifeSpan;
        public PulseFire.CastType castBehaviour;
        public PulseFire.PelletType pelletBehaviour;
        public List<AttackElemental> elementals;
    }
}