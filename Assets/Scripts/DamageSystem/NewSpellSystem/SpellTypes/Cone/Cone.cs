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

        public coneEntity coneTrigger;
        public override void Cast(Transform origin)
        {
            if (!coneTrigger.isActive())
            {
                coneTrigger.Activate();
                if (!setup)
                {
                    coneTrigger.SetTickRate(spellInfo.tickRate);
                    coneTrigger.AssignDamageInfo(spellInfo.elementals, origin.gameObject, spellInfo);
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
            if (coneTrigger.isActive())
            {
                coneTrigger.Deactivate();
            }
        }
    }
}