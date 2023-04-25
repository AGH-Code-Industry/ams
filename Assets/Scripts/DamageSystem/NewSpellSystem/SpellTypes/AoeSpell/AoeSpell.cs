using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.NewSpellSystem.Core;

namespace DamageSystem.NewSpellSystem.SpellTypes.Aoe
{
    public class AoeSpell : Core.Spell
    {
        public AoeSpellInfo spellInfo;

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
