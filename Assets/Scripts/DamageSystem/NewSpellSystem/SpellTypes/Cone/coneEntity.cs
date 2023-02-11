using DamageSystem.NewSpellSystem.Core;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.SpellTypes.Cone
{
    public class coneEntity : MonoBehaviour
    {
        public DamageInfo damageInfo;
        float tickRate = 0.5f;

        float cooldown = 0f;
        GameObject origin;
        bool setup = false;

        List<Damageable> enemies = new List<Damageable>();
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Damageable>())
            {
                enemies.Add(other.gameObject.GetComponent<Damageable>());
            }
        }

        private void OnTriggerExit(Collider other)
        {

            if(enemies.Contains(other.GetComponent<Damageable>()))
            {
                enemies.Remove(other.GetComponent<Damageable>());
            }
        }

        public void AssignDamageInfo(List<AttackElemental> dmg, GameObject caster)
        {
            damageInfo.elementals = dmg;
            origin = caster;
            setup = true;
        }

        public void SetTickRate(float rate)
        {
            tickRate = rate;
        }

        private void OnEnable()
        {
            enemies.Clear();
        }

        private void Update()
        {
            TickRate();
        }

        //Damage over time
        void TickRate()
        {
            if (Time.time >= cooldown)
            {
                foreach (Damageable enemy in enemies)
                {
                    enemy.TakeDamage(damageInfo);
                }
                cooldown = Time.time + tickRate;
            }
        }
    }
}
