using System.Collections;
using System.Collections.Generic;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
using UnityEngine;

public class Moth : MonoBehaviour
{
    [SerializeField] private List<AttackElemental> attackElementalsList; // Do zmiany, bo na razie jest ogien
    public float damage = 10f;
    //private PlayerHealth playerHealth; (for the future)

    private void Awake()
    {
        //playerHealth = GameObject.Find("Player").GetComponent<PlayerHealth>(); (for the future)
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerTakeDamage(other);
    }

    private void PlayerTakeDamage(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        //playerHealth.TakeDamage(damage); (for the future)
        var playerDamageable = other.GetComponent<Damageable>();
        DamageInfo damageInfo = new DamageInfo(attackElementalsList, gameObject);

        playerDamageable.TakeDamage(damageInfo);
        Destroy(gameObject);   
    }
}
