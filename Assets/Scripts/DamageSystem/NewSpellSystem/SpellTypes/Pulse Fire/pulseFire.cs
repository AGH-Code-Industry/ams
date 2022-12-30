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
                warto pamiêtaæ na przysz³oœæ gdyby to nadal nie dzia³a³o.*/


        public override void Cast(Transform _origin) {
            Rigidbody temp;
            temp = Instantiate(spellInfo.pellet, _origin.position, _origin.rotation) as Rigidbody;
            temp.AddForce(_origin.forward * 500f);
            PulsePellet pelletInfo = temp.gameObject.AddComponent<PulsePellet>();
            pelletInfo.lifeSpan = spellInfo.pelletLifeSpan;
            pelletInfo.behaviour = spellInfo.pelletBehaviour;

            if (pelletInfo.behaviour == PelletType.PURSUIT) {
                SphereCollider col = temp.gameObject.AddComponent<SphereCollider>();
                col.isTrigger = true;
                col.radius = 10; //Here we can add customizability (probably to be set in pulseFireInfo)
            }

            pelletInfo.AssignDamageInfo(spellInfo.elementals, _origin.gameObject);
            pelletInfo.Start();
        }

        public override float GetCastTime() {
            return spellInfo.castTime;
        }

        public override float GetCooldown() {
            return spellInfo.cooldown;
        }

        public override void StopCast() { }

    }
}