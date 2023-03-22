using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;


namespace DamageSystem.NewSpellSystem.SpellTypes.Beam
{
    public class basicLaserFxController : BeamFx
    {
        //Various VFX elements of the laser
        public ParticleSystem castParticles;
        public ParticleSystem collisionParticles;
        public LineRenderer lr;

        //Width multiplier of the line Renderer (for laser fade in & fade out)
        float defaultWidthMultiplier = 1.8f;
        //the rate at which the laser appears/disappears
        float castingSpeed = 0.2f;

        private void Start()
        {
            defaultWidthMultiplier = lr.widthMultiplier;
        }

        public override void UpdateFx(bool casting, Transform origin, Vector3 target)
        {
            //Set the origin and hit point of the laser (line renderer)
            lr.SetPosition(1, target);
            lr.SetPosition(0, origin.transform.position);

            if (casting)
            {
                //Turn on particles if they weren't on yet
                if (!castParticles.isPlaying || !collisionParticles.isPlaying)
                {
                    castParticles.Play();
                    collisionParticles.Play();
                }
                
                //enable the line renderer and make it wider ("animation")
                if(lr.enabled == false)
                {
                    lr.enabled = true;
                }else if(lr.widthMultiplier < defaultWidthMultiplier)
                {
                    lr.widthMultiplier += castingSpeed;
                }

                //Change the rotation of the parent gameObject (to make the particles rotate along the player)
                Quaternion rotation = Quaternion.Euler(0f, origin.transform.rotation.eulerAngles.y, 0f);
                gameObject.transform.parent.rotation = rotation;
                //Set the position of the particles at the tip of the laser
                collisionParticles.gameObject.transform.position = lr.GetPosition(1);
            }

            if(!casting)
            {
                //Turn off particles if they are still on
                if (castParticles.isPlaying || collisionParticles.isPlaying)
                {
                    castParticles.Stop();
                    collisionParticles.Stop();
                }

                //Animation logic for despawning the line renderer (laser)
                if(lr.widthMultiplier > 0)
                {
                    lr.widthMultiplier -= castingSpeed;
                }else if(lr.widthMultiplier < 0.1f)
                {
                    lr.enabled = false;
                }
            }
        }
    }
}
