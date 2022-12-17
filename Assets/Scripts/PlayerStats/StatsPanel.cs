using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] StatsDisplay[] statDisplays;
    [SerializeField] string[] statNames;

    private PlayerStats[] stats;

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatsDisplay>();
        UpdateStatsName();
    }

    public void SetStats(params PlayerStats[] charStats)
    {
        stats = charStats;

        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < statDisplays.Length);
        }
    }

    public void UpdateStatsValue()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].ValueText.text = stats[i].Value.ToString();
        }
    }

    public void UpdateStatsName()
    {
        for (int i = 0; i < statNames.Length; i++)
        {
            statDisplays[i].NameText.text = statNames[i];
        }
    }

}