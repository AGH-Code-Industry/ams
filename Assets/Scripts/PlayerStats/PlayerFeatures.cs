using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerFeatures : MonoBehaviour
{
    public PlayerStat speed;
    public PlayerStat strenght;
    public PlayerStat mana;
    public PlayerStat spell1;
    public PlayerStat spell2;

    [System.Serializable]
    public class PlayerStat
    {

        public float baseValue;
        public float value;

        public PlayerStat(float baseValue, float value)
        {
            this.baseValue = baseValue;
            this.value = value;
        }

        


    }

}




