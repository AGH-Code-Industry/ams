using System.Collections.Generic;
using System.Collections.ObjectModel;
using System;


[Serializable] public class PlayerStats
{
    public float BaseValue;
    public float Value
    {
        get
        {
            if (isAltered || BaseValue != LastBaseValue)
            {
                LastBaseValue = BaseValue;
                _value = FinalValue();
                isAltered = false;
            }
            return _value;
        }
    }

    private readonly List<StatsModifier> statsModifiers;        //all stats modifiers, cant be altered

    private ReadOnlyCollection<StatsModifier> statsModifiersReadOnly;

    private float _value;           //recent calculations

    private bool isAltered = true;
    private float LastBaseValue = float.MinValue;
    

    public PlayerStats()
    {
        statsModifiers = new List<StatsModifier>();
        statsModifiersReadOnly = statsModifiers.AsReadOnly();

    }

    public PlayerStats(float baseValue) : this()
    {
        BaseValue = baseValue;
    }

    private int SortModifiers(StatsModifier a, StatsModifier b)
    {
        if (a.Order < b.Order)
            return -1;
        else if (a.Order > b.Order)
            return 1;
        return 0;
    }

    public void AddModifier(StatsModifier mod)
    {
        isAltered = true;
        statsModifiers.Add(mod);
        statsModifiers.Sort();
    }

    public bool RemoveModifier(StatsModifier mod)
    {
        if (statsModifiers.Remove(mod))
        {
            isAltered = true;
            return true;
        }
        return false;
    }

    public bool RemoveAllModifiersSource(object source)
    {
        bool didRemove = false;
        for (int i= statsModifiers.Count -1; i>=0; i--)
        {
            if(statsModifiers[i].Source == source)
            {
                isAltered = true;
                statsModifiers.RemoveAt(i);
            }
        }
        return didRemove;
    }
    private float FinalValue()
    {
        float finalValue = BaseValue;
        float sumPercentageAdd = 0;
        for (int i = 0; i < statsModifiers.Count; i++)
        {
            StatsModifier mod = statsModifiers[i];
            if (mod.Type == StatModType.Flat)
            {
                finalValue += statsModifiers[i].Value;
            }
            else if (mod.Type == StatModType.PercentageAdd)
            {
                sumPercentageAdd += mod.Value;
                if (i + 1 >= statsModifiers.Count || statsModifiers[i + 1].Type != StatModType.PercentageAdd)
                {
                    finalValue *= 1 + sumPercentageAdd;
                    sumPercentageAdd = 0;
                }
            }
            else if (mod.Type == StatModType.PercentageMult)
            {
                finalValue *= 1 + mod.Value;
            }
        }
        return (float)Math.Round(finalValue, 4);
    }
}
