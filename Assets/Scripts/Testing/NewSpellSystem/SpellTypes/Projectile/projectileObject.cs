using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class projectileObject : MonoBehaviour
{
    public DamageInfo damageInfo;
    public int lifeSpan;


    public void AssignDamageInfo(ElementalType[] elementals, GameObject caster)
    {
        damageInfo.elementals = elementals;
        damageInfo.caster = caster;
    }

    public void Start()
    {
        Destroy(this.gameObject, lifeSpan);
        StartCoroutine(DisableCollisionForInitialisation());
    }
    IEnumerator DisableCollisionForInitialisation() {
        gameObject.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<Collider>().enabled = true;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Damageable>())
        {
            collision.gameObject.GetComponent<Damageable>().TakeDamage(damageInfo);
        }
        //Do starej implementacji dummy
        if (collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().DealDamage(damageInfo.elementals[0].damage, "Projectile");
        }
    }
}
