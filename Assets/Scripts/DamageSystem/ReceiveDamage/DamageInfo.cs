using System.Collections.Generic;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
using UnityEngine;

namespace DamageSystem.ReceiveDamage.Elementals
{
    public struct DamageInfo
    {
        public List<AttackElemental> elementals;
        public GameObject caster;
    }
}