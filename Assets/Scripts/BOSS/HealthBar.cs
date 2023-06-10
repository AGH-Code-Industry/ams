using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BOSS
{
   public class HealthBar : MonoBehaviour
   {
      [SerializeField] private Slider slider;

      public static HealthBar instance;

      // Start is called before the first frame update    
      void Awake()
      {
         instance = this;
      }

      public void SetHealth(float health)
      {
         slider.value = health;
      }

      public void SetMaxHealth(float maxHealth)
      {
         slider.maxValue = maxHealth;
         slider.value = maxHealth;
      }
   }
}