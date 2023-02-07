using System.Collections;
using System.Collections.Generic;
using DamageSystem.NewSpellSystem.Core;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.SpellTypes.Cone
{
    public class Cone : SpellType
    {
        public coneInfo spellInfo;

        Collider coneTrigger;
        public override void Cast(Transform origin)
        {
            if (!coneTrigger.gameObject.activeInHierarchy)
            {
                coneTrigger.gameObject.SetActive(true);
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
            if (coneTrigger.gameObject.activeInHierarchy)
            {
                coneTrigger.gameObject.SetActive(false);
            }
        }
    }
}
