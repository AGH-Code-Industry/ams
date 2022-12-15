using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Damageable : MonoBehaviour
{
    [SerializeField] private EntityInfo enity;
    public uint hp = 200;
    public List<Effect> effects; // do zamiany

    void TakeDamage(DamageInfo damage)
    {
        foreach (ElementalType damageElemental in damage.elementals)
        {
            hp -= (uint)damageElemental.damage;
        }
    }
}
