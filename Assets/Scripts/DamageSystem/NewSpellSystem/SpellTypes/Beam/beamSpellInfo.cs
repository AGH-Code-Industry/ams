using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.ReceiveDamage.Elementals.Elementals;

namespace DamageSystem.NewSpellSystem.SpellTypes.Beam
{
    [CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/Spells/Beam", order = 6)]
    public class beamSpellInfo : ScriptableObject
    {
        public float tickRate = 0.5f;
        public List<AttackElemental> elementals;
    }
}