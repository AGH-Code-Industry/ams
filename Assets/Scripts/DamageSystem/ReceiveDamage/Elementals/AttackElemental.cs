using UnityEngine;

namespace DamageSystem.ReceiveDamage.Elementals.Elementals {
    
    [CreateAssetMenu(fileName = "new AttackElemental", menuName = "ScriptableObjects/AttackElemental")]
    public class AttackElemental : Elemental {
        public int elementalDamage;
        public int effectValue;
    }
}