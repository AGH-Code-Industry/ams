using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

    [Serializable]
public class PlayerStats
    {
        public float BaseValue;

        protected bool isDirty = true;
        protected float lastBaseValue;

        protected float _value;
        public virtual float Value
        {
            get
            {
                if (isDirty || lastBaseValue != BaseValue)
                {
                    lastBaseValue = BaseValue;
                    _value = CalculateFinalValue();
                    isDirty = false;
                }
                return _value;
            }
        }

        protected readonly List<StatsModifier> statsModifier;
        public readonly ReadOnlyCollection<StatsModifier> StatsModifier;

        public PlayerStats()
        {
            statsModifier = new List<StatsModifier>();
            StatsModifier = statsModifier.AsReadOnly();
        }

        public PlayerStats(float baseValue) : this()
        {
            BaseValue = baseValue;
        }

        public virtual void AddModifier(StatsModifier mod)
        {
            isDirty = true;
            statsModifier.Add(mod);
        }

        public virtual bool RemoveModifier(StatsModifier mod)
        {
            if (statsModifier.Remove(mod))
            {
                isDirty = true;
                return true;
            }
            return false;
        }

        public virtual bool RemoveAllModifiersFromSource(object source)
        {
            int numRemovals = statsModifier.RemoveAll(mod => mod.Source == source);

            if (numRemovals > 0)
            {
                isDirty = true;
                return true;
            }
            return false;
        }

        protected virtual int CompareModifierOrder(StatsModifier a, StatsModifier b)
        {
            if (a.Order < b.Order)
                return -1;
            else if (a.Order > b.Order)
                return 1;
            return 0; //if (a.Order == b.Order)
        }

        protected virtual float CalculateFinalValue()
        {
            float finalValue = BaseValue;
            float sumPercentAdd = 0;

            statsModifier.Sort(CompareModifierOrder);

            for (int i = 0; i < statsModifier.Count; i++)
            {
                StatsModifier mod = statsModifier[i];

                if (mod.Type == StatModType.Flat)
                {
                    finalValue += mod.Value;
                }
                else if (mod.Type == StatModType.PercentAdd)
                {
                    sumPercentAdd += mod.Value;

                    if (i + 1 >= statsModifier.Count || statsModifier[i + 1].Type != StatModType.PercentAdd)
                    {
                        finalValue *= 1 + sumPercentAdd;
                        sumPercentAdd = 0;
                    }
                }
                else if (mod.Type == StatModType.PercentMult)
                {
                    finalValue *= 1 + mod.Value;
                }
            }

            // Workaround for float calculation errors, like displaying 12.00001 instead of 12
            return (float)Math.Round(finalValue, 4);
        }
    }
