using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DamageSystem.ReceiveDamage.Elementals;
using DamageSystem.ReceiveDamage.Elementals.Elementals;


namespace DamageSystem.NewSpellSystem.SpellTypes.Beam
{

    //Class not used (was supposed to be a trigger at the end of the beam)
    public class beamTriggerEntity : MonoBehaviour
    {
        float tickRate = 0.5f;
        GameObject origin;
        bool active = false;
        List<Damageable> enemies = new List<Damageable>();

        private void Start()
        {
            GetComponent<Collider>().enabled = false;
        }

        public void AdjustBeam(Transform _transform)
        {
            if(!active)
            {
                active = true;
                GetComponent<Collider>().enabled = true;
            }
            //transform. = _transform.position;
        }
    }
}
