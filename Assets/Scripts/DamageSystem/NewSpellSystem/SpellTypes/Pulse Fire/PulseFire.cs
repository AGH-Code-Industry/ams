using DamageSystem.NewSpellSystem.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.ReceiveDamage.Elementals;


namespace DamageSystem.NewSpellSystem.SpellTypes.PulseFire {
    public class PulseFire : Core.Spell {
        public PulseFireInfo spellInfo;

        public override string spellName { get => spellInfo.spellName; }
        public override string spellDescription { get => spellInfo.description; }

        [System.Serializable]
        public enum PelletType { SIMPLE, PURSUIT }
        
        [System.Serializable]
        public enum CastType { CONSTANT, BURSTS }

        // Variable for keeping track of how many shots remaining in a burst
        private int burstCounter = 0;

        // Delay between every pellet of a burst, could be added into spell info later on
        private float burstDelay = 0.08f;

        private float burstCooldown = 0f;

        private Transform castOrigin;

        public override bool isPrimarySpell()
        {
            return spellInfo.isPrimary;
        }
        public override bool isSecondarySpell()
        {
            return spellInfo.isSecondary;
        }
        public override void Cast(Transform _origin) {
            switch (spellInfo.castBehaviour) {
                case CastType.CONSTANT:
                    InitializePellet(_origin);
                    return;
                case CastType.BURSTS:
                    ShootBurst(3, _origin);
                    return;
            }
        }
    
        public override float GetCastTime() {
            return spellInfo.castTime;
        }

        public override float GetCooldown() {
            return spellInfo.cooldown;
        }

        public override void StopCast() { }

        void InitializePellet(Transform origin) {
            Rigidbody temp;
            temp = Instantiate(spellInfo.pellet, origin.position, origin.rotation) as Rigidbody;
            temp.AddForce(origin.forward * 500f);
            PulsePellet pelletInfo = temp.gameObject.AddComponent<PulsePellet>();
            
            pelletInfo.Init(origin.GetComponentInParent<Damageable>().gameObject, spellInfo.elementals, spellInfo.pelletLifeSpan, spellInfo.pelletBehaviour, spellInfo.pelletExplosion);

            AddPelletBehaviour(pelletInfo, temp);

            pelletInfo.Start();
        }

        void AddPelletBehaviour(PulsePellet info, Rigidbody pellet)
        {
            if (info.behaviour == PelletType.PURSUIT)
            {
                SphereCollider col = pellet.gameObject.AddComponent<SphereCollider>();
                col.isTrigger = true;
                col.radius = 20; //Here we can add customizability (probably to be set in pulseFireInfo)
                
                //FIX dla problemu obracania postaci z uzyciem raycastowania. Ta linijka kodu nie powinna byc potrzebna p�niej.
                pellet.gameObject.layer = 2;
            }
        }

        private void Update()
        {
            Burst();
        }

        public void ShootBurst(int burstSize, Transform origin)
        {
            castOrigin = origin;
            burstCounter = burstSize;
        }


        // Create a pellet if there are pellets to burst (burstCounter) and if the cooldown has elapsed (burstCooldown)
        private void Burst()
        {
            if (burstCounter > 0 && Time.time > burstCooldown)
            {
                burstCounter--;
                InitializePellet(castOrigin);
                burstCooldown = Time.time + burstDelay;
            }
        }

        // Old implementation of bursting
        /*IEnumerator shootBurst(int burstSize, Transform origin) {
            //Can add burstSize as a modifiable variable in PulseFireInfo :)
            for (int i = 0; i < burstSize; i++)
            {
                InitializePellet(origin);
                //Burst delay could also be a variable
                yield return new WaitForSeconds(0.08f);
            }
        }*/

    }
}