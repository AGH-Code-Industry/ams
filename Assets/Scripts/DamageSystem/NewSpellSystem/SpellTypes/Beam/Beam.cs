using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using DamageSystem.NewSpellSystem.Core;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;

namespace DamageSystem.NewSpellSystem.SpellTypes.Beam
{
    public class Beam : Core.Spell
    {
        //Variable used for the damage ball at the tip of the laser (not implemented at the moment)
        //public beamTriggerEntity trigger;
        public beamSpellInfo spellInfo;

        public override string spellName { get => spellInfo.spellName; }
        public override string spellDescription { get => spellInfo.description; }

        public override bool isPrimarySpell()
        {
            return spellInfo.isPrimary;
        }
        public override bool isSecondarySpell()
        {
            return spellInfo.isSecondary;
        }

        private Damageable target;

        float cooldown = 0f;
        DamageInfo damageInfo;
        GameObject origin;

        bool casting = false;


        //Deprecated implementation of handling Laser VFX
        //Various VFX elements of the laser
        /*public LineRenderer lr;
        float castingSpeed = 0.2f;
        //Width multiplier of the line Renderer (for laser fade in & fade out)
        float defaultWidthMultiplier = 1.8f;
        public ParticleSystem castParticles;
        public ParticleSystem collisionParticles;*/

        //The position of the end of the laser
        Vector3 hitPoint;

        //The Vfx implementation of the laser
        [SerializeField, SerializeReference]
        public BeamFx beamVfx;


        public override void Cast(Transform _origin)
        {
            //Assign damage values and the origin of the spell (the player)
            if(damageInfo.elementals == null || origin == null)
            {
                damageInfo.elementals = spellInfo.elementals;
                origin = _origin.gameObject;
            }

            //Raycast to see where the laser will hit, and enabling the laser
            RaycastHit hit;
            if (Physics.Raycast(_origin.position, _origin.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
            {
                if(!casting)
                {
                    casting = true;
                    //Deprecated implementation of handling Laser VFX
                    //castParticles.Play();
                    //collisionParticles.Play();
                }
                //Deprecated implementation of handling Laser VFX
                //Set the destination of the laser (line renderer)
                // lr.SetPosition(1, hit.point);

                //assign the end point of the laser to hitPoint
                hitPoint = hit.point;

                //Assigning the target (what the laser hit) as the target, which will be dealt damage via TickRate()
                if (hit.collider.GetComponent<Damageable>())
                { 
                    target = hit.collider.GetComponent<Damageable>();
                }
                else
                {
                    target = null;
                }
            }
        }

        private void Start()
        {
            //Deprecated implementation of handling Laser VFX
            //defaultWidthMultiplier = lr.widthMultiplier;
            //castParticles.Stop();
            //collisionParticles.Stop();
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
            //Deprecated implementation of handling Laser VFX
            //castParticles.Stop();
            //collisionParticles.Stop();
        }

        private void FixedUpdate()
        {
            TickRate();

            //Called for updating the Visual Effects of the laser
            if(origin && hitPoint != null)
                beamVfx.UpdateFx(casting, origin.transform, hitPoint);
            //Deprecated implementation of handling Laser VFX
            //LaserVfxController();
        }


        //Method that handles dealing damage every "tick"
        void TickRate()
        {
            if(Time.time >= cooldown && target && damageInfo.elementals != null)
            {
                target.TakeDamage(damageInfo);
                cooldown = Time.time + spellInfo.tickRate;
            }
        }


        //Deprecated implementation of handling Laser VFX
        /*
        void LaserVfxController()
        {
            //Set the proper rotation for the Laser (to match player's rotation)
            //Set the origin of the laser to the position of the player
            if (origin && casting)
            {
                Quaternion rotation = Quaternion.Euler(0f, origin.transform.rotation.eulerAngles.y, 0f);
                gameObject.transform.parent.rotation = rotation;
                lr.SetPosition(0, origin.transform.position);
            }

            //Decrease width and then disable the laser VFX (line renderer)
            if (!casting && lr.widthMultiplier > 0)
            {
                lr.widthMultiplier -= castingSpeed;
            }
            else if (!casting && lr.widthMultiplier < 0.1f)
            {
                lr.enabled = false;
            }

            //Enable and increase the width of the laser VFX until it reaches the default width
            if (casting && lr.enabled == false)
            {
                lr.enabled = true;
            }
            else if (casting && lr.widthMultiplier < defaultWidthMultiplier)
            {
                lr.widthMultiplier += castingSpeed;
            }

            //Move the collision particles VFX to the point, where the laser hits the target
            if (casting)
            {
                collisionParticles.gameObject.transform.position = lr.GetPosition(1);
            }
        }*/
    }
}
