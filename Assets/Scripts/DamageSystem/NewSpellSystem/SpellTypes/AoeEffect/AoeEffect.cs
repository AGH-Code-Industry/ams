using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.DamageNumbers;
using UnityEngine.VFX;


namespace DamageSystem.NewSpellSystem.SpellTypes.Aoe
{
    public class AoeEffect : MonoBehaviour
    {

        public static AoeEffect Spawn(Vector3 position, AoeInfo aoeInfo, GameObject origin)
        {
            Transform aoeTransform = Instantiate(GameAssets.i.aoePrefab, position, Quaternion.identity);
            AoeEffect aoe = aoeTransform.GetComponent<AoeEffect>();
            aoe.caster = origin;

            aoe.Setup(aoeInfo);

            return aoe;
        }

        private VisualEffect vfx;
        private DamageInfo damageInfo;
        private CapsuleCollider dmgTrigger;
        private float aoeRange;
        private GameObject caster;


        private void Awake()
        {
            vfx = GetComponentInChildren<VisualEffect>();
            dmgTrigger = GetComponent<CapsuleCollider>();
        }

        void Setup(AoeInfo info)
        {
            damageInfo.elementals = info.elementals;
            vfx.visualEffectAsset = info.vfx;
            dmgTrigger.radius = 0;
            aoeRange = info.aoeRange;

            vfx.Play();
        }

        private void Update()
        {
            float burstRate = 10f;
            dmgTrigger.radius += burstRate * Time.deltaTime;

            if(dmgTrigger.radius >= aoeRange)
            {
                dmgTrigger.enabled = false;
                if(vfx.aliveParticleCount <= 0)
                {
                    Destroy(gameObject);
                }
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.GetComponent<Damageable>() && other.gameObject != caster)
            {
                other.gameObject.GetComponent<Damageable>().TakeDamage(damageInfo);
            }
        }


    }
}
