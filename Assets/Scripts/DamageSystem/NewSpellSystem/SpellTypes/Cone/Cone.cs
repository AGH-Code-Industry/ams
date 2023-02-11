using System.Collections;
using System.Collections.Generic;
using DamageSystem.NewSpellSystem.Core;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.SpellTypes.Cone
{
    
    public class Cone : SpellType
    {
        public coneInfo spellInfo;
        bool setup = false;

        //Collider coneTrigger;
        public GameObject coneTrigger;
        public override void Cast(Transform origin)
        {
            if (!coneTrigger.activeInHierarchy)
            {
                coneTrigger.SetActive(true);
                if(!setup)
                {
                    coneTrigger.GetComponent<coneEntity>().SetTickRate(spellInfo.tickRate);
                    coneTrigger.GetComponent<coneEntity>().AssignDamageInfo(spellInfo.elementals, origin.gameObject);
                    setup = true;
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
            if (coneTrigger.activeInHierarchy)
            {
                coneTrigger.SetActive(false);
            }
        }
    }
}
