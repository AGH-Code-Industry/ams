using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.ReceiveDamage.Elementals
{
    [CreateAssetMenu(fileName = "new Entity", menuName="ScriptableObjects/EntityInfo")]
    public class EntitySO : ScriptableObject {
        public bool isInvincible;
        public bool isImmortal;
        public int maxHealth;
        public bool showDamagePopup = true;
    }
}