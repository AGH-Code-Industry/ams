using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.SpellTypes.PulseFire {

    [RequireComponent(typeof(Rigidbody))]
    public class PulsePellet : MonoBehaviour {
        public DamageInfo damageInfo;
        public int lifeSpan;
        public PulseFire.PelletType behaviour;
        // The amount of time that passes before the pursuit behaviour is activated
        public float pursuitDelay = 2f;
        private float activatePursuitTime = 0.0f;

        //FOR PURSUIT BEHAVIOUR
        GameObject target;

        private void OnCollisionEnter(Collision collision) {
            if (collision.gameObject.GetComponent<Damageable>()) {
                collision.gameObject.GetComponent<Damageable>().TakeDamage(damageInfo);
            }
            //Do starej implementacji dummy
            //if (collision.gameObject.GetComponent<Enemy>()) {

            //    collision.gameObject.GetComponent<Enemy>().DealDamage(damageInfo.elementals[0].elementalDamage, "Pulse Fire");
            //}
            Destroy(this.gameObject);
        }


        public void Start() {
            Destroy(gameObject, lifeSpan);
            StartCoroutine(disableCollisionForInitialisation());
            activatePursuitTime = Time.time + pursuitDelay;
            //Handle pellet behavior
        }

        public void AssignDamageInfo(List<AttackElemental> elementals, GameObject caster) {
            damageInfo.elementals = elementals;
            damageInfo.caster = caster;
        }

        //Logic for pursuit system

        private void FixedUpdate() {
            if (behaviour == PulseFire.PelletType.PURSUIT && target) {                
                GetComponent<Rigidbody>().velocity += (target.transform.position - transform.position) * 20 * Time.fixedDeltaTime;
            }
        }

        private void OnTriggerEnter(Collider other) {
            //Debug.Log("time: " + Time.time + " delayTime: " + activatePursuitTime);
            if (behaviour == PulseFire.PelletType.PURSUIT && Time.time > pursuitDelay  && (other.GetComponent<Damageable>() || other.GetComponent<Enemy>()) && other.transform.name != "Player") {
                //set target as that collider
                target = other.gameObject;
            }
        }

        private void OnTriggerExit(Collider other) {
            if (other.GetComponent<Damageable>() || other.GetComponent<Enemy>()) {
                //disable target
                target = null;
            }
        }


        //Player Collision workaround (please change)
        IEnumerator disableCollisionForInitialisation() {
            GetComponent<Collider>().enabled = false;
            yield return new WaitForSeconds(0.1f);
            GetComponent<Collider>().enabled = true;
        }
    }
}
