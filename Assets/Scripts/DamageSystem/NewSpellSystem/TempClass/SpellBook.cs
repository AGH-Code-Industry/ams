using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DamageSystem.NewSpellSystem.Core.Temp
{
    [RequireComponent(typeof(Collider))]
    public class SpellBook : MonoBehaviour
    {
        public GameObject spell;

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        //WIP, still need to redo it
        private void OnTriggerEnter(Collider other)
        {
            if (other.transform.GetComponentInParent<PlayerSpellManager>())
            {
                if (other.transform.GetComponentInParent<PlayerSpellManager>().AddSpell(spell, null, KeyCode.None))
                {
                    //Destroy(gameObject);
                    gameObject.GetComponent<MeshRenderer>().enabled = false;
                    gameObject.GetComponent<Collider>().enabled = false;
                }
            }
        }
    }
}
