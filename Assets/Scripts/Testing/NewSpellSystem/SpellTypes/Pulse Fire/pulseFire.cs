using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pulseFire : SpellType
{
    public PulseFireInfo spellInfo;

    public override void Cast(Transform _origin)
    {
        Rigidbody temp;
        temp = Instantiate(spellInfo.pellet, _origin) as Rigidbody;
        temp.AddForce(_origin.forward * 500f);
    }

    public override float GetCastTime()
    {
        return spellInfo.castTime;
    }

    public override float GetCooldown()
    {
        return spellInfo.cooldown;
    }

    public override void StopCast() { }
    
}
