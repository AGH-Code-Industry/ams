using System;
using System.Collections;
using System.Collections.Generic;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
using UnityEngine;

public class TakeDamageTest : MonoBehaviour
{
    [SerializeField] List<AttackElemental> attackElementals1;
    [SerializeField] List<AttackElemental> attackElementals2;
    [SerializeField] List<AttackElemental> attackElementals3;

    public void Update()
    {
        var inputs = new List<bool>()
        {
            Input.GetKeyDown(KeyCode.P),
            Input.GetKeyDown(KeyCode.O),
            Input.GetKeyDown(KeyCode.I)
        };

        if (inputs.FindIndex(el => el == true) != -1)
        {
            var damageInfo = new DamageInfo();
            damageInfo.caster = this.gameObject;

            if (inputs[0])
            {
                damageInfo.elementals = attackElementals1;
            }
            else if (inputs[1])
            {
                damageInfo.elementals = attackElementals2;
            }
            else if (inputs[2])
            {
                damageInfo.elementals = attackElementals3;
            }
            

            Damageable.instance.TakeDamage(damageInfo);
        }
    }
}
