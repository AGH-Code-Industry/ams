using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.NewSpellSystem.Core;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
//using DamageSystem.NewSpellSystem.SpellTypes.Cone;

namespace DamageSystem.NewSpellSystem.SpellTypes.Beam
{
    public class Beam : SpellType
    {
        // public beamTriggerEntity trigger;
        public beamSpellInfo spellInfo;
        private Damageable target;

        float cooldown = 0f;
        DamageInfo damageInfo;

        public override void Cast(Transform origin)
        {
            if(damageInfo.elementals == null)
            {
                damageInfo.elementals = spellInfo.elementals;
            }
            RaycastHit hit;
            if (Physics.Raycast(origin.position, origin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity) && hit.collider.GetComponent<Damageable>())
            {
                Debug.DrawRay(origin.position, origin.TransformDirection(Vector3.forward) * hit.distance, Color.red);
                //trigger.AdjustBeam(hit.transform);
                target = hit.collider.GetComponent<Damageable>();
            }
        }

        public override float GetCastTime()
        {
            return 0f;
        }

        public override float GetCooldown()
        {
            return 0f;
        }

        public override void StopCast()
        {
            Debug.Log("Stop :)");
            target = null;
        }

        private void FixedUpdate()
        {
            TickRate();
        }

        void TickRate()
        {
            if(Time.time >= cooldown && target && damageInfo.elementals != null)
            {
                target.TakeDamage(damageInfo);
                cooldown = Time.time + spellInfo.tickRate;
            }
        }
    }
}
