using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PulsePellet : MonoBehaviour
{
    public DamageInfo damageInfo;
    public int lifeSpan;
    public pulseFire.PelletType behaviour;


    //FOR PURSUIT BEHAVIOUR
    GameObject target;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Damageable>()) {
            collision.gameObject.GetComponent<Damageable>().TakeDamage(damageInfo);
        }
        //Do starej implementacji dummy
        if (collision.gameObject.GetComponent<Enemy>())
        {
            collision.gameObject.GetComponent<Enemy>().DealDamage(damageInfo.elementals[0].damage, "Pulse Fire");
        }
            Destroy(this.gameObject);
    }
    

    public void Start()
    {
        Destroy(gameObject, lifeSpan);
        StartCoroutine(disableCollisionForInitialisation());
        //Handle pellet behavior
    }

    public void AssignDamageInfo(ElementalType[] elementals, GameObject caster)
    { 
        damageInfo.elementals = elementals;
        damageInfo.caster = caster;
    }

    //Logic for pursuit system

    private void FixedUpdate()
    {
        if (behaviour == pulseFire.PelletType.PURSUIT && target) {
            GetComponent<Rigidbody>().velocity += (target.transform.position - transform.position) * 20 * Time.deltaTime;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Damageable>() || other.GetComponent<Enemy>())
        {
            //set target as that collider
            target = other.gameObject;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<Damageable>() || other.GetComponent<Enemy>())
        {
            //disable target
            target = null;
        }
    }


    //Player Collision workaround (please change)
    IEnumerator disableCollisionForInitialisation()
    {
        GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(0.1f);
        GetComponent<Collider>().enabled = true;
    }
}
