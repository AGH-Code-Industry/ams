using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ItemStatsCount
{
    public string name;
    public float value;
}

public class ItemStatsCounter : MonoBehaviour
{
    public List<ItemStatsCount> statsCount = new List<ItemStatsCount>();

    private void Update()
    {
        // Znajdź wszystkie obiekty na scenie z komponentem ItemStack
        ItemStack[] itemStacks = FindObjectsOfType<ItemStack>();

        // Wyczyść listę statsCount
        statsCount.Clear();

        // Zlicz wartości każdej zmiennej statystyk dla każdego ItemStack
        foreach (ItemStack stack in itemStacks)
        {
            foreach (ItemStats stat in stack._item.stats)
            {
                // Szukaj zmiennej stat w liście statsCount
                ItemStatsCount statCount = statsCount.Find(x => x.name == stat.name);

                // Pomnóż wartość zmiennej stat przez _count
                float value = stat.value * stack._count;

                if (statCount != null)
                {
                    // Zwiększ wartość zmiennej stat
                    statCount.value += value;
                }
                else
                {
                    // Dodaj nową pozycję do listy statsCount
                    statsCount.Add(new ItemStatsCount { name = stat.name, value = value });
                }
            }
        }
    }
}