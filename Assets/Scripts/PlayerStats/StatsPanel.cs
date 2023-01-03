using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] StatsDisplay[] statsDisplay;
    [SerializeField] string[] statNames;
    [SerializeField] float[] statValues;
    private PlayerStats[] stats;

    public playerMovement playerMovement;



    public void OnValidate()
    {
        statsDisplay = GetComponentsInChildren<StatsDisplay>();

        playerMovement = GameObject.FindGameObjectWithTag("Player").GetComponent<playerMovement>();

        UpdateStatNames();
        UpdateStatValues();
    }
    public void Update()
    {
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
        
        for (int i = 1; i < statValues.Length; i++)
        {
            statsDisplay[i].ValueText.text = statValues[i].ToString();
        }
        statsDisplay[0].ValueText.text = playerMovement.speed.ToString();
    }

    public void UpdateStatNames()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statsDisplay[i].NameText.text = statNames[i];
        }
    }
}