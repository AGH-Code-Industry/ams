using DamageSystem.NewSpellSystem.Core;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using UnityEngine.ProBuilder.Shapes;

namespace DamageSystem.NewSpellSystem.SpellTypes.Cone
{
    public class coneEntity : MonoBehaviour
    {
        public DamageInfo damageInfo;
        float tickRate = 0.5f;
        VisualEffect vfx;
        float cooldown = 0f;
        GameObject origin;
        bool active = false;

        List<Damageable> enemies = new List<Damageable>();

        private void Start()
        {
            GetComponent<MeshCollider>().enabled = false;
            vfx = GetComponentInChildren<VisualEffect>();
            vfx.Stop();
        }

        public void Activate()
        {
            if (!active)
            {
                vfx.Play();
            }
            active = true;
            GetComponent<MeshCollider>().enabled = true;
        }

        public void Deactivate()
        {
            active = false;
            GetComponent<MeshCollider>().enabled = false;
            vfx.Stop();
            enemies.Clear();
        }

        public bool isActive()
        {
            return active;
        }

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

        public void AssignDamageInfo(List<AttackElemental> dmg, GameObject caster, coneInfo info)
        {
            damageInfo.elementals = dmg;
            origin = caster;
            vfx.visualEffectAsset = info.particles;
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
            if (origin)
            {
                Quaternion rotation = Quaternion.Euler(90f, origin.transform.rotation.eulerAngles.y + 90f, -90f);
                gameObject.transform.parent.rotation = rotation;
            }
        }

        //Damage over time
        void TickRate()
        {
            if (Time.time >= cooldown)
            {
                foreach (Damageable enemy in enemies)
                {
                    if (enemy)
                        enemy.TakeDamage(damageInfo);
                }
                cooldown = Time.time + tickRate;
            }
        }
    }
}
