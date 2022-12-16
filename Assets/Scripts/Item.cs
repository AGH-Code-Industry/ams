using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player
{
    public PlayerStats Strength;
}
public class Item
{

    public void Equip(Player c)
    {
        c.Strength.AddModifier(new StatsModifier(10, StatModType.Flat, this));
        c.Strength.AddModifier(new StatsModifier(10, StatModType.Flat, this));
    }

    public void Unequip(Player c)
    {
        c.Strength.RemoveAllModifiersSource(this);
    }
}
