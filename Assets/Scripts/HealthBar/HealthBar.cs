using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    public Slider healthBar;
    public Gradient gradient;
    public Image fill;
    
    public void SetMaxHealth(int health)
    {
        healthBar.maxValue = health;
        fill.color = gradient.Evaluate(1f);
    }   
     
    public void SetHealth(int health)
    {
        healthBar.value = health;
        fill.color = gradient.Evaluate(healthBar.normalizedValue);
    }


}
