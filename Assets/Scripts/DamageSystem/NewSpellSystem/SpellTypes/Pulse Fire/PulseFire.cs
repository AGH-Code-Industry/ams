using DamageSystem.NewSpellSystem.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DamageSystem.NewSpellSystem.SpellTypes.PulseFire {
    public class PulseFire : SpellType {
        public PulseFireInfo spellInfo;

        [System.Serializable]
        public enum PelletType { SIMPLE, PURSUIT }
        /*Bug log for pursuit behaviour:
            Postaæ siê dziwnie obraca podczas u¿ywania pulse Fire typu PURSUIT

            Prawdopodobny powód:
                Skrypt od obracania siê "trafia" w trigger u¿ywany przy detekcji magnetyzmu pocisków, przez co postaæ siê 
                próbuje odbiæ w jego stronê.

                Nie powinno byæ to problemem przy lepszej implementacji systemu obracania siê, ale jest to rzecz o której
                warto pamiêtaæ na przysz³oœæ gdyby to nadal nie dzia³a³o.
            
            Tymczasowe rozwi¹zanie:
                w void AddPelletBehaviour, nadaje pelletom z PURSUIT layer Ignore Raycast, w przysz³oœci (gdy bêdziemy u¿ywaæ innego
                    sposobu na obracanie postaci) j¹ wywaliæ.

         */
        [System.Serializable]
        public enum CastType { CONSTANT, BURSTS }

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
                    StartCoroutine(shootBurst(3, _origin));
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
            pelletInfo.lifeSpan = spellInfo.pelletLifeSpan;
            pelletInfo.behaviour = spellInfo.pelletBehaviour;

            AddPelletBehaviour(pelletInfo, temp);

            pelletInfo.AssignDamageInfo(spellInfo.elementals, origin.gameObject);
            pelletInfo.Start();
        }

        void AddPelletBehaviour(PulsePellet info, Rigidbody pellet)
        {
            if (info.behaviour == PelletType.PURSUIT)
            {
                SphereCollider col = pellet.gameObject.AddComponent<SphereCollider>();
                col.isTrigger = true;
                col.radius = 10; //Here we can add customizability (probably to be set in pulseFireInfo)
                
                //FIX dla problemu obracania postaci z uzyciem raycastowania. Ta linijka kodu nie powinna byc potrzebna póŸniej.
                pellet.gameObject.layer = 2;
            }
        }

        IEnumerator shootBurst(int burstSize, Transform origin) {
            //Can add burstSize as a modifiable variable in PulseFireInfo :)
            for (int i = 0; i < burstSize; i++)
            {
                InitializePellet(origin);
                //Burst delay could also be a variable
                yield return new WaitForSeconds(0.08f);
            }
        }

    }
}