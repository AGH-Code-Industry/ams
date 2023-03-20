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

        bool casting = false;
        float castingSpeed = 0.2f;
        public static float unitDistance = 0.175f;

        public LineRenderer lr;
        float defaultWidthMultiplier = 1.8f;
        public ParticleSystem castParticles;


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
                if(!casting)
                {
                    casting = true;
                    castParticles.Play();
                }

                lr.SetPosition(1, hit.point);

                if (hit.collider.GetComponent<Damageable>())
                { 
                    target = hit.collider.GetComponent<Damageable>();
                }
            }
        }

        private void Start()
        {
            defaultWidthMultiplier = lr.widthMultiplier;
            castParticles.Stop();
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
            target = null;
            casting = false;
            castParticles.Stop();
        }

        private void FixedUpdate()
        {
            TickRate();

            if (origin)
            {
                Quaternion rotation = Quaternion.Euler(0f, origin.transform.rotation.eulerAngles.y, 0f);
                gameObject.transform.parent.rotation = rotation;
                lr.SetPosition(0, origin.transform.position);
            }

            if (!casting && lr.widthMultiplier > 0)
            {
                lr.widthMultiplier -= castingSpeed;
            }else if(!casting && lr.widthMultiplier < 0.1f)
            {
                lr.enabled = false;
            }
            
            if(casting && lr.enabled == false)
            {
                lr.enabled = true;
            }else if(casting && lr.widthMultiplier < defaultWidthMultiplier)
            {
                lr.widthMultiplier += castingSpeed;
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
