using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatsPanel : MonoBehaviour
{
    [SerializeField] StatsDisplay[] statsDisplay;    
    PlayerFeatures playerFeatures;



    public void OnValidate()
    {
        statsDisplay = GetComponentsInChildren<StatsDisplay>();
        playerFeatures = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerFeatures>();

        UpdateStatNames();
        
    }
    public void Update()
    {
        UpdateStatValuesPercentage();
    }

    public void UpdateStatValues()
    {
        statsDisplay[0].ValueText.text = playerFeatures.speed.value.ToString();
        statsDisplay[1].ValueText.text = playerFeatures.strenght.value.ToString();
        statsDisplay[2].ValueText.text = playerFeatures.mana.value.ToString();
        statsDisplay[3].ValueText.text = playerFeatures.spell1.value.ToString();
        statsDisplay[4].ValueText.text = playerFeatures.spell2.value.ToString();
    }
    public void UpdateStatValuesPercentage()
    {
        statsDisplay[0].ValueText.text = (playerFeatures.speed.value / playerFeatures.speed.baseValue * 100).ToString() + "%";
        statsDisplay[1].ValueText.text = (playerFeatures.strenght.value / playerFeatures.strenght.baseValue * 100).ToString() + "%";
        statsDisplay[2].ValueText.text = (playerFeatures.mana.value / playerFeatures.mana.baseValue * 100).ToString() + "%";
        statsDisplay[3].ValueText.text = (playerFeatures.spell1.value / playerFeatures.spell1.baseValue * 100).ToString() + "%";
        statsDisplay[4].ValueText.text = (playerFeatures.spell2.value / playerFeatures.spell2.baseValue * 100).ToString() + "%";
        
    }

    public void UpdateStatNames()
    {
        statsDisplay[0].NameText.text = "Speed";
        statsDisplay[1].NameText.text = "Strenght";
        statsDisplay[2].NameText.text = "Mana";
        statsDisplay[3].NameText.text = "Spell1";
        statsDisplay[4].NameText.text = "Spell2";
    }
}