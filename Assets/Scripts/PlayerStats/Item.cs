using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _Spell0
{

    public void Equip(PlayerFeatures c)
    {
        c.Mana.AddModifier(new StatsModifier(10, StatModType.Flat, this));
    }

    public void Unequip(PlayerFeatures c)
    {
        c.Mana.RemoveAllModifiersSource(this);
    }
}
