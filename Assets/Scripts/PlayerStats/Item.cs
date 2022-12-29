using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public void Equip(PlayerFeatures c)
    {
        // Create the modifiers and set the Source to "this"
        // Note that we don't need to store the modifiers in variables anymore
        c.Strength.AddModifier(new StatsModifier(10, StatModType.Flat, this));
    }

    public void Unequip(PlayerFeatures c)
    {
        // Remove all modifiers applied by "this" Item
        c.Strength.RemoveAllModifiersFromSource(this);
    }
}
