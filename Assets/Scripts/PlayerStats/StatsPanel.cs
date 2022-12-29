using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] StatsDisplay[] statsDisplay;
    [SerializeField] string[] statNames;
    [SerializeField] float[] statValues;
    private PlayerStats[] stats;
    

    public void OnValidate()
    {
        statsDisplay = GetComponentsInChildren<StatsDisplay>();
        
        UpdateStatNames();
        UpdateStatValues();
    }

    public void SetStats(params PlayerStats[] charStats)
    {
        stats = charStats;

        if (stats.Length > statsDisplay.Length)
        {
            Debug.LogError("Not Enough Stat Displays!");
            return;
        }

        for (int i = 0; i < statsDisplay.Length; i++)
        {
            statsDisplay[i].Stat = i < stats.Length ? stats[i] : null;
            statsDisplay[i].gameObject.SetActive(i < stats.Length);
        }
    }

    public void UpdateStatValues()
    {
        
        for (int i = 0; i < statValues.Length; i++)
        {
            statsDisplay[i].ValueText.text = statValues[i].ToString();
        }
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statsDisplay[i].NameText.text = statNames[i];
        }
    }
}