using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.SpellTypes.Beam
{
    //A class used for implementing any Visual Effects for your laser
    public abstract class BeamFx : MonoBehaviour
    {
        /*
         Fields:
            casting - if the player is casting the spell
            origin - the transform of the caster (usually the player)
            target - the transform.position of the point where the laser hits
         */
        public abstract void UpdateFx(bool casting, Transform origin, Vector3 target);
    }
}
