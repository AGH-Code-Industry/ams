using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.NewSpellSystem.Core;

namespace DamageSystem.NewSpellSystem.SpellTypes.Aoe
{
    public class AoeSpell : SpellType
    {
        public AoeSpellInfo spellInfo;

        public override void Cast(Transform origin)
        {
            AoeEffect.Spawn(origin.position, spellInfo.aoeEffect, origin.gameObject);
        }

        public override float GetCastTime()
        {
            return spellInfo.castTime;
        }

        public override float GetCooldown()
        {
            return spellInfo.cooldown;
        }

        public override void StopCast()
        {
        }
    }
}
