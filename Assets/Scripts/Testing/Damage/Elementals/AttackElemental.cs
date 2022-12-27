using UnityEngine;

namespace Testing.Damage.Elementals {
    
    [CreateAssetMenu(fileName = "new AttackElemental", menuName = "ScriptableObjects/AttackElemental")]
    public class AttackElemental : Elemental {
        public int elementalDamage;
        public int effectValue;
    }
}