using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DamageSystem.NewSpellSystem.Core;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;

namespace DamageSystem.NewSpellSystem.SpellTypes.Beam
{
    public class Beam : SpellType
    {
        // public beamTriggerEntity trigger;
        public beamSpellInfo spellInfo;
        private Damageable target;

        float cooldown = 0f;
        DamageInfo damageInfo;
        GameObject origin;

        VisualEffect vfx;
        bool casting = false;
        float castingSpeed = 0.5f;
        public static float unitDistance = 0.175f;

        private void Start()
        {
            vfx = GetComponentInChildren<VisualEffect>();
            vfx.Stop();
        }

        public override void Cast(Transform _origin)
        {
            if(damageInfo.elementals == null || origin == null)
            {
                damageInfo.elementals = spellInfo.elementals;
                origin = _origin.gameObject;
            }
            RaycastHit hit;
            if (Physics.Raycast(_origin.position, _origin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {

                vfx.SetFloat("Length", unitDistance * hit.distance);
                if(!casting)
                {
                    vfx.SetFloat("Life", 0f);
                    vfx.Play();
                    casting = true;
                }



                if (hit.collider.GetComponent<Damageable>())
                { 
                    target = hit.collider.GetComponent<Damageable>();
                }
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
            casting = false;
            vfx.SetFloat("Life", 0.9f);
        }

        private void FixedUpdate()
        {
            TickRate();

            if (origin)
            {
                Quaternion rotation = Quaternion.Euler(0f, origin.transform.rotation.eulerAngles.y, 0f);
                gameObject.transform.parent.rotation = rotation;
            }
            if (casting && vfx.GetFloat("Life") < 0.9f)
            {
                vfx.SetFloat("Life", vfx.GetFloat("Life") + (Time.deltaTime * castingSpeed));
            }
            if(!casting && vfx.GetFloat("Life") >= 0.9f && vfx.GetFloat("Life") < 1f)
            {
                vfx.SetFloat("Life", vfx.GetFloat("Life") + (Time.deltaTime * castingSpeed));
            }
            else if(!casting)
            {
                vfx.Stop();
            }
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
