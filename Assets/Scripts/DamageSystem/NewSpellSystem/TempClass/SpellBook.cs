using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.Core.Temp
{
    [RequireComponent(typeof(Collider))]
    public class SpellBook : MonoBehaviour
    {
        public Spell spell;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void OnTriggerEnter(Collider other)
        {
            /*if (other.transform.parent != null && other.transform.parent.GetComponent<PlayerSpellManager>())
            {
                if (other.transform.parent.GetComponent<PlayerSpellManager>().AddSpell(spell))
                {
                    Destroy(gameObject);
                }
            }*/
        }
    }
}
