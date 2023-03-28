using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace DamageSystem.NewSpellSystem.Core 
{
    public abstract class SpellType : MonoBehaviour
    {
        public abstract string spellName { get; }
        public abstract string spellDescription { get; }

        //Bools used for determining if a spell is supposed to be a primary spell, secondary spell, or both
        public abstract bool isPrimarySpell();
        public abstract bool isSecondarySpell();

        public abstract void Cast(Transform origin);
        public abstract float GetCooldown();
        public abstract float GetCastTime();

        public abstract void StopCast(); //This method implemented only for spells that work "continuously" (ex.: beam, cone)
    }

}