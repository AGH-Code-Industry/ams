using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleProjectile : SpellType
{

    public SimpleProjectileInfo spellInfo;

    public override void Cast(Transform origin)
    {
        Rigidbody temp;
        temp = Instantiate(spellInfo.projectile, origin.position, origin.rotation) as Rigidbody;
        temp.AddForce(origin.forward * spellInfo.force);
        projectileObject projectile = temp.gameObject.AddComponent<projectileObject>();
        projectile.AssignDamageInfo(spellInfo.elementals, origin.gameObject);
        projectile.lifeSpan = spellInfo.projectileLifeSpan;
        projectile.Start();
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
